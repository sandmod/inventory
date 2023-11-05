using Sandbox;
using Sandmod.Inventory.Item;

namespace Sandmod.Inventory.Provider;

public interface IItemEntityProvider
{
    IEntity Provide(IItem item);
}