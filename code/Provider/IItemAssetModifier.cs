using System.Collections.Generic;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Provider;

public interface IItemAssetModifier
{
    IReadOnlyCollection<IItemAsset> Modify(IReadOnlyCollection<IItemAsset> assets);
}