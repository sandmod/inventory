using Sandmod.Core.Provider;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Provider;

[DefaultProvider(ProviderPriority.Internal)]
internal sealed class DefaultItemProvider : IItemProvider
{
    public IItem Provide(IItemAsset asset)
    {
        IItem item = new Item.Item(asset);

        foreach (var component in InventorySystem.ProvideItemComponents(asset))
        {
            item.Components.Add(component);
        }

        return item;
    }
}