#nullable enable
using System.IO;
using Sandmod.Core.Network;

namespace Sandmod.Inventory.Item.Context;

public class ItemContext : IItemContext
{
    public ulong  NetworkIdent { get; set; }
    
    private bool _internalIsDirty;
    public bool IsDirty
    {
        get => _internalIsDirty;
        set
        {
            _internalIsDirty = value;
            if (_internalIsDirty)
            {
                OnMarkedDirty?.Invoke();
            }
        }
    }

    public event INetworkSerializable.MarkedDirty? OnMarkedDirty;

    public virtual void NetWrite(BinaryWriter writer)
    {
    }

    public virtual void NetRead(BinaryReader reader)
    {
    }
}