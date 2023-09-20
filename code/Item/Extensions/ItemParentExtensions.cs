using Sandbox;
using Sandmod.Inventory.Container;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item.Extensions;

public static class ItemParentExtensions
{
    public static bool IsContainer(this IItemParent self)
    {
        return self.GetType().IsAssignableTo(typeof(IItemContainer<>));
    }

    public static IItemContainer<IItem<IItemAsset, IEntity>> AsContainer(this IItemParent self)
    {
        return self as IItemContainer<IItem<IItemAsset, IEntity>>;
    }

    public static bool IsEntity(this IItemParent self)
    {
        return self.GetType().IsAssignableTo(typeof(IItemComponent));
    }

    public static IEntity AsEntity(this IItemParent self)
    {
        return (self as IItemComponent)?.Entity;
    }
}