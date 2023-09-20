using System.Collections.Generic;
using Sandbox;
using Sandmod.Inventory.Container;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item.Types;

public interface IContainerItem<out TAsset, out TEntity, TItem> : IItem<TAsset, TEntity>
    where TAsset : IItemAsset where TEntity : IEntity where TItem : IItem<IItemAsset, IEntity>
{
    IReadOnlyCollection<IItemContainer<TItem>> Containers { get; }
}