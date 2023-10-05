namespace Sandmod.Inventory.Item.Types;

public interface IEquippable
{
    bool CanEquip();

    bool TryEquip();
}