using System;
using System.IO;

namespace Bricksharp.Firmware.Lcds
{
    public class Lcd
    {
        public const int Width = 178;
        public const int Height = 128;

        private const int LineLength = 24;

        private static readonly byte[] buffer = new byte[LineLength * Height];
        private static readonly FileInfo frameBuffer = new FileInfo("/dev/fb0");
        private static readonly FileStream stream = new FileStream(frameBuffer.FullName, FileMode.Open, FileAccess.ReadWrite);

        private static void Flush()
        {
            stream.Position = 0;
            stream.Write(buffer, 0, buffer.Length);
        }

        private static int GetOffsetAndMask(int x, int y, out byte mask)
        {
            var offset = y * LineLength;
            var lineOffset = x / 8;
            var bitOffset = x % 8;
            offset += lineOffset;
            mask = (byte)(1 << bitOffset);
            return offset;
        }

        public static void Clear()
        {
            Array.Clear(buffer, 0, buffer.Length);
            Flush();
        }

        public static bool GetPixel(int x, int y)
        {
            byte mask;
            var offset = GetOffsetAndMask(x, y, out mask);
            var value = buffer[offset];
            var bit = value & mask;
            return bit != 0;
        }

        public static void SetPixel(int x, int y, bool on)
        {
            byte mask;
            var offset = GetOffsetAndMask(x, y, out mask);
            var value = buffer[offset];
            if (on)
                value = (byte)(value | mask);
            else
                value = (byte)(value & ~mask);
            buffer[offset] = value;
            Flush();
        }
    }
}