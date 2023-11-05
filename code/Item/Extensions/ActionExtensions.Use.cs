using Sandmod.Inventory.Item.Component.Actions;

namespace Sandmod.Inventory.Item;

public static partial class ActionExtensions
{
    public static bool IsUseable(this IItem item)
    {
        return item.Components.Get<IUseAction>() != null;
    }

    public static bool CanUse(this IItem item)
    {
        return item.Components.Get<IUseAction>().CanExecute();
    }

    public static bool TryUse(this IItem item)
    {
        return item.Components.Get<IUseAction>().TryExecute();
    }
}