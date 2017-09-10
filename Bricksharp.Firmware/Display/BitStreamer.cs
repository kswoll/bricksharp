using System;
using System.Diagnostics;

namespace Bricksharp.Firmware.Display
{
    public class BitStreamer
    {
        private const int PixelsPerWord = 32;

        private readonly uint[] data;
        private int offset;
        private int bitOffset;

        public BitStreamer(uint[] data, int offset = 0)
        {
            this.offset = offset;
            this.data = data;
            bitOffset = 0;
        }

        private void MoveForward(int bits)
        {
            bitOffset += bits;
            if (bitOffset >= PixelsPerWord)
            {
                bitOffset = 0;
                offset++;
            }
        }

        public uint GetBits(uint count)
        {
            Debug.Assert(count <= PixelsPerWord);
            var bitsLeft = PixelsPerWord-bitOffset;
            var bitsToTake = Math.Min((int)count, bitsLeft);
            var result = (data[offset] >> bitOffset) & (0xFFFFFFFF >> (PixelsPerWord - bitsToTake));
            MoveForward(bitsToTake);

            if (count > bitsToTake) // Do we need more bits than we got from the first word
            {
                result |= GetBits((uint)(count - bitsToTake)) << bitsToTake;
            }

            return result;
        }

        public void PutBits(uint bits, int count)
        {
            var bitsLeft = PixelsPerWord - bitOffset;
            var bitsToPut = Math.Min(count, bitsLeft);
            data[offset] |= bits << bitOffset;
            MoveForward(bitsToPut);

            if (count > bitsToPut)
            {
                PutBits (bits << bitsToPut, count - bitsToPut);
            }
        }
    }

}