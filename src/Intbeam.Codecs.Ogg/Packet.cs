using System;
using System.Buffers;

namespace Intbeam.Codecs.Ogg;

public unsafe class Packet : IDisposable
{
    private OggPacket* _packet;
    private readonly OggDecoder _decoder;
    
    internal Packet(OggPacket* packet, OggDecoder decoder)
    {
        _packet = packet;
        _decoder = decoder;
    }

    public Span<byte> Data => _packet == null ? throw new InvalidCastException() : new Span<byte>(_packet->Packet, (int)_packet->Bytes);
    public bool BeginningOfStream => _packet == null ? throw new InvalidCastException() : _packet->BeginningOfStream == 0;
    public long Length => _packet == null ? throw new InvalidCastException() : _packet->Bytes;
    public long PacketNumber => _packet == null ? throw new InvalidCastException() : _packet->PacketNumber;
    public long GranularPosition => _packet == null ? throw new InvalidCastException() : _packet->GranulePosition;
    public bool EndOfStream => _packet == null ? throw new InvalidCastException() : _packet->EndOfStream == 0;

    internal OggPacket* GetAddr() => _packet;

    public void Dispose()
    {
        _decoder.DeletePacket(this);
        _packet = (OggPacket*)IntPtr.Zero;
    }

}