using System.Collections.Generic;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Provider;

public interface IItemAssetModifier
{
    void Modify(IItemAsset asset);
}