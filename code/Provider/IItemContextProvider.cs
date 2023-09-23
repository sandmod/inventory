using Sandmod.Inventory.Item.Context;

namespace Sandmod.Inventory.Provider;

public interface IItemContextProvider
{
    IItemContext Provide();
}