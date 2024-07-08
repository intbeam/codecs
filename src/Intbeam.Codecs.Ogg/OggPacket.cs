using System;
using System.Runtime.InteropServices;

namespace Intbeam.Codecs.Ogg;

/// <summary>
/// This structure encapsulates the data and metadata for a single Ogg packet.
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
internal unsafe struct OggPacket
{
    public OggPacket()
    {
        
    }
    public byte* Packet;
    public long Bytes;
    public long BeginningOfStream;
    public long EndOfStream;
    public Int64 GranulePosition;
    public Int64 PacketNumber;
}