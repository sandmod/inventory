using Sandmod.Core.Action;

namespace Sandmod.Inventory.Item.Component.Actions;

public interface IEquipAction : IActionComponent
{
    string IAction.Text => "#equip";
}