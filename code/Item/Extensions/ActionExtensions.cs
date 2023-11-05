using System.Collections.Generic;
using Sandmod.Core.Action;
using Sandmod.Inventory.Item.Component;

namespace Sandmod.Inventory.Item;

public static partial class ActionExtensions
{
    public static IEnumerable<IAction> GetActions(this IItem item)
    {
        return item.Components.GetAll<IActionComponent>();
    }
}