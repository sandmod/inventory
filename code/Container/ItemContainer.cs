#nullable enable
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sandbox;
using Sandmod.Core.Network;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Network;

namespace Sandmod.Inventory.Container;

public class ItemContainer<TItem> : IItemContainer<TItem> where TItem : IItem<IItemAsset, IEntity>
{
    protected List<TItem> InternalItems = new();

    public IReadOnlyCollection<TItem> Items => InternalItems;

    private int _internalSize;

    public int Size
    {
        get => _internalSize;
        protected set
        {
            _internalSize = value;
            IsDirty = true;
        }
    }

    public ulong NetworkIdent { get; set; }

    private bool _internalDirty;

    public bool IsDirty
    {
        get => _internalDirty;
        set
        {
            _internalDirty = value;
            if (_internalDirty)
            {
                OnMarkedDirty?.Invoke();
            }
        }
    }

    public event INetworkSerializable.MarkedDirty? OnMarkedDirty;

    public ItemContainer()
    {
    }

    public ItemContainer(int size)
    {
        _internalSize = size;
    }

    public virtual bool CanAdd(IItem<IItemAsset, IEntity> item)
    {
        return Items.Count < Size && item.GetType().IsAssignableTo(typeof(TItem));
    }

    public virtual bool TryAdd(IItem<IItemAsset, IEntity> item)
    {
        var allowed = CanAdd(item);
        if (!allowed) return false;
        InternalItems.Add((TItem) item);
        IsDirty = true;
        return true;
    }

    public virtual bool CanRemove(IItem<IItemAsset, IEntity> item)
    {
        return item.GetType().IsAssignableTo(typeof(TItem)) && Items.Contains((TItem) item);
    }

    public virtual bool TryRemove(IItem<IItemAsset, IEntity> item)
    {
        var allowed = CanRemove(item);
        if (!allowed) return false;
        InternalItems.Remove((TItem) item);
        IsDirty = true;
        return true;
    }

    public virtual void NetWrite(BinaryWriter writer)
    {
        writer.Write(Size);
        writer.Write(InternalItems.Count);
        foreach (var item in InternalItems)
        {
            NetItem.Register(item);
            NetItem.WriteItem(writer, item);
        }
    }

    public virtual void NetRead(BinaryReader reader)
    {
        Size = reader.ReadInt32();
        var count = reader.ReadInt32();
        List<TItem> items = new(count);
        for (var i = 0; i < count; i++)
        {
            items.Add((TItem) NetItem.ReadItem(reader));
        }

        InternalItems.Clear();
        InternalItems.AddRange(items);
    }
}