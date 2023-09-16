using System.Collections.Generic;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Provider;

public interface IItemAssetProvider
{
    IReadOnlyCollection<IItemAsset> Provide();
}