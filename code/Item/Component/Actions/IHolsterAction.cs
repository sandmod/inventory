using Sandmod.Core.Action;

namespace Sandmod.Inventory.Item.Component.Actions;

public interface IHolsterAction : IActionComponent
{
    string IAction.Text => "#holster";
}