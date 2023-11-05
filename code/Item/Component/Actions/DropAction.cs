using System.IO;
using System.Linq;
using Sandbox;
using Sandmod.Core.Network;

namespace Sandmod.Inventory.Item.Component.Actions;

public class DropAction : IDropAction
{
    public IItem Item { get; set; }

    public ulong NetworkIdent { get; set; }

    public bool IsDirty
    {
        get => false;
        set { }
    }

    public event INetworkSerializable.MarkedDirty OnMarkedDirty;

    public bool CanExecute()
    {
        Log.Info(Item.Parent);
        return Item.Parent.IsContainer() && Item.Parent.AsContainer().CanRemove(Item);
    }

    public bool TryExecute()
    {
        if (!CanExecute()) return false;
        // TODO use parent
        var entity = Item.Spawn(Game.Clients.First().Position + new Vector3(0, 0, 70));
        Item.Parent.AsContainer().TryRemove(Item);
        entity.Components.Add(new ItemParentComponent(Item));
        return true;
    }

    public void NetWrite(BinaryWriter writer)
    {
    }

    public void NetRead(BinaryReader reader)
    {
    }
}