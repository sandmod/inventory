using System;
using System.Collections.Generic;
using System.IO;
using Sandbox;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Network;

public sealed class NetItem : BaseNetworkable, INetworkSerializer
{
    private static Dictionary<ulong, IItem<IItemAsset, IEntity>> InternalItems { get; } = new();
    public static IReadOnlyDictionary<ulong, IItem<IItemAsset, IEntity>> Items => InternalItems;
    private static ulong LastNetworkIdent { get; set; } = 1;
    private static ulong NextNetworkIdent => LastNetworkIdent++;

    public static void Register(IItem<IItemAsset, IEntity> item)
    {
        if (Items.TryGetValue(item.NetworkIdent, out var foundItem))
        {
            if (item == foundItem) return;
            // Should only happen if someone messes with the NetworkIdent
            item.NetworkIdent = NextNetworkIdent;
            InternalItems.Add(item.NetworkIdent, item);
        }
        else
        {
            // Item was not networked yet
            item.NetworkIdent = NextNetworkIdent;
            InternalItems.Add(item.NetworkIdent, item);
        }
    }

    public static void WriteItem(BinaryWriter writer, IItem<IItemAsset, IEntity> item)
    {
        writer.Write(item.NetworkIdent);
        writer.Write(item.Asset.Id);

        using var stream = new MemoryStream();
        using var itemWriter = new BinaryWriter(stream);
        item.NetWrite(itemWriter);
        var data = stream.ToArray();
        writer.Write(data.Length);
        writer.Write(data);
    }

    public static IItem<IItemAsset, IEntity> ReadItem(BinaryReader reader)
    {
        var networkIdent = reader.ReadUInt64();
        var assetId = reader.ReadString();
        IItem<IItemAsset, IEntity> item;
        if (Items.TryGetValue(networkIdent, out var foundItem))
        {
            item = foundItem;
        }
        else
        {
            var asset = InventorySystem.GetAsset<IItemAsset>(assetId);
            if (asset == null) throw new Exception($"Asset \"{assetId}\" not existing on client");
            var createdItem = InventorySystem.CreateItem<IItem<IItemAsset, IEntity>>(asset);
            createdItem.NetworkIdent = networkIdent;
            InternalItems.Add(createdItem.NetworkIdent, createdItem);
            item = createdItem;
        }

        var totalBytes = reader.ReadInt32();
        var data = reader.ReadBytes(totalBytes);
        using var stream = new MemoryStream(data);
        using var itemReader = new BinaryReader(stream);
        item.NetRead(itemReader);
        return item;
    }

    public IItem<IItemAsset, IEntity> Item { get; private set; }

    private uint Version;

    public NetItem()
    {
    }

    public NetItem(IItem<IItemAsset, IEntity> item)
    {
        Game.AssertServer();
        Register(item);

        Item = item;
        Item.OnMarkedDirty += () =>
        {
            WriteNetworkData();
            Item.IsDirty = false;
        };
    }

    public void Write(NetWrite write)
    {
        write.Write(++Version);

        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        WriteItem(writer, Item);

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
        Item = ReadItem(reader);
    }
}