#nullable enable
using Sandbox;
using Sandmod.Core.Network;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Item.Context;

namespace Sandmod.Inventory.Item;

public interface IItem<out TAsset, out TEntity> : INetworkSerializable
    where TAsset : IItemAsset where TEntity : IEntity
{
    TAsset Asset { get; }

    IItemContext Context { get; }

    IItemParent Parent { get; set; }

    string Id => Asset.Id;

    string Name => Asset.Name;

    TypeDescription? EntityType { get; }

    bool CanDrop => EntityType != null;

    T? GetContext<T>() where T : IItemContext
    {
        if (!Context.GetType().IsAssignableTo(typeof(T))) return default;
        return (T) Context;
    }
}