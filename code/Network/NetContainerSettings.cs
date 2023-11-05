using Sandmod.Core.Network;
using Sandmod.Inventory.Container;

namespace Sandmod.Inventory.Network;

public sealed class NetContainerSettings : NetWrapper<IContainerSettings>
{
    public static readonly NetRegistry<IContainerSettings> Registry = new(
        (writer, settings) => writer.Write(settings.GetType()),
        (reader, ident) =>
        {
            var type = reader.ReadType();

            return Registry.TryGetValue(ident, out var foundSettings)
                ? foundSettings
                : TypeLibrary.Create<IContainerSettings>(type);
        });

    protected override NetRegistry<IContainerSettings> InternalRegistry => Registry;

    public NetContainerSettings()
    {
    }

    public NetContainerSettings(IContainerSettings settings) : base(settings)
    {
    }
}