using System.Collections.Generic;
using Sandmod.Core.Network;
using Sandmod.Inventory.Item;

namespace Sandmod.Inventory.Container;

public interface IItemContainer : IItemParent, INetworkSerializable
{
    IReadOnlyCollection<IItem> Items { get; }

    IContainerSettings Settings { get; }

    bool CanAdd(IItem item);

    bool TryAdd(IItem item);

    bool CanRemove(IItem item);

    bool TryRemove(IItem item);
}