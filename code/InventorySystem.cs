using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.Diagnostics;
using Sandmod.Core.Logger;
using Sandmod.Core.Provider;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Item.Component;
using Sandmod.Inventory.Provider;

namespace Sandmod.Inventory;

public static partial class InventorySystem
{
    internal static readonly Logger Log = new SandmodLogger("Inventory");

    private static List<IItemAssetProvider> _assetProviders = new();
    private static List<IItemAssetModifier> _assetModifiers = new();
    private static List<IItemAssetFilter> _assetFilters = new();
    private static List<IItemAsset> _assets = new();

    public static IReadOnlyCollection<IItemAsset> Assets => _assets.AsReadOnly();

    private static IItemProvider _itemProvider;
    private static List<IItemComponentProvider> _itemComponentProviders = new();
    private static IItemEntityProvider _itemEntityProvider;

    [GameEvent.Entity.PostSpawn]
    [Event.Hotload]
    private static void Init()
    {
        _assetProviders = new List<IItemAssetProvider>(ProviderFactory.Provide<IItemAssetProvider>());
        _assetModifiers = new List<IItemAssetModifier>(ProviderFactory.Provide<IItemAssetModifier>());
        _assetFilters = new List<IItemAssetFilter>(ProviderFactory.Provide<IItemAssetFilter>());

        _itemProvider = ProviderFactory.ProvideSingle<IItemProvider>();
        _itemComponentProviders = new List<IItemComponentProvider>(ProviderFactory.Provide<IItemComponentProvider>());
        _itemEntityProvider = ProviderFactory.ProvideSingle<IItemEntityProvider>();

        LoadAssets();
    }

    [ClientRpc]
    public static void InitClient()
    {
        if (!Game.IsClient) return;
        Init();
    }

    [GameEvent.Server.ClientJoined]
    private static void InitClient(ClientJoinedEvent joinedEvent)
    {
        InitClient(To.Single(joinedEvent.Client));
    }

    private static void LoadAssets()
    {
        IReadOnlyCollection<IItemAsset> assets = _assetProviders.SelectMany(provider => provider.Provide()).ToList()
            .AsReadOnly();
        foreach (var asset in assets)
        {
            foreach (var assetModifier in _assetModifiers)
            {
                assetModifier.Modify(asset);
            }
        }

        assets = assets.Where(asset => _assetFilters.All(assetFilter => assetFilter.Filter(asset))).ToList();

        _assets = assets.GroupBy(asset => asset.Id).Select(group =>
        {
            var result = group.First();
            if (group.Count() > 1)
            {
                Log.Warning(
                    $"{group.Count()} item assets have the same id: {result.Id}, using only the first found: {result.GetType().FullName}");
            }

            return result;
        }).ToList();
    }

    public static T GetAsset<T>(string id) where T : IItemAsset
    {
        var asset = Assets.FirstOrDefault(asset => asset.Id == id);
        if (asset == null) return default;
        if (!asset.GetType().IsAssignableTo(typeof(T))) return default;
        return (T) asset;
    }

    public static IItem CreateItem(IItemAsset asset)
    {
        return _itemProvider.Provide(asset);
    }

    public static IReadOnlyCollection<IItemComponent> ProvideItemComponents(IItemAsset asset)
    {
        return _itemComponentProviders.SelectMany(provider => provider.Provide(asset)).ToList();
    }

    public static IEntity SpawnItem(IItem item)
    {
        Game.AssertServer();
        return _itemEntityProvider.Provide(item);
    }
}