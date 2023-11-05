using Sandmod.Core.Network;

namespace Sandmod.Inventory.Container;

public interface IContainerSettings : INetworkSerializable
{
    int Size { get; set; }
}