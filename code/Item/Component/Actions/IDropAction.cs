using Sandmod.Core.Action;

namespace Sandmod.Inventory.Item.Component.Actions;

public interface IDropAction : IActionComponent
{
    string IAction.Text => "#drop";
}