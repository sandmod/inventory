using Sandmod.Core.Provider;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Provider;

[DefaultProvider(ProviderPriority.Internal)]
internal sealed class DefaultItemProvider : IItemProvider
{
    public IItem<TAsset> Provide<TAsset>(TAsset asset) where TAsset : class, IItemAsset
    {
        return new DefaultItem<TAsset>(asset);
    }
}