using System;
using System.Runtime.InteropServices;

namespace Intbeam.Codecs.Ogg;

public unsafe class SynchronizationState : IDisposable
{
    public SynchronizationState()
    {
        _syncState = (OggSyncState*)Marshal.AllocHGlobal(sizeof(OggSyncState));
        if (NativeApi.SyncInit(_syncState) != 0)
        {
            Dispose();
            throw new InvalidOperationException("Could not initialize synchronization state");
        }
    }

    private OggSyncState* _syncState;

    public Span<byte> Data => new Span<byte>(_syncState->Data, _syncState->Storage);
    public int HeaderLength => _syncState->HeaderBytes;
    public int Fill => _syncState->Fill;
    public int Returned => _syncState->Returned;
    public int Unsynced => _syncState->Unsynced;
    public int BodyBytes => _syncState->BodyBytes;

    internal OggSyncState* GetAddr() => _syncState;

    public void Dispose()
    {
        if (_syncState == null)
            return;
        
        Marshal.FreeHGlobal((IntPtr)_syncState);
        _syncState = null;
    }
}