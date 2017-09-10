using System;

namespace Bricksharp.Firmware.Display
{
    public class Bitmap
    {
        public uint Width { get; }
        public uint Height { get; }

        private const int PixelsPerWord = 32;

        private static uint MagicCodeBitmap = 0x4D544942;

        private readonly uint[] data;
        private readonly int dataOffset;

        public Bitmap(uint width, uint height)
        {
            Width = width;
            Height = height;
            data = new uint[(width * height + PixelsPerWord - 1) / PixelsPerWord];
            dataOffset = 0;
        }

        public Bitmap(uint[] data)
        {
            if (data[0] != MagicCodeBitmap)
                throw new ArgumentException("Invalid value in bitmap data");

            Width = data[1];
            Height = data[2];
            this.data = data;
            dataOffset = 3;
        }

        public BitStreamer GetStream()
        {
            return new BitStreamer(data, dataOffset);
        }

        public static Bitmap FromResouce(System.Reflection.Assembly asm, string resourceName)
        {
            var stream = asm.GetManifestResourceStream(resourceName);
            var bytedata = new byte[stream.Length];
            stream.Read(bytedata, 0, (int)stream.Length);
            var data = new uint[stream.Length / 4];
            for (var i = 0; i != stream.Length / 4; ++i)
                data[i] = BitConverter.ToUInt32(bytedata, i * 4);
            return new Bitmap(data);
        }
    }
}