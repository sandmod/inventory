using System.Collections.Generic;
using Sandmod.Core.Provider;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Item.Component;
using Sandmod.Inventory.Item.Component.Actions;

namespace Sandmod.Inventory.Provider;

[DefaultProvider(ProviderPriority.Internal)]
public class DefaultItemComponentProvider : IItemComponentProvider
{
    public IReadOnlyCollection<IItemComponent> Provide(IItemAsset asset)
    {
        List<IItemComponent> components = new();
        if (asset is IWorldItemAsset)
        {
            components.Add(new DropAction());
        }
        return components;
    }
}