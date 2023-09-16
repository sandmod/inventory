using Sandbox;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item;

public interface IItemEntity<out TAsset> : IEntity where TAsset : IItemAsset
{
    IItem<TAsset> Item => Components.Get<IItemComponent<TAsset>>().Item;
}