namespace Sandmod.Inventory.Container.Extensions;

public static class ContainerExtensions
{
    public static int MaxSize(this IItemContainer self)
    {
        return self.Settings.Size;
    }
}