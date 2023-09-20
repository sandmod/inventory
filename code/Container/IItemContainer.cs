using System.Collections.Generic;
using Sandbox;
using Sandmod.Core.Network;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Container;

public interface IItemContainer<TItem> : IItemParent, INetworkSerializable
    where TItem : IItem<IItemAsset, IEntity>
{
    IReadOnlyCollection<TItem> Items { get; }

    int Size { get; }

    bool CanAdd(TItem item);

    bool TryAdd(TItem item);

    bool CanRemove(TItem item);

    bool TryRemove(TItem item);
}