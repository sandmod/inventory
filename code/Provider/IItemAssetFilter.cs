using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Provider;

public interface IItemAssetFilter
{
    bool Filter(IItemAsset asset);
}