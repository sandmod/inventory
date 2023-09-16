using Sandbox;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item;

public interface IItemComponent<out TAsset> : IComponent where TAsset : IItemAsset
{
    IItem<TAsset> Item { get; }
}