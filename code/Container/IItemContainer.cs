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

    bool CanAdd(IItem<IItemAsset, IEntity> item);

    bool TryAdd(IItem<IItemAsset, IEntity> item);

    bool CanRemove(IItem<IItemAsset, IEntity> item);

    bool TryRemove(IItem<IItemAsset, IEntity> item);
}