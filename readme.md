# Ogg Decoder

This is an attempt at creating a OGG decoder in .NET

Warning : The source code is currently non-functional and may change drastically, this is a work-in-progress.
The name is also no final, I just took the first word I could think of. So, don't add this as a dependency to anything, because it _will_ break.

Use at your own peril

## Features

 - [ ] Actually being able to subscribe to streams
 - [ ] Support encoding as well
 - [ ] Use `System.IO.Pipes` instead of streams directly
 - [ ] Build for dependent third-party modules (`libogg`)
 - [ ] Add more checks for the native interop.. Feels flaky

## What is Ogg?

Ogg is a media container format. It encodes multiple streams in interleave that you can consume with synchronization. The things normal people stuff into `ogg` files are audio, video and text (like subtitles). An Ogg stream may also contain other things like album art or insensitive jokes.

## How to build

Requirements :
 - .NET 8
 - dotnet SDK

Just build it. `dotnet build .` you know the drill.

It has no external build-dependencies (as of right now)

## How to run

You need `libogg`. Either installed system-wide, or deployed alongside the binary. This library uses dynamic linking to link with libogg at startup.


## Why?

Well, maybe it's interesting to some.. It's also part of some more stuff I want to make.

I've always been interested in audio and video encoding/decoding, but I've never actually looked into any of the details.

## How to use this to render a video?

I don't know, yet

## How to use this to play audio?

1. Use a audio library of your choice
2. Fetch the streams from the Ogg
3. Read from the audio stream into a buffer
4. Pass the buffer through an audio decoding library
5. Resample the output into the target audio format (source and target may differ)
6. Send the result to the audio library

## Future plans?

Get it to work, so I can decode some audio. Then maybe encode. Then we'll see...
