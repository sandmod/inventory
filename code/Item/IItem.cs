#nullable enable
using Sandbox;
using Sandbox.Internal;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item;

public interface IItem<out TAsset> : INetworkTable where TAsset : IItemAsset
{
    TAsset Asset { get; }

    string Id => Asset.Id;

    string Name => Asset.Name;

    IEntity? Entity { get; }

    IItemContainer<TAsset> Container { get; }
}