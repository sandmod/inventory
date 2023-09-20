using Sandbox;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Network;

namespace Sandmod.Inventory.Item;

internal sealed partial class ItemComponent : EntityComponent,
    IItemComponent
{
    [Net] private NetItem NetItem { get; set; }

    public new IEntity Entity => base.Entity;

    public IItem<IItemAsset, IEntity> Item => NetItem.Item;

    public ItemComponent()
    {
    }

    public ItemComponent(IItem<IItemAsset, IEntity> item)
    {
        Game.AssertServer();
        NetItem = new NetItem(item);
    }
}