using Sandbox;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item;

public interface IItemEntity<out TItem> : IEntity
    where TItem : IItem<IItemAsset, IEntity>
{
    TItem Item => Components.Get<IItemComponent>().GetItem<TItem>();
}