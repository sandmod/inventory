using System.Collections.Generic;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory;

public interface IItemContainer<out TAsset> where TAsset : IItemAsset
{
    IReadOnlyCollection<IItem<TAsset>> Items { get; }
}