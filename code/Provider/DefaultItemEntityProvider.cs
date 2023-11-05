using System;
using Sandbox;
using Sandmod.Core.Provider;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Provider;

[DefaultProvider(ProviderPriority.Internal)]
internal sealed class DefaultItemEntityProvider : IItemEntityProvider
{
    public IEntity Provide(IItem item)
    {
        if (item.Asset is not IWorldItemAsset asset)
        {
            throw new Exception(
                $"Item asset \"{item.Asset.GetType().FullName}\" for item \"{item.Id()}\" has to be of type {nameof(IWorldItemAsset)} to be spawnable");
        }

        var type = asset.EntityType;
        if (type == null)
        {
            throw new Exception($"Item asset of item \"{item.Id()}\" has no entity type to be spawned");
        }

        return TypeLibrary.Create<IEntity>(type.TargetType);
    }
}