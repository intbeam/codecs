using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Intbeam.Codecs.Ogg;

public unsafe class OggDecoder : IDisposable
{
    private readonly Stream _stream;
    private readonly SynchronizationState _syncState;
    private readonly HashSet<IntPtr> _pages = [];
    private readonly HashSet<IntPtr> _streamStates = [];
    private readonly HashSet<IntPtr> _packets = [];
    private readonly Dictionary<int, OggStream> _streams = [];
    private  Action<OggStream> _onStream;

    private Page CreatePage()
    {
        var page = (OggPage*)Marshal.AllocHGlobal(sizeof(OggPage));
        page->Body = null;
        page->Header = null;
        page->BodyLength = 0L;
        page->HeaderLength = 0L;
        
        _pages.Add((IntPtr)page);
        
        return new Page(page, this);
    }

    public OggDecoder OnStream(Action<OggStream> newStream)
    {
        _onStream = newStream;

        return this;
    }

    private OggStreamState* CreateStreamState(int serialNumber)
    {
        var state = (OggStreamState*)Marshal.AllocHGlobal(sizeof(OggStreamState));
        NativeApi.ogg_stream_init(state, serialNumber);

        _streamStates.Add((IntPtr)state);

        return state;
    }

    internal void DestroyStreamState(IntPtr state)
    {
        NativeApi.ogg_stream_destroy((OggStreamState*)state);
        Marshal.FreeHGlobal((IntPtr)state);
        _streamStates.Remove((IntPtr)state);
    }

    internal void DestroyPacket(IntPtr packet)
    {
        NativeApi.ogg_packet_clear((OggPacket*)packet);
        Marshal.FreeHGlobal(packet);
    }

    internal Packet CreatePacket()
    {
        var packet = (OggPacket *)Marshal.AllocHGlobal(sizeof(OggPacket));
        packet->Packet = null;
        packet->PacketNumber = 0;
        packet->EndOfStream = 0;
        packet->GranulePosition = 0;
        packet->EndOfStream = 0;
        packet->BeginningOfStream = 0;
        packet->Bytes = 0;
        
        
        return new Packet(packet, this);
    }

    private OggStream InitializeStream(Page page)
    {
        var sno = page.SerialNumber;
        if (!page.BeginningOfStream) return _streams[sno];
        
        var state = CreateStreamState(sno);
            
        var os = new OggStream(this, state); 
        _streams.Add(sno, os);

        return os;
    }

    private OggStream GetStream(Page page, out bool newStream)
    {
        if (_streams.TryGetValue(page.SerialNumber, out var stream))
        {
            newStream = false;
            return stream;
        }

        stream = InitializeStream(page);

        newStream = true;

        return stream;


    }
    

    internal void DeletePage(Page page)
    {
        Marshal.FreeHGlobal((IntPtr)page.GetAddr());
        
        var p = _pages.Remove((IntPtr)page.GetAddr());
    }

    internal void DeletePacket(Packet packet)
    {
        Marshal.FreeHGlobal((IntPtr)packet.GetAddr());
        _packets.Remove((IntPtr)packet.GetAddr());
    }
    
    private OggDecoder()
    {
        _syncState = new SynchronizationState();
        _stream = Stream.Null;
        _onStream = (s) => { };
    }
    
    public OggDecoder(Stream input) : this()
    {
        _stream = input;
    }

    internal bool ReadNextPacket()
    {
        using var packet = CreatePacket();
        while (true)
        {
            var page = ReadPage();

            var stream = GetStream(page, out var newStream);

            var ret = NativeApi.ogg_stream_packetout(stream.GetAddr(), packet.GetAddr());

            
            if (ret == 0 && !page.EndOfStream)
                continue; // page is not complete
            if (ret == -1)
                throw new InvalidOperationException(); // missed something
            
            stream.Push(packet);

            if (newStream)
                _onStream(stream);

            
            NativeApi.ogg_packet_clear(packet.GetAddr());
            break;

        }

        return true;

    }

    public bool Read()
    {
        return ReadNextPacket();
    }
    
    private Page ReadPage()
    {
        var page = CreatePage();

        try
        {
            while (NativeApi.ogg_sync_pageout(_syncState.GetAddr(), page.GetAddr()) != 1)
            {
                var buffer = NativeApi.ogg_sync_buffer(_syncState.GetAddr(), 4096);
                var span = new Span<byte>(buffer, 4096);
                if (buffer == null)
                    throw new InvalidOperationException();

                var read = _stream.Read(span);
                if (read == 0)
                    break;

                var ret = NativeApi.ogg_sync_wrote(_syncState.GetAddr(), read);

                if (ret == -1)
                    throw new InvalidOperationException();
            }

            return page;
        }
        catch
        {
            page.Dispose();
            throw;
        }
    }

    public void Dispose()
    {
        _syncState.Dispose();

        foreach (var streamState in _streamStates)
        {
            DestroyStreamState(streamState);
        }
        
        foreach (var page in _pages)
        {
            Marshal.FreeHGlobal(page);
        }
        
        _pages.Clear();

        foreach (var packet in _packets)
        {
            Marshal.FreeHGlobal(packet);
        }
        _packets.Clear();
    }
}