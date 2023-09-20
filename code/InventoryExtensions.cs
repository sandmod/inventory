using Sandbox;

namespace Sandmod.Inventory;

public static class InventoryExtensions
{
    public static IInventoryComponent GetInventory(this IEntity self)
    {
        return self.Components.Get<IInventoryComponent>();
    }
}