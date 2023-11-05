using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandmod.Inventory.Container;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Network;

namespace Sandmod.Inventory;

public partial class InventoryComponent : EntityComponent, IInventoryComponent
{
    [Net, Local] protected List<NetContainer> NetContainers { get; set; }

    public IReadOnlyCollection<IItemContainer> Containers =>
        NetContainers.Select(netContainer => netContainer.Networked).ToList();

    public IReadOnlyCollection<IItem> Items =>
        Containers.SelectMany(container => container.Items).ToList();

    public InventoryComponent()
    {
        if (Game.IsServer)
        {
            NetContainers = new List<NetContainer>
            {
                new (new ItemContainer(new ContainerSettings(5)))
            };
        }
    }

    public bool CanAdd(IItem item)
    {
        return Containers.Any(container => container.CanAdd(item));
    }

    public bool TryAdd(IItem item)
    {
        return Containers.Any(container => container.TryAdd(item));
    }

    public bool CanRemove(IItem item)
    {
        return Containers.Any(container => container.CanRemove(item));
    }

    public bool TryRemove(IItem item)
    {
        return Containers.Any(container => container.TryRemove(item));
    }
}