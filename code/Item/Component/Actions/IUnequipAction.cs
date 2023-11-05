using Sandmod.Core.Action;

namespace Sandmod.Inventory.Item.Component.Actions;

public interface IUnequipAction : IActionComponent
{
    string IAction.Text => "#unequip";
}