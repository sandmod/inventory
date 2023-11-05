using Sandbox;

namespace Sandmod.Inventory.Item.Asset;

public interface IWorldItemAsset : IItemAsset
{
    TypeDescription EntityType { get; set; }
}