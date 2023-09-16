using System.Collections.Generic;
using Sandmod.Core.Provider;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Provider;

[DefaultProvider(ProviderPriority.Internal)]
internal sealed class DefaultItemAssetModifier : IItemAssetModifier
{
    public IReadOnlyCollection<IItemAsset> Modify(IReadOnlyCollection<IItemAsset> assets)
    {
        return assets;
    }
}