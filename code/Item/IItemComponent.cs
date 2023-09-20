#nullable enable
using Sandbox;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item;

public interface IItemComponent : IComponent, IItemParent
{
    new IEntity Entity { get; }

    IItem<IItemAsset, IEntity> Item { get; }

    T? GetItem<T>() where T : IItem<IItemAsset, IEntity>
    {
        if (!Item.GetType().IsAssignableTo(typeof(T))) return default;
        return (T) Item;
    }
}