// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using Intbeam.Codecs.Ogg;

using (var instream = new FileStream("intbeam-test.ogg", FileMode.Open, FileAccess.Read))
{
    using (var decoder = new OggDecoder(instream))
    {

        decoder.OnStream(s =>
        {
            Console.WriteLine(s.SerialNumber);
        });

        decoder.Read();



    }
}