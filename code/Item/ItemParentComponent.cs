using Sandbox;
using Sandmod.Inventory.Network;

namespace Sandmod.Inventory.Item;

internal sealed partial class ItemParentComponent : EntityComponent,
    IItemParentComponent
{
    [Net] private NetItem NetItem { get; set; }

    public new IEntity Entity => base.Entity;

    public IItem Item => NetItem.Networked;

    public ItemParentComponent()
    {
    }

    public ItemParentComponent(IItem item)
    {
        Game.AssertServer();
        NetItem = new NetItem(item);
        item.Parent = this;
    }
}