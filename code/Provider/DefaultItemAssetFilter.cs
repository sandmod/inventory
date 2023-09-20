using System.Collections.Generic;
using Sandmod.Core.Provider;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Provider;

[DefaultProvider(ProviderPriority.Internal)]
internal sealed class DefaultItemAssetFilter : IItemAssetFilter
{
    public bool Filter(IItemAsset asset)
    {
        return true;
    }
}