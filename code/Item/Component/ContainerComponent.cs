using Sandmod.Inventory.Container;

namespace Sandmod.Inventory.Item.Component;

public class ContainerComponent : ItemContainer, IContainerComponent
{
    public IItem Item { get; set; }
}