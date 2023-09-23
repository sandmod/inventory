#nullable enable
using System;
using System.IO;
using Sandbox;
using Sandmod.Core.Network;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Item.Context;
using Sandmod.Inventory.Item.Extensions;
using Sandmod.Inventory.Network;

namespace Sandmod.Inventory.Item;

public class Item<TAsset, TEntity> : IItem<TAsset, TEntity>
    where TAsset : IItemAsset where TEntity : class, IEntity
{
    public TAsset Asset { get; }

    public IItemContext Context { get; }
    public IItemParent Parent { get; set; }
    public virtual TypeDescription? EntityType => null;

    public ulong NetworkIdent { get; set; }

    private bool _internalIsDirty;

    public bool IsDirty
    {
        get => _internalIsDirty;
        set
        {
            _internalIsDirty = value;
            if (_internalIsDirty)
            {
                OnMarkedDirty?.Invoke();
            }
            else
            {
                Context.IsDirty = false;
            }
        }
    }

    public event INetworkSerializable.MarkedDirty? OnMarkedDirty;

    public Item(TAsset asset, IItemContext context)
    {
        Asset = asset;
        Context = context;
        context.OnMarkedDirty += () => IsDirty = true;
    }

    public virtual void NetWrite(BinaryWriter writer)
    {
        if (Parent.IsContainer)
        {
            writer.Write(true);
            writer.Write(Parent.AsContainer().NetworkIdent);
        }
        else
        {
            writer.Write(false);
            writer.Write(Parent.AsEntity().NetworkIdent);
        }

        Context.NetWrite(writer);
    }

    public virtual void NetRead(BinaryReader reader)
    {
        var isContainerParent = reader.ReadBoolean();
        if (isContainerParent)
        {
            var networkIdent = reader.ReadUInt64();
            if (NetContainer.Containers.TryGetValue(networkIdent, out var container))
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
            Parent = entity.Components.Get<IItemComponent>();
        }

        Context.NetRead(reader);
    }
}