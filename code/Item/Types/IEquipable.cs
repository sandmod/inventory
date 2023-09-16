using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item.Types;

public interface IEquippable<out TAsset> : IItemEntity<TAsset> where TAsset : IItemAsset
{
    bool CanEquip();

    bool TryEquip();
}