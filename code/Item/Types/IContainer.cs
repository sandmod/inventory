using System.Collections.Generic;
using Sandbox;
using Sandmod.Inventory.Container;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item.Types;

public interface IContainer<TItem> where TItem : IItem<IItemAsset, IEntity>
{
    IReadOnlyCollection<IItemContainer<IItem<IItemAsset, IEntity>>> Containers { get; }

    IReadOnlyCollection<IItemContainer<TItem>> TypedContainers { get; }
}