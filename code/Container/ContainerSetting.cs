namespace Sandmod.Inventory.Container;

public class ContainerSetting : IContainerSetting
{
    public int Size { get; set; }

    public ContainerSetting()
    {
    }

    public ContainerSetting(int size)
    {
        Size = size;
    }
}