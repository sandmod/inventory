using Sandmod.Core.Provider;
using Sandmod.Inventory.Item.Context;

namespace Sandmod.Inventory.Provider;

[DefaultProvider(ProviderPriority.Internal)]
internal sealed class DefaultItemContextProvider : IItemContextProvider
{
    public IItemContext Provide()
    {
        return new ItemContext();
    }
}