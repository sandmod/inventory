using Sandbox;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Item.Types;

namespace Sandmod.Inventory.Item.Extensions;

public static class ItemTypeExtensions
{
    public static bool IsClothing(this IItem<IItemAsset, IEntity> self)
    {
        return self.GetType().IsAssignableTo(typeof(IClothingItem<,>));
    }

    public static IClothingItem<IItemAsset, IEntity> AsClothing(this IItem<IItemAsset, IEntity> self)
    {
        return self as IClothingItem<IItemAsset, IEntity>;
    }

    public static bool IsContainer(this IItem<IItemAsset, IEntity> self)
    {
        return self.GetType().IsAssignableTo(typeof(IContainerItem<,,>));
    }

    public static IContainerItem<IItemAsset, IEntity, IItem<IItemAsset, IEntity>> AsContainer(
        this IItem<IItemAsset, IEntity> self)
    {
        return self as IContainerItem<IItemAsset, IEntity, IItem<IItemAsset, IEntity>>;
    }

    public static bool IsEquippable(this IItem<IItemAsset, IEntity> self)
    {
        return self.GetType().IsAssignableTo(typeof(IEquippableItem<,>));
    }

    public static IEquippableItem<IItemAsset, IEntity> AsEquippable(
        this IItem<IItemAsset, IEntity> self)
    {
        return self as IEquippableItem<IItemAsset, IEntity>;
    }
}