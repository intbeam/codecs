using System.Runtime.InteropServices;

namespace Intbeam.Codecs.Ogg;

/// <summary>
/// The oggpack_buffer struct is used with libogg's bitpacking functions. You should never need to directly access anything in this structure. 
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct OggPackBuffer
{
    public OggPackBuffer()
    {
        
    }
    
    public long EndByte;
    public int EndBit;

    /// <summary>
    /// Pointer to data being manipulated
    /// </summary>
    public byte* Buffer;

    /// <summary>
    /// Location pointer for where data was read
    /// </summary>
    public byte* Pointer;

    /// <summary>
    /// Size of buffer
    /// </summary>
    public long BufferSize;
}