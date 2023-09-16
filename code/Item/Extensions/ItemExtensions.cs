using System;
using Sandbox;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item;

public static class ItemExtensions
{
    public static IItem<TAsset> CreateItem<TAsset>(this TAsset self) where TAsset : class, IItemAsset
    {
        return InventorySystem.CreateItem(self);
    }

    public static TEntity Spawn<TEntity, TAsset>(this IItem<TAsset> self, Vector3 position)
        where TEntity : class, IEntity
        where TAsset : IItemAsset
    {
        Game.AssertServer();
        var asset = self.Asset;
        var type = asset.EntityType;
        if (type == null)
        {
            throw new Exception($"No entity type on item \"{asset.Id}\"");
        }

        var entityType = type.TargetType;
        if (!entityType.IsAssignableTo(typeof(TEntity)))
        {
            Log.Error($"Invalid entity type \"{type.FullName}\" on item \"{asset.Id}\"");
            return null;
        }

        if (entityType.IsAssignableTo(typeof(IItemEntity<IItemAsset>)))
            if (!entityType.IsAssignableTo(typeof(IItemEntity<TAsset>)))
            {
                Log.Error("Invalid item type");
                return null;
            }

        var entity = TypeLibrary.Create<TEntity>(entityType, new object[] {self});
        entity.Components.Add(new ItemComponent<TAsset>(self));
        entity.Position = position;
        return entity;
    }
}