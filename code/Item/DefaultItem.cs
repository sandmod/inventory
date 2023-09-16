using Sandbox;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Item;

internal sealed partial class DefaultItem<TAsset> : BaseNetworkable, IItem<TAsset> where TAsset : class, IItemAsset
{
    [Net, Change] private string AssetId { get; set; }

    public TAsset Asset { get; private set; }

    [Net] public IEntity Entity { get; private set; }

    public IItemContainer<TAsset> Container { get; }

    public DefaultItem()
    {
    }

    public DefaultItem(TAsset asset)
    {
        Game.AssertServer();
        Asset = asset;
        AssetId = Asset.Id;
    }

    public void OnAssetIdChanged(string oldId, string newId)
    {
        Asset = InventorySystem.GetAsset<TAsset>(newId);
        if (Asset == null)
        {
            InventorySystem.Log.Error($"Asset {newId} doesn't exist on client");
        }
    }
}