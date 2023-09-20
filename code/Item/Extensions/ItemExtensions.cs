using System;
using Sandbox;
using Sandmod.Core.Util;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Item.Verify;

namespace Sandmod.Inventory.Item;

public static class ItemExtensions
{
    public static IItem<IItemAsset, IEntity> CreateItem(this IItemAsset self)
    {
        return self.CreateItem<IItem<IItemAsset, IEntity>>();
    }

    public static TItem CreateItem<TItem>(this IItemAsset self)
        where TItem : IItem<IItemAsset, IEntity>
    {
        return InventorySystem.CreateItem<TItem>(self);
    }

    public static IEntity Spawn<TItem>(this TItem self,
        Vector3 position)
        where TItem : IItem<IItemAsset, IEntity>
    {
        Game.AssertServer();
        var type = self.EntityType;
        if (type == null)
        {
            throw new Exception($"No entity type on item type \"{self.GetType()}\" for item \"{self.Id}\"");
        }

        self.Verify();

        object[] constructorArgs = Array.Empty<object>();
        if (GenericUtil.HasGenericInterface(type.TargetType, typeof(IItemEntity<>)))
        {
            constructorArgs = new object[] {self};
        }

        var entity = TypeLibrary.Create<IEntity>(type.TargetType, constructorArgs);
        var component = new ItemComponent(self);
        self.Parent = component;
        entity.Components.Add(component);
        entity.Position = position;
        return entity;
    }
}