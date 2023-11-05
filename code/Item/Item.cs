#nullable enable
using System;
using System.IO;
using Sandbox;
using Sandmod.Core.Network;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Item.Component;
using Sandmod.Inventory.Network;

namespace Sandmod.Inventory.Item;

public class Item : IItem
{
    public IItemAsset Asset { get; }

    public IItemParent Parent { get; set; }

    public IItemComponentSystem Components { get; }

    public ulong NetworkIdent { get; set; }

    protected bool InternalIsDirty;

    public virtual bool IsDirty
    {
        get => InternalIsDirty;
        set
        {
            InternalIsDirty = value;
            if (InternalIsDirty)
            {
                OnMarkedDirty?.Invoke();
            }
            else
            {
                Components.IsDirty = false;
            }
        }
    }

    public event INetworkSerializable.MarkedDirty? OnMarkedDirty;

    public Item(IItemAsset asset)
    {
        Components = new ItemComponentSystem(this);
        Asset = asset;
    }

    public virtual void NetWrite(BinaryWriter writer)
    {
        if (Parent.IsContainer())
        {
            writer.Write(true);
            writer.Write(Parent.AsContainer().NetworkIdent);
        }
        else
        {
            writer.Write(false);
            writer.Write(Parent.AsEntity().NetworkIdent);
        }

        Components.NetWrite(writer);
    }

    public virtual void NetRead(BinaryReader reader)
    {
        var isContainerParent = reader.ReadBoolean();
        if (isContainerParent)
        {
            var networkIdent = reader.ReadUInt64();
            if (NetContainer.Registry.TryGetValue(networkIdent, out var container))
            {
                Parent = container;
            }
            else
            {
                throw new Exception($"Container parent {networkIdent} not existing on client");
            }
        }
        else
        {
            var networkIdent = reader.ReadInt32();
            var entity = Entity.FindByIndex(networkIdent);
            Parent = entity.Components.Get<IItemParentComponent>();
        }

        Components.NetRead(reader);
    }
}