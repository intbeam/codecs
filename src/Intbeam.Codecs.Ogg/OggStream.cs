using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Intbeam.Codecs.Ogg;

public class OggStreamCollection
{
    
}

public unsafe class OggStream : Stream
{
    private readonly long _serialNumber;
    
    private readonly OggDecoder _decoder;
    private readonly OggStreamState* _streamState;
    private readonly MemoryStream _memoryStream;

    public long SerialNumber => _serialNumber;
    
    
    internal OggStream(OggDecoder decoder, OggStreamState* streamState)
    {
        _decoder = decoder;
        _streamState = streamState;
        _serialNumber = streamState->SerialNumber;
        _memoryStream = new MemoryStream();
    }

    internal OggStreamState* GetAddr() => _streamState;

    protected override void Dispose(bool disposing)
    {
        _decoder.DestroyStreamState((IntPtr)_streamState);
    }
    
    internal void Push(Packet packet)
    {
        var npos = _memoryStream.Position;
        
        _memoryStream.Write(packet.Data);

        _memoryStream.Position = npos;
    }

    public override void Flush()
    {
        throw new NotImplementedException();
    }
    

    public override int Read(byte[] buffer, int offset, int count)
    {
        return _memoryStream.Read(buffer, offset, count);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotImplementedException();
    }

    public override void SetLength(long value)
    {
        throw new NotImplementedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotImplementedException();
    }

    public override bool CanRead => true;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override long Length => _memoryStream.Length;

    public override long Position
    {
        get => _memoryStream.Length;
        set => throw new NotImplementedException();
    } 
}