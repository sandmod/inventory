using System.Collections.Generic;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Item.Component;

namespace Sandmod.Inventory.Provider;

public interface IItemComponentProvider
{
    IReadOnlyCollection<IItemComponent> Provide(IItemAsset asset);
}