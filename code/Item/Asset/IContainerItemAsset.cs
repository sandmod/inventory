using System.Collections.Generic;
using Sandmod.Inventory.Container;

namespace Sandmod.Inventory.Item.Asset;

public interface IContainerItemAsset : IItemAsset
{
    IReadOnlyCollection<IContainerSetting> ContainerSettings { get; set; }
}