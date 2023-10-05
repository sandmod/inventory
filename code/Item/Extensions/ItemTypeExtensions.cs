using Sandbox;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Item.Types;

namespace Sandmod.Inventory.Item.Extensions;

public static class ItemTypeExtensions
{
    public static bool IsClothing(this IItem<IItemAsset, IEntity> self)
    {
        return self.GetType().IsAssignableTo(typeof(IClothing));
    }

    public static IClothing AsClothing(this IItem<IItemAsset, IEntity> self)
    {
        return self as IClothing;
    }

    public static bool IsContainer(this IItem<IItemAsset, IEntity> self)
    {
        return self.GetType().IsAssignableTo(typeof(IContainer<>));
    }

    public static IContainer<IItem<IItemAsset, IEntity>> AsContainer(
        this IItem<IItemAsset, IEntity> self)
    {
        return self as IContainer<IItem<IItemAsset, IEntity>>;
    }

    public static bool IsEquippable(this IItem<IItemAsset, IEntity> self)
    {
        return self.GetType().IsAssignableTo(typeof(IEquippable));
    }

    public static IEquippable AsEquippable(
        this IItem<IItemAsset, IEntity> self)
    {
        return self as IEquippable;
    }

    public static bool IsHoldable(this IItem<IItemAsset, IEntity> self)
    {
        return self.GetType().IsAssignableTo(typeof(IHoldable));
    }

    public static IHoldable AsHoldable(
        this IItem<IItemAsset, IEntity> self)
    {
        return self as IHoldable;
    }
}