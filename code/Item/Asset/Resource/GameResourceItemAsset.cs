using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Sandbox;

namespace Sandmod.Inventory.Item.Asset.Resource;

// More of a technical playground than an actual user friendly GameResource
// A custom GameResource editor might be useful
[GameResource("Item Definition", "item", "Item definition", Icon = "cases", Category = "Item")]
internal sealed class GameResourceItemAsset : GameResource
{
    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
    {
        ReadCommentHandling = JsonCommentHandling.Skip,
        PropertyNameCaseInsensitive = true,
        AllowTrailingCommas = true
    };

    public Dictionary<string, Dictionary<string, string>> Test { get; set; }

    public string ItemAssetJSON { get; set; }

    [HideInEditor] [JsonIgnore] public IItemAsset ItemAsset { get; private set; }

    protected override void PostLoad()
    {
        Parse();
    }

    protected override void PostReload()
    {
        Parse();
    }

    private void Parse()
    {
        var interfaceTypes = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(ItemAssetJSON, Options);
        var interfaces = interfaceTypes.ToDictionary(entry => TypeLibrary.GetType(entry.Key), entry => entry.Value);
        var type = GetTypeForInterfaces(interfaces.Keys);
        var instance = TypeLibrary.Create<IItemAsset>(type.TargetType);

        foreach (var interfaceConfig in interfaces)
        {
            ApplyInterfaceValues(instance, interfaceConfig.Key, interfaceConfig.Value);
        }

        ItemAsset = instance;
    }

    private TypeDescription GetTypeForInterfaces(IReadOnlyCollection<TypeDescription> interfaces)
    {
        if (interfaces.Count <= 0) throw new Exception("Must at least have one interface");
        var types = TypeLibrary.GetTypes(interfaces.First().TargetType).Where(type => type.IsClass && !type.IsAbstract);
        foreach (var interfaceType in interfaces.Skip(1))
        {
            types = types.Where(type => type.TargetType.IsAssignableTo(interfaceType.TargetType));
            if (types.Count() <= 0) throw new Exception("No types exists with all used interfaces");
        }

        return types.FirstOrDefault();
    }

    private void ApplyInterfaceValues(IItemAsset instance, TypeDescription type, JsonElement json)
    {
        var properties = type.Properties;
        foreach (var property in properties)
        {
            if (!property.CanWrite) continue;
            if (!json.TryGetProperty(property.Name, out JsonElement jsonValue)) continue;
            var value = jsonValue.Deserialize(property.PropertyType, Options);
            property.SetValue(instance, value);
        }
    }
}