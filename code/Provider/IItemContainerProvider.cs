using Sandbox;
using Sandmod.Inventory.Container;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Provider;

public interface IItemContainerProvider
{
    IItemContainer<TItem> Provide<TItem>(IContainerSetting setting)
        where TItem : IItem<IItemAsset, IEntity>;
}