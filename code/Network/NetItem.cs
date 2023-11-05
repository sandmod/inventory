using System;
using Sandmod.Core.Network;
using Sandmod.Inventory.Item;
using Sandmod.Inventory.Item.Asset;

namespace Sandmod.Inventory.Network;

public sealed class NetItem : NetWrapper<IItem>
{
    public static readonly NetRegistry<IItem> Registry = new(
        (writer, item) => writer.Write(item.Asset.Id),
        (reader, ident) =>
        {
            var assetId = reader.ReadString();
            if (Registry.TryGetValue(ident, out var foundItem))
            {
                return foundItem;
            }

            var asset = InventorySystem.GetAsset<IItemAsset>(assetId);
            if (asset == null) throw new Exception($"Asset \"{assetId}\" not existing on client");
            return InventorySystem.CreateItem(asset);
        });

    protected override NetRegistry<IItem> InternalRegistry => Registry;

    public NetItem()
    {
    }

    public NetItem(IItem item) : base(item)
    {
    }
}