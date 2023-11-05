using Sandmod.Inventory.Item.Component.Actions;

namespace Sandmod.Inventory.Item;

public static partial class ActionExtensions
{
    public static bool IsDroppable(this IItem item)
    {
        return item.Components.Get<IDropAction>() != null;
    }

    public static bool CanDrop(this IItem item)
    {
        return item.Components.Get<IDropAction>().CanExecute();
    }

    public static bool TryDrop(this IItem item)
    {
        return item.Components.Get<IDropAction>().TryExecute();
    }
}