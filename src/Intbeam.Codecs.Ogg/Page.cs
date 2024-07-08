using System;

namespace Intbeam.Codecs.Ogg;

public unsafe class Page : IDisposable
{

    private readonly OggPage* _page;
    private readonly OggDecoder _decoder;
    internal Page(OggPage* page, OggDecoder decoder)
    {
        _decoder = decoder;
        _page = page;
    }

    public Span<byte> Body => new Span<byte>(_page->Body, (int)_page->BodyLength);
    public Span<byte> Header => new Span<byte>(_page->Header, (int)_page->HeaderLength);

    internal OggPage* GetAddr() => _page;

    /// <summary>
    /// Gets the serial number for this page
    /// </summary>
    public int SerialNumber => NativeApi.ogg_page_serialno(_page);

    /// <summary>
    /// Gets the version of this page. Should always be zero
    /// </summary>
    public int Version => NativeApi.ogg_page_version(_page);

    /// <summary>
    /// Gets whether this page contains packed data continued from the last page
    /// </summary>
    public bool Continued => NativeApi.ogg_page_continued(_page) == 1;

    /// <summary>
    /// Returns the exact granular position of the packet data contained at the end of this page.
    /// This is useful for tracking location when seeking or decoding.
    /// For example, in audio codecs this position is the pcm sample number and in video this is the frame number. 
    /// </summary>
    public long GranularityPosition => NativeApi.ogg_page_granulepos(_page);


    /// <summary>
    /// Gets the sequential page number
    /// This is useful for ordering pages or determining when pages have been lost
    /// </summary>
    public int PageNumber => NativeApi.ogg_page_pageno(_page);


    /// <summary>
    /// Returns the number of packets that are completed on this page. If the leading packet is begun on a previous page, but ends on this page, it's counted.
    /// </summary>
    public int PacketCount => NativeApi.ogg_page_packets(_page);

    /// <summary>
    /// Indicates whether this page is at the beginning of the logical bitstream
    /// </summary>
    public bool BeginningOfStream => NativeApi.ogg_page_bos(_page) > 0;

    /// <summary>
    /// Indicates whether this page is at the end of the logical bitstream
    /// </summary>
    public bool EndOfStream => NativeApi.ogg_page_eos(_page) > 0;
    
    
    public void Dispose()
    {
        _decoder.DeletePage(this);
    }
}