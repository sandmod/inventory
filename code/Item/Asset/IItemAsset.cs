using System;
using Sandbox;

namespace Sandmod.Inventory.Item.Asset;

public interface IItemAsset
{
    string Id { get; set; }

    string Name { get; set; }

    TypeDescription ItemType { get; set; }
}