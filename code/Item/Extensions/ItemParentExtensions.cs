using Sandbox;
using Sandmod.Inventory.Container;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item;

public static class ItemParentExtensions
{
    public static bool IsContainer(this IItemParent self)
    {
        return self.GetType().IsAssignableTo(typeof(IItemContainer));
    }

    public static IItemContainer AsContainer(this IItemParent self)
    {
        return self as IItemContainer;
    }

    public static bool IsEntity(this IItemParent self)
    {
        return self.GetType().IsAssignableTo(typeof(IItemParentComponent));
    }

    public static IEntity AsEntity(this IItemParent self)
    {
        return (self as IItemParentComponent)?.Entity;
    }
}