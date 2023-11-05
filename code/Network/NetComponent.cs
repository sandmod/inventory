using Sandmod.Core.Network;
using Sandmod.Inventory.Item.Component;

namespace Sandmod.Inventory.Network;

public sealed class NetComponent : NetWrapper<IItemComponent>
{
    public static readonly NetRegistry<IItemComponent> Registry = new(
        (writer, component) => writer.Write(component.GetType()),
        (reader, ident) =>
        {
            var type = reader.ReadType();

            return Registry.TryGetValue(ident, out var foundComponent)
                ? foundComponent
                : TypeLibrary.Create<IItemComponent>(type);
        });

    public NetComponent()
    {
    }

    public NetComponent(IItemComponent component) : base(component)
    {
    }

    protected override NetRegistry<IItemComponent> InternalRegistry => Registry;
}