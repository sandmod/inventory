using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item.Types;

public interface IClothing<out TAsset> : IEquippable<TAsset> where TAsset : IItemAsset
{
}