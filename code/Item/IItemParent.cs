using Sandbox;
using Sandmod.Inventory.Container;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item;

public interface IItemParent
{
    bool IsContainer => GetType().IsAssignableTo(typeof(IItemContainer<>));
    IItemContainer<IItem<IItemAsset, IEntity>> Container => this as IItemContainer<IItem<IItemAsset, IEntity>>;

    bool IsEntity => GetType().IsAssignableTo(typeof(IItemComponent));
    IEntity Entity => (this as IItemComponent)?.Entity;
}