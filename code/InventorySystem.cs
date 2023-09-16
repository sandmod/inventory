using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.Diagnostics;
using Sandmod.Core.Logger;
using Sandmod.Core.Provider;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;
using Sandmod.Inventory.Provider;

namespace Sandmod.Inventory;

public static class InventorySystem
{
    internal static readonly Logger Log = new SandmodLogger("Inventory");

    private static List<IItemAssetProvider> _assetProviders;
    private static List<IItemAssetModifier> _assetModifiers;
    private static List<IItemAsset> _assets;

    public static IReadOnlyCollection<IItemAsset> Assets => _assets.AsReadOnly();

    private static IItemProvider _itemProvider;

    [GameEvent.Entity.PostSpawn]
    [Event.Hotload]
    private static void Init()
    {
        _assetProviders = new List<IItemAssetProvider>(ProviderFactory.Provide<IItemAssetProvider>());
        _assetModifiers = new List<IItemAssetModifier>(ProviderFactory.Provide<IItemAssetModifier>());
        if (Game.IsServer)
        {
            _itemProvider = ProviderFactory.ProvideSingle<IItemProvider>();
        }

        LoadAssets();
    }

    private static void LoadAssets()
    {
        IReadOnlyCollection<IItemAsset> assets = _assetProviders.SelectMany(provider => provider.Provide()).ToList()
            .AsReadOnly();
        foreach (var assetModifier in _assetModifiers)
        {
            assets = assetModifier.Modify(assets);
        }

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

    public static IItem<TAsset> CreateItem<TAsset>(TAsset asset) where TAsset : class, IItemAsset
    {
        Game.AssertServer();
        return _itemProvider.Provide(asset);
    }

    public static T GetAsset<T>(string id) where T : class, IItemAsset
    {
        var asset = Assets.First(asset => asset.Id == id);
        if (!asset.GetType().IsAssignableTo(typeof(T))) return null;
        return asset as T;
    }
}