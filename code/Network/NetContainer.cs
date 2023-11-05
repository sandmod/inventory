using Sandmod.Core.Network;
using Sandmod.Inventory.Container;

namespace Sandmod.Inventory.Network;

public sealed class NetContainer : NetWrapper<IItemContainer>
{
    public static readonly NetRegistry<IItemContainer> Registry = new(
        (writer, container) => writer.Write(container.GetType()),
        (reader, ident) =>
        {
            var type = reader.ReadType();

            return Registry.TryGetValue(ident, out var foundContainer)
                ? foundContainer
                : TypeLibrary.Create<IItemContainer>(type);
        });

    protected override NetRegistry<IItemContainer> InternalRegistry => Registry;

    public NetContainer()
    {
    }

    public NetContainer(IItemContainer container) : base(container)
    {
    }
}