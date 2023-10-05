namespace Sandmod.Inventory.Item.Context;

public interface IConditionItemContext : IItemContext
{
    decimal Condition { get; set; }
}