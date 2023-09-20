using Sandbox;

namespace Sandmod.Inventory.Item.Asset;

internal sealed class ItemAsset : IItemAsset
{
    public string Id { get; set; }
    public string Name { get; set; }

    public TypeDescription ItemType { get; set; }
}