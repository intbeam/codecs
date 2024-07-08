using System.Runtime.InteropServices;

namespace Intbeam.Codecs.Ogg;

/// <summary>
/// Contains bitstream synchronization information.
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
internal unsafe struct OggSyncState
{
    public OggSyncState()
    {
        
    }
    
    public byte* Data;
    public int Storage;
    public int Fill;
    public int Returned;
    public int Unsynced;
    public int HeaderBytes;
    public int BodyBytes;
}