using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sandbox;
using Sandmod.Core.Network;
using Sandmod.Core.Util;
using Sandmod.Inventory.Network;

namespace Sandmod.Inventory.Item.Component;

public sealed class ItemComponentSystem : IItemComponentSystem
{
    private bool InternalIsDirty;

    public ItemComponentSystem(IItem item)
    {
        Item = item;
    }

    private IItem Item { get; }

    private List<IItemComponent> Components { get; } = new();

    public int Count => Components.Count;

    public ulong NetworkIdent
    {
        get => Item.NetworkIdent;
        set { }
    }

    public bool IsDirty
    {
        get => InternalIsDirty;
        set
        {
            InternalIsDirty = value;
            if (InternalIsDirty)
                OnMarkedDirty?.Invoke();
            else
                foreach (var component in Components)
                    component.IsDirty = false;
        }
    }

    public event INetworkSerializable.MarkedDirty OnMarkedDirty;

    /*public T Create<T>(bool startEnabled = true) where T : class, IItemComponent, new()
    {
        var component = new T();
        return Add(component) ? component : null;
    }*/

    public T Create<T>(bool startEnabled = true) where T : class, IItemComponent, new()
    {
        var component = new T();
        return Add(component) ? component : null;
    }

    public T GetOrCreate<T>(bool startEnabled = true) where T : class, IItemComponent, new()
    {
        var component = Get<T>();
        return component ?? Create<T>();
    }

    public T Get<T>(bool includeDisabled = false) where T : class, IItemComponent
    {
        return GetAll<T>().First();
    }

    public bool TryGet<T>(out T component, bool includeDisabled = false) where T : class, IItemComponent
    {
        component = Get<T>();
        return component != null;
    }

    public IEnumerable<T> GetAll<T>(bool includeDisabled = false) where T : class, IItemComponent
    {
        return Components.Select(component => component as T).Where(component => component != null);
    }

    public bool Add(IItemComponent component)
    {
        if (component == null) throw new ArgumentException("component is null");
        if (component.Item == Item) return true;

        RemoveSingletons(component.GetType());
        component.OnMarkedDirty += MarkDirty;
        Components.Add(component);
        component.Item = Item;
        IsDirty = true;
        return true;
    }

    public bool Remove(IItemComponent component)
    {
        if (!InternalRemove(component)) return false;
        IsDirty = true;
        return true;
    }

    public bool RemoveAny(Type type)
    {
        if (!InternalRemoveAny(type)) return false;
        IsDirty = true;
        return true;
    }

    public bool RemoveAny<T>() where T : IItemComponent
    {
        if (!InternalRemoveAny(typeof(T))) return false;
        IsDirty = true;
        return true;
    }

    public void RemoveAll()
    {
        var removed = false;
        foreach (var component in Components)
        {
            component.Item = null;
            removed = InternalRemove(component) || removed;
        }

        if (removed) IsDirty = true;
    }

    public void NetWrite(BinaryWriter writer)
    {
        writer.Write(Components.Count);
        foreach (var component in Components) NetComponent.Registry.Write(writer, component);
    }

    public void NetRead(BinaryReader reader)
    {
        var count = reader.ReadInt32();
        List<IItemComponent> components = new(count);
        for (var i = 0; i < count; i++) components.Add(NetComponent.Registry.Read(reader));

        Components.Clear();
        Components.AddRange(components);
    }

    private void MarkDirty()
    {
        IsDirty = true;
    }

    private bool InternalRemove(IItemComponent component)
    {
        if (!Components.Remove(component)) return false;
        component.Item = null;
        component.OnMarkedDirty -= MarkDirty;
        return true;
    }

    private bool InternalRemoveAny(Type type)
    {
        return Components.Where(component => component.GetType().IsAssignableTo(type)).Any(InternalRemove);
    }

    private void RemoveSingletons(Type type)
    {
        var singletonType = TypeUtil.GetImplementingInterfaceType(type, typeof(ISingletonComponent));
        if (singletonType != null) InternalRemoveAny(singletonType);
    }
}