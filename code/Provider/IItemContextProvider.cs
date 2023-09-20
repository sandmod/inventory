using Sandbox;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Item.Context;

namespace Sandmod.Inventory.Provider;

public interface IItemContextProvider
{
    IItemContext Provide();
}