using Sandmod.Core.Components;
using Sandmod.Core.Network;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Item.Component;

namespace Sandmod.Inventory.Item;

public interface IItem : INetworkSerializable
{
    IItemAsset Asset { get; }

    IItemParent Parent { get; set; }

    IItemComponentSystem Components { get; }
}