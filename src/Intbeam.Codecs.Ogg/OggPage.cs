using System.Runtime.InteropServices;

namespace Intbeam.Codecs.Ogg;

/// <summary>
/// This structure encapsulates data into one ogg bitstream page.
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
internal unsafe struct OggPage
{
    public OggPage()
    {
        
    }
    
    public byte* Header;
    public long HeaderLength;
    public byte* Body;
    public long BodyLength;
}