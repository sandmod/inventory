using Sandbox;
using Sandmod.Core.Action;

namespace Sandmod.Inventory.Item.Component;

public interface IActionComponent : IItemComponent, IAction, ISingletonComponent
{
}