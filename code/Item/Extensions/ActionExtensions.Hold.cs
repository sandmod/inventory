using Sandmod.Inventory.Item.Component.Actions;

namespace Sandmod.Inventory.Item;

public static partial class ActionExtensions
{
    public static bool IsHoldable(this IItem item)
    {
        return item.Components.Get<IHoldAction>() != null;
    }

    public static bool CanHold(this IItem item)
    {
        return item.Components.Get<IHoldAction>().CanExecute();
    }

    public static bool TryHold(this IItem item)
    {
        return item.Components.Get<IHoldAction>().TryExecute();
    }

    public static bool IsHolsterable(this IItem item)
    {
        return item.Components.Get<IHolsterAction>() != null;
    }

    public static bool CanHolster(this IItem item)
    {
        return item.Components.Get<IHolsterAction>().CanExecute();
    }

    public static bool TryHolster(this IItem item)
    {
        return item.Components.Get<IHolsterAction>().TryExecute();
    }
}