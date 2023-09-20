using System;
using System.Linq;
using Sandbox;
using Sandmod.Core.Util;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item.Verify;

public static class VerifyExtensions
{
    public static void Verify(this IItemAsset self)
    {
        var assetType = self.GetType();
        if (self.ItemType == null)
        {
            throw new Exception($"No item type on item \"{self.Id}\"");
        }

        var itemType = self.ItemType.TargetType;
        var genericItemType = typeof(IItem<,>);
        if (!GenericUtil.AllowsGenericInterfaceType(itemType, genericItemType, assetType))
        {
            var requiredTypes = string.Join(", ",
                GenericUtil.GetGenericInterfaceTypes(itemType, genericItemType).Select(type => type.FullName));
            throw new Exception(
                $"Item type \"{itemType.FullName}\" requires asset type \"{requiredTypes}\" but got \"{assetType}\"");
        }
    }

    public static void Verify(this IItem<IItemAsset, IEntity> self)
    {
        var itemType = self.GetType();
        if (self.EntityType == null) return;
        var entityType = self.EntityType.TargetType;
        var genericItemType = typeof(IItem<,>);
        if (!GenericUtil.AllowsGenericInterfaceType(itemType, genericItemType, entityType, 1))
        {
            var requiredTypes = string.Join(", ",
                GenericUtil.GetGenericInterfaceTypes(itemType, genericItemType, 1).Select(type => type.FullName));
            throw new Exception(
                $"Item type \"{itemType.FullName}\" requires entity type \"{requiredTypes}\" but got \"{entityType}\"");
        }

        var genericItemEntityType = typeof(IItemEntity<>);
        if (GenericUtil.HasGenericInterface(entityType, typeof(IItemEntity<>)))
        {
            if (!GenericUtil.AllowsGenericInterfaceType(entityType, genericItemEntityType, itemType))
            {
                var requiredTypes = string.Join(", ",
                    GenericUtil.GetGenericInterfaceTypes(itemType, genericItemType).Select(type => type.FullName));
                throw new Exception(
                    $"Entity type \"{itemType.FullName}\" requires item type \"{requiredTypes}\"but got \"{itemType}\"");
            }
        }
    }
}