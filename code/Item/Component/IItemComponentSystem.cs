using Sandmod.Core.Components;
using Sandmod.Core.Network;

namespace Sandmod.Inventory.Item.Component;

public interface IItemComponentSystem : IComponentSystem<IItemComponent>, INetworkSerializable
{
}