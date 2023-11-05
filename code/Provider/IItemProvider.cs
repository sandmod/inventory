using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Provider;

public interface IItemProvider
{
    IItem Provide(IItemAsset asset);
}