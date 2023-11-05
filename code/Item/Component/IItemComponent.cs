using Sandmod.Core.Network;

namespace Sandmod.Inventory.Item.Component;

public interface IItemComponent : INetworkSerializable
{
    IItem Item { get; set; }
}