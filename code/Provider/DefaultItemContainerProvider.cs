using Sandbox;
using Sandmod.Core.Provider;
using Sandmod.Inventory.Container;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Provider;

[DefaultProvider(ProviderPriority.Internal)]
public class DefaultItemContainerProvider : IItemContainerProvider
{
    public IItemContainer<TItem> Provide<TItem>(IContainerSetting setting)
        where TItem : IItem<IItemAsset, IEntity>
    {
        return new ItemContainer<TItem>(setting.Size);
    }
}