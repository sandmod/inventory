using Sandbox;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item;

public static class ItemExtensions
{
    public static string Id(this IItem self)
    {
        return self.Asset.Id;
    }

    public static string Name(this IItem self)
    {
        return self.Asset.Name;
    }

    public static IItem CreateItem(this IItemAsset self)
    {
        return InventorySystem.CreateItem(self);
    }

    public static IEntity Spawn(this IItem self)
    {
        return InventorySystem.SpawnItem(self);
    }

    public static IEntity Spawn(this IItem self, Vector3 position)
    {
        var entity = self.Spawn();
        entity.Position = position;
        return entity;
    }
}