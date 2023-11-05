using Sandbox;

namespace Sandmod.Inventory.Item.Asset;

internal class WorldItemAsset : ItemAsset, IWorldItemAsset
{
    public TypeDescription EntityType { get; set; }
}