using Sandbox;

namespace Sandmod.Inventory.Item.Asset;

internal sealed class DefaultItemAsset : IItemAsset
{
    public string Id { get; set; }
    public string Name { get; set; }
    public TypeDescription EntityType { get; }
}