using System;
using Bricksharp.Firmware.Unix;

namespace Bricksharp.Firmware.Display
{
    public class Lcd
    {
        public const int Width = 178;
        public const int Height = 128;

        private static readonly int bytesPrLine = (Width + 31) / 32 * 4;
        private static readonly int bufferSize = bytesPrLine * Height;
        private static readonly int hwBufferLineSize = bytesPrLine;
        private static readonly int hwBufferSize = hwBufferLineSize * Height;
        private static readonly MemoryArea memory;
        private static readonly byte[] displayBuf = new byte[bufferSize];
        private static readonly byte[] hwBuffer = new byte[hwBufferSize];

        static Lcd()
        {
            var device = new UnixDevice("/dev/fb0");
            memory = device.MMap((uint)hwBufferSize, 0);
            Clear();
            Update();
        }

        public static void Update()
        {
            Update(0);
        }

        public static void Update(int yOffset)
        {
            var byteOffset = yOffset % Height * bytesPrLine;
            Array.Copy(displayBuf, byteOffset, hwBuffer, 0, hwBufferSize - byteOffset);
            Array.Copy(displayBuf, 0, hwBuffer, hwBufferSize - byteOffset, byteOffset);
            memory.Write(0, hwBuffer);
        }

        public static void Clear()
        {
            ClearLines(0, Height);
        }

        public static void ClearLines(int y, int count)
        {
            Array.Clear(displayBuf, bytesPrLine * y, count * bytesPrLine);
        }

        public void DrawBitmap(Bitmap bm, Point p)
        {
            DrawBitmap(bm.GetStream(), p, bm.Width, bm.Height, true);
        }

        public static void DrawBitmap(BitStreamer bs, Point p, uint xSize, uint ySize, bool color)
        {
            for (var yPos = p.Y; yPos != p.Y + ySize; yPos++)
            {
                var bufferPosition = bytesPrLine * yPos + p.X / 8;
                var xBitsLeft = xSize;
                var xPos = p.X;

                while (xBitsLeft > 0)
                {
                    var bitPos = xPos & 0x7;
                    var bitsToWrite = Math.Min(xBitsLeft, (uint)(8 - bitPos));
                    if (color)
                        displayBuf[bufferPosition] |= (byte)(bs.GetBits(bitsToWrite) << bitPos);
                    else
                        displayBuf[bufferPosition] &= (byte)~(bs.GetBits(bitsToWrite) << bitPos);
                    xBitsLeft -= bitsToWrite;
                    xPos += (int)bitsToWrite;
                    bufferPosition++;
                }
            }
        }

        public static void WriteText(Font font, Point point, string text, bool color)
        {
            foreach (var c in text)
            {
                var cs = font.GetChar(c);
                if (point.X + cs.Width > Width)
                    break;

                DrawBitmap(cs, point, cs.Width, cs.Height, color);
                point.X += (int)cs.Width;
            }
        }

        public static void WriteTextBox(Font f, Rectangle r, string text, bool color)
        {
            WriteTextBox(f, r, text, color, Alignment.Left);
        }

        public static void WriteTextBox(Font f, Rectangle r, string text, bool color, Alignment aln)
        {
            DrawRectangle(r, !color, true); // Clear background
            var xpos = 0;
            if (aln == Alignment.Left)
            {
            }
            else if (aln == Alignment.Center)
            {
                var width = TextWidth(f, text);
                xpos = (r.P2.X - r.P1.X) / 2 - width / 2;
                if (xpos < 0) xpos = 0;
            }
            else
            {
                var width = TextWidth(f, text);
                xpos = r.P2.X - r.P1.X - width;
                if (xpos < 0) xpos = 0;
            }
            WriteText(f, r.P1 + new Point(xpos, 0), text, color);
        }

        public static void DrawRectangle(Rectangle r, bool color, bool fill)
        {
            if (fill)
            {
                var length = r.P2.X - r.P1.X;
                for (var y = r.P1.Y; y <= r.P2.Y; ++y)
                    DrawHLine(new Point(r.P1.X, y), length, color);
            }
            else
            {
                var length = r.P2.X - r.P1.X;
                var height = r.P2.Y - r.P1.Y;

                DrawHLine(new Point(r.P1.X, r.P1.Y), length, color);
                DrawHLine(new Point(r.P1.X, r.P2.Y), length, color);

                DrawVLine(new Point(r.P1.X, r.P1.Y + 1), height - 2, color);
                DrawVLine(new Point(r.P2.X, r.P1.Y + 1), height - 2, color);
            }
        }

        public static void DrawVLine(Point startPoint, int height, bool color)
        {
            for (var y = 0; y <= height; y++)
                SetPixel(startPoint.X, startPoint.Y + y, color);
        }

        public static void DrawHLine(Point startPoint, int length, bool color)
        {
            for (var i = 0; i < length; i++)
                SetPixel(startPoint.X + i, startPoint.Y, color);
        }

        public static int TextWidth(Font font, string text)
        {
            var width = 0;
            foreach (var c in text)
            {
                var cs = font.GetChar(c);
                width += (int)cs.Width;
            }
            return width;
        }

        public static void SetPixel(int x, int y, bool color)
        {
            if (!IsPixelInLcd(x, y))
                return;

            var index = x / 8 + y * bytesPrLine;
            var bit = x & 0x7;
            if (color)
                displayBuf[index] |= (byte)(1 << bit);
            else
                displayBuf[index] &= (byte)~(1 << bit);
        }

        protected static bool IsPixelInLcd(Point pixel)
        {
            return pixel.X >= 0 && pixel.Y >= 0 && pixel.X <= Width && pixel.Y <= Height;
        }

        protected static bool IsPixelInLcd(int x, int y)
        {
            return x >= 0 && y >= 0 && x <= Width && y <= Height;
        }
    }
}