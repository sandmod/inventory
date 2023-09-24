using System;
using System.Collections.Generic;
using System.IO;
using Sandbox;
using Sandmod.Core.Network;
using Sandmod.Inventory.Container;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Network;

public sealed class NetContainer : BaseNetworkable, INetworkSerializer
{
    private static Dictionary<ulong, IItemContainer<IItem<IItemAsset, IEntity>>> InternalContainers { get; } = new();

    public static IReadOnlyDictionary<ulong, IItemContainer<IItem<IItemAsset, IEntity>>> Containers =>
        InternalContainers;

    private static ulong LastNetworkIdent { get; set; } = 1;
    private static ulong NextNetworkIdent => LastNetworkIdent++;

    public static void Register(IItemContainer<IItem<IItemAsset, IEntity>> container)
    {
        if (Containers.TryGetValue(container.NetworkIdent, out var foundContainer))
        {
            if (container == foundContainer) return;
            // Should only happen if someone messes with the NetworkIdent
            container.NetworkIdent = NextNetworkIdent;
            InternalContainers.Add(container.NetworkIdent, container);
        }
        else
        {
            // Container was not networked yet
            container.NetworkIdent = NextNetworkIdent;
            InternalContainers.Add(container.NetworkIdent, container);
        }
    }

    public static void WriteContainer(BinaryWriter writer, IItemContainer<IItem<IItemAsset, IEntity>> container)
    {
        writer.Write(container.NetworkIdent);
        writer.Write(container.GetType());

        using var stream = new MemoryStream();
        using var containerWriter = new BinaryWriter(stream);
        container.NetWrite(containerWriter);
        var data = stream.ToArray();
        writer.Write(data.Length);
        writer.Write(data);
    }

    public static IItemContainer<IItem<IItemAsset, IEntity>> ReadContainer(BinaryReader reader)
    {
        var networkIdent = reader.ReadUInt64();
        var type = reader.ReadType();

        IItemContainer<IItem<IItemAsset, IEntity>> container;
        if (Containers.TryGetValue(networkIdent, out var foundContainer))
        {
            container = foundContainer;
        }
        else
        {
            var createdContainer = TypeLibrary.Create<IItemContainer<IItem<IItemAsset, IEntity>>>(type);
            createdContainer.NetworkIdent = networkIdent;
            InternalContainers.Add(createdContainer.NetworkIdent, createdContainer);
            container = createdContainer;
        }

        var totalBytes = reader.ReadInt32();
        var data = reader.ReadBytes(totalBytes);
        using var stream = new MemoryStream(data);
        using var containerReader = new BinaryReader(stream);
        container.NetRead(containerReader);
        return container;
    }

    public IItemContainer<IItem<IItemAsset, IEntity>> Container { get; private set; }

    private uint Version;

    public NetContainer()
    {
    }

    public NetContainer(IItemContainer<IItem<IItemAsset, IEntity>> container)
    {
        Game.AssertServer();
        Register(container);

        Container = container;
        Container.OnMarkedDirty += () =>
        {
            WriteNetworkData();
            Container.IsDirty = false;
        };
    }

    public void Write(NetWrite write)
    {
        write.Write(++Version);

        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        WriteContainer(writer, Container);

        var data = stream.ToArray();
        write.Write(data.Length);
        write.Write(data);
    }

    public void Read(ref NetRead read)
    {
        var version = read.Read<uint>();

        var totalBytes = read.Read<int>();
        var data = new byte[totalBytes];
        read.ReadUnmanagedArray(data);

        if (version == Version) return;
        Version = version;

        using var stream = new MemoryStream(data);
        using var reader = new BinaryReader(stream);
        Container = ReadContainer(reader);
    }
}