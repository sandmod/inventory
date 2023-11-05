using Sandmod.Inventory.Item.Component.Actions;

namespace Sandmod.Inventory.Item;

public static partial class ActionExtensions
{
    public static bool IsEquippable(this IItem item)
    {
        return item.Components.Get<IEquipAction>() != null;
    }

    public static bool CanEquip(this IItem item)
    {
        return item.Components.Get<IEquipAction>().CanExecute();
    }

    public static bool TryEquip(this IItem item)
    {
        return item.Components.Get<IEquipAction>().TryExecute();
    }

    public static bool IsUnequippable(this IItem item)
    {
        return item.Components.Get<IUnequipAction>() != null;
    }

    public static bool CanUnequip(this IItem item)
    {
        return item.Components.Get<IUnequipAction>().CanExecute();
    }

    public static bool TryUnequip(this IItem item)
    {
        return item.Components.Get<IUnequipAction>().TryExecute();
    }
}