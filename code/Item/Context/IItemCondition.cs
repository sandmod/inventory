namespace Sandmod.Inventory.Item.Context;

public interface IItemCondition : IItemContext
{
    decimal Condition { get; set; }
}