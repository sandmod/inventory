#nullable enable
using Sandbox;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item;

public interface IItemParentComponent : IComponent, IItemParent, ISingletonComponent
{
    IEntity Entity { get; }

    IItem Item { get; }
}