namespace Sandmod.Inventory.Item.Context;

public interface IContainerItemContext : IItemContext
{
    int Slot { get; }
}