using System;
using System.Runtime.InteropServices;

namespace Intbeam.Codecs.Ogg;

internal static unsafe partial class NativeApi
{
    private const string LibraryName = "libogg";

    [LibraryImport(LibraryName, EntryPoint = "ogg_stream_packetin")]
    public static partial int ogg_stream_packetin(OggStreamState *state, OggPacket* packet);
    [LibraryImport(LibraryName, EntryPoint = "ogg_stream_pageout")]
    public static partial int ogg_stream_pageout(OggStreamState *state, OggPage* page);
    [LibraryImport(LibraryName, EntryPoint = "ogg_stream_pageout_fill")]
    public static partial int ogg_stream_pageout_fill(OggStreamState *state, OggPage *page, int fillBytes);
    [LibraryImport(LibraryName, EntryPoint = "ogg_stream_flush")]
    public static partial int ogg_stream_flush(OggStreamState *state, OggPage *page);
    [LibraryImport(LibraryName, EntryPoint = "ogg_stream_flush_fill")]
    public static partial int ogg_stream_flush_fill(OggStreamState *state, OggPage *page, int fillBytes);

    
    [LibraryImport(LibraryName, EntryPoint = "oggpack_writeinit")]
    public static partial void oggpack_writeinit(OggPackBuffer *buffer);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_writecheck")]
    public static partial int oggpack_writecheck(OggPackBuffer *buffer);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_reset")]
    public static partial void oggpack_reset(OggPackBuffer *buffer);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_writetrunc")]
    public static partial void oggpack_writetrunc(OggPackBuffer *buffer, long bits);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_writealign")]
    public static partial void oggpack_writealign(OggPackBuffer *buffer);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_writecopy")]
    public static partial void oggpack_writecopy(OggPackBuffer *buffer, void *source, long bits);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_writeclear")]
    public static partial void oggpack_writeclear(OggPackBuffer *buffer);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_readinit")]
    public static partial void oggpack_readinit(OggPackBuffer *buffer, byte *buf, int bytes);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_write")]
    public static partial void oggpack_write(OggPackBuffer *buffer, ulong value, int bits);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_look")]
    public static partial long oggpack_look(OggPackBuffer *buffer, int bits);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_look1")]
    public static partial long oggpack_look1(OggPackBuffer *buffer);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_adv")]
    public static partial void oggpack_adv(OggPackBuffer *buffer, int bits);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_adv1")]
    public static partial void oggpack_adv1(OggPackBuffer *buffer);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_read")]
    public static partial long oggpack_read(OggPackBuffer *buffer, int bits);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_read1")]
    public static partial long oggpack_read1(OggPackBuffer *buffer);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_bytes")]
    public static partial long oggpack_bytes(OggPackBuffer *buffer);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_bits")]
    public static partial long oggpack_bits(OggPackBuffer *buffer);
    [LibraryImport(LibraryName, EntryPoint = "oggpack_get_buffer")]
    public static partial byte* oggpack_get_buffer(OggPackBuffer *buffer);
    
    [LibraryImport(LibraryName, EntryPoint = "ogg_sync_init")]
    public static partial int SyncInit(OggSyncState* state);
    [LibraryImport(LibraryName, EntryPoint = "ogg_sync_check")]
    public static partial int ogg_sync_check(OggSyncState* state);
    [LibraryImport(LibraryName, EntryPoint = "ogg_sync_clear")]
    public static partial int ogg_sync_clear(OggSyncState* state);
    [LibraryImport(LibraryName, EntryPoint = "ogg_sync_destroy")]
    public static partial int ogg_sync_destroy(OggSyncState* state);
    [LibraryImport(LibraryName, EntryPoint = "ogg_sync_reset")]
    public static partial int ogg_sync_reset(OggSyncState* state);
    [LibraryImport(LibraryName, EntryPoint = "ogg_sync_buffer")]
    public static partial byte* ogg_sync_buffer(OggSyncState* state, long size);
    [LibraryImport(LibraryName, EntryPoint = "ogg_sync_wrote")]
    public static partial int ogg_sync_wrote(OggSyncState* state, long bytes);
    [LibraryImport(LibraryName, EntryPoint = "ogg_sync_pageseek")]
    public static partial int ogg_sync_pageseek(OggSyncState* state, OggPage* page);
    [LibraryImport(LibraryName, EntryPoint = "ogg_sync_pageout")]
    public static partial int ogg_sync_pageout(OggSyncState* state, OggPage* page);
    [LibraryImport(LibraryName, EntryPoint = "ogg_stream_pagein")]
    public static partial int ogg_stream_pagein(OggStreamState* state, OggPage *page);
    [LibraryImport(LibraryName, EntryPoint = "ogg_stream_packetout")]
    public static partial int ogg_stream_packetout(OggStreamState* state, OggPacket *packet);
    [LibraryImport(LibraryName, EntryPoint = "ogg_stream_packetpeek")]
    public static partial int ogg_stream_packetpeek(OggStreamState* state, OggPacket *packet);

    [LibraryImport(LibraryName, EntryPoint = "ogg_stream_init")]
    public static partial int ogg_stream_init(OggStreamState* state, int serialNumber);
    [LibraryImport(LibraryName, EntryPoint = "ogg_stream_check", StringMarshalling = StringMarshalling.Utf8)]
    public static partial int ogg_stream_check(OggStreamState* state);
    [LibraryImport(LibraryName, EntryPoint = "ogg_stream_clear")]
    public static partial int ogg_stream_clear(OggStreamState* state);
    [LibraryImport(LibraryName, EntryPoint = "ogg_stream_reset")]
    public static partial int ogg_stream_reset(OggStreamState* state);
    [LibraryImport(LibraryName, EntryPoint = "ogg_stream_reset_serialno")]
    public static partial int ogg_stream_reset_serialno(OggStreamState* state, int serialNumber);
    [LibraryImport(LibraryName, EntryPoint = "ogg_stream_destroy")]
    public static partial int ogg_stream_destroy(OggStreamState* state);
    [LibraryImport(LibraryName, EntryPoint = "ogg_page_version")]
    public static partial int ogg_page_version(OggPage* page);
    [LibraryImport(LibraryName, EntryPoint = "ogg_page_continued")]
    public static partial int ogg_page_continued(OggPage* page);
    [LibraryImport(LibraryName, EntryPoint = "ogg_page_packets")]
    public static partial int ogg_page_packets(OggPage* page);
    [LibraryImport(LibraryName, EntryPoint = "ogg_page_bos")]
    public static partial int ogg_page_bos(OggPage* page);
    [LibraryImport(LibraryName, EntryPoint = "ogg_page_eos")]
    public static partial int ogg_page_eos(OggPage* page);
    [LibraryImport(LibraryName, EntryPoint = "ogg_page_granulepos")]
    public static partial Int64 ogg_page_granulepos(OggPage* page);
    [LibraryImport(LibraryName, EntryPoint = "ogg_page_serialno")]
    public static partial int ogg_page_serialno(OggPage* page);
    [LibraryImport(LibraryName, EntryPoint = "ogg_page_pageno")]
    public static partial int ogg_page_pageno(OggPage* page);
    [LibraryImport(LibraryName, EntryPoint = "ogg_packet_clear")]
    public static partial void ogg_packet_clear(OggPacket* packet);
    [LibraryImport(LibraryName, EntryPoint = "ogg_page_checksum_set")]
    public static partial int ogg_page_checksum_set(OggPage* page);
}