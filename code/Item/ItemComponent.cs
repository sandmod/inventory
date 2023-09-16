using Sandbox;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item;

internal sealed partial class ItemComponent<TAsset> : EntityComponent, IItemComponent<TAsset> where TAsset : IItemAsset
{
    [Net] public IItem<TAsset> Item { get; private set; }

    public ItemComponent()
    {
    }

    public ItemComponent(IItem<TAsset> item)
    {
        Item = item;
    }
}