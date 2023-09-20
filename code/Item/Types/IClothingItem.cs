using Sandbox;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item.Types;

public interface IClothingItem<out TAsset, out TEntity> : IItem<TAsset, TEntity>
    where TAsset : IItemAsset where TEntity : IEntity
{
}