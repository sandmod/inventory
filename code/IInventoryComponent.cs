using System.Collections.Generic;
using Sandbox;
using Sandmod.Inventory.Container;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory;

public interface IInventoryComponent : IComponent
{
    IReadOnlyCollection<IItemContainer<IItem<IItemAsset, IEntity>>> Containers { get; }

    IReadOnlyCollection<IItem<IItemAsset, IEntity>> Items { get; }

    bool CanAdd(IItem<IItemAsset, IEntity> item);

    bool TryAdd(IItem<IItemAsset, IEntity> item);

    bool CanRemove(IItem<IItemAsset, IEntity> item);

    bool TryRemove(IItem<IItemAsset, IEntity> item);
}