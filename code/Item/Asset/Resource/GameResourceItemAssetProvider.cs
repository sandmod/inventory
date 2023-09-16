using System.Collections.Generic;
using System.Linq;
using Sandmod.Core.Provider;
using Sandmod.Inventory.Provider;

namespace Sandmod.Inventory.Item.Asset.Resource;

[Provider]
internal sealed class GameResourceItemAssetProvider : IItemAssetProvider
{
    public IReadOnlyCollection<IItemAsset> Provide()
    {
        var resources = ResourceLibrary.GetAll<GameResourceItemAsset>();
        return resources.Select(resource => resource.ItemAsset).ToList();
    }
}