using System;
using System.Runtime.InteropServices;

namespace Intbeam.Codecs.Ogg;

/// <summary>
/// This structure contains current encode/decode data for a logical bitstream.
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
internal unsafe struct OggStreamState
{
    public OggStreamState()
    {
        
    }
    
    public byte* BodyData;
    public long BodyStorage;
    public long BodyReturned;
    public int* LacingValues;
    public Int64* GranulePosValues;
    public long LacingStorage;
    public long LacingFill;
    public long LacingPacket;
    public long LacingReturned;
    public fixed byte Header[258];
    public int HeaderFill;
    public int EndOfStream;
    public int BeginningOfStream;
    public long SerialNumber;
    public Int64 PacketNumber;
    public Int64 GranulePosition;
}