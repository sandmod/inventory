using Sandmod.Core.Action;

namespace Sandmod.Inventory.Item.Component.Actions;

public interface IUseAction : IActionComponent
{
    string IAction.Text => "#use";
}