using Sandmod.Core.Action;

namespace Sandmod.Inventory.Item.Component.Actions;

public interface IHoldAction : IActionComponent
{
    string IAction.Text => "#hold";
}