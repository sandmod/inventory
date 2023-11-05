using System.IO;
using Sandbox;
using Sandmod.Core.Network;

namespace Sandmod.Inventory.Container;

public class ContainerSettings : IContainerSettings
{
    public int Size { get; set; }
    public ulong NetworkIdent { get; set; }
    
    protected bool InternalDirty;

    public virtual bool IsDirty
    {
        get => InternalDirty;
        set
        {
            InternalDirty = value;
            if (InternalDirty)
            {
                OnMarkedDirty?.Invoke();
            }
        }
    }

    public event INetworkSerializable.MarkedDirty OnMarkedDirty;

    public ContainerSettings()
    {
    }

    public ContainerSettings(int size)
    {
        Size = size;
    }

    
    public void NetWrite(BinaryWriter writer)
    {
        writer.Write(Size);
    }

    public void NetRead(BinaryReader reader)
    {
        Size = reader.ReadInt32();
    }
}