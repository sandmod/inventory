#nullable enable
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sandmod.Core.Network;
using Sandmod.Inventory.Container.Extensions;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Network;

namespace Sandmod.Inventory.Container;

public class ItemContainer : IItemContainer
{
    protected List<IItem> InternalItems = new();

    public IReadOnlyCollection<IItem> Items => InternalItems;

    public IContainerSettings Settings { get; protected set; }

    public ulong NetworkIdent { get; set; }

    protected bool InternalDirty;

    public virtual bool IsDirty
    {
        get => InternalDirty;
        set
        {
            InternalDirty = value;
            if (InternalDirty)
            {
                OnMarkedDirty?.Invoke();
            }
        }
    }

    public event INetworkSerializable.MarkedDirty? OnMarkedDirty;

    public ItemContainer()
    {
    }

    public ItemContainer(IContainerSettings settings)
    {
        Settings = settings;
    }

    public virtual bool CanAdd(IItem item)
    {
        return Items.Count < this.MaxSize();
    }

    public virtual bool TryAdd(IItem item)
    {
        var allowed = CanAdd(item);
        if (!allowed) return false;
        InternalItems.Add(item);
        item.Parent = this;
        IsDirty = true;
        return true;
    }

    public virtual bool CanRemove(IItem item)
    {
        return Items.Contains(item);
    }

    public virtual bool TryRemove(IItem item)
    {
        var allowed = CanRemove(item);
        if (!allowed) return false;
        InternalItems.Remove(item);
        item.Parent = null;
        IsDirty = true;
        return true;
    }

    public virtual void NetWrite(BinaryWriter writer)
    {
        NetContainerSettings.Registry.Write(writer, Settings);
        
        writer.Write(InternalItems.Count);
        foreach (var item in InternalItems)
        {
            NetItem.Registry.Write(writer, item);
        }
    }

    public virtual void NetRead(BinaryReader reader)
    {
        Settings = NetContainerSettings.Registry.Read(reader);
        
        var count = reader.ReadInt32();
        List<IItem> items = new(count);
        for (var i = 0; i < count; i++)
        {
            items.Add(NetItem.Registry.Read(reader));
        }
        InternalItems.Clear();
        InternalItems.AddRange(items);
    }
}