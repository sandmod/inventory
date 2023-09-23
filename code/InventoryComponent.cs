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

    public IReadOnlyCollection<IItemContainer<IItem<IItemAsset, IEntity>>> Containers =>
        NetContainers.Select(netContainer => netContainer.Container).ToList();

    public IReadOnlyCollection<IItem<IItemAsset, IEntity>> Items =>
        Containers.SelectMany(container => container.Items).ToList();

    public InventoryComponent()
    {
        if (Game.IsServer)
        {
            NetContainers = new List<NetContainer>
            {
                new NetContainer(InventorySystem.CreateContainer<IItem<IItemAsset, IEntity>>(new ContainerSetting(5)))
            };
        }
    }

    public bool CanAdd(IItem<IItemAsset, IEntity> item)
    {
        return Containers.Any(container => container.CanAdd(item));
    }

    public bool TryAdd(IItem<IItemAsset, IEntity> item)
    {
        return Containers.Any(container => container.TryAdd(item));
    }

    public bool CanRemove(IItem<IItemAsset, IEntity> item)
    {
        return Containers.Any(container => container.CanRemove(item));
    }

    public bool TryRemove(IItem<IItemAsset, IEntity> item)
    {
        return Containers.Any(container => container.TryRemove(item));
    }
}