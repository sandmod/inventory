using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Provider;

public interface IItemProvider
{
    IItem<TAsset> Provide<TAsset>(TAsset asset) where TAsset : class, IItemAsset;
}