using System.Collections.Generic;
using Sandbox;
using Sandmod.Inventory.Container;
using Sandmod.Inventory.Item;

namespace Sandmod.Inventory;

public interface IInventoryComponent : IComponent
{
    IReadOnlyCollection<IItemContainer> Containers { get; }

    IReadOnlyCollection<IItem> Items { get; }

    bool CanAdd(IItem item);

    bool TryAdd(IItem item);

    bool CanRemove(IItem item);

    bool TryRemove(IItem item);
}