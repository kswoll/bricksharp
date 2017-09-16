using System;
using System.Runtime.InteropServices;
using Bricksharp.Firmware.Utils;

namespace Bricksharp.Firmware.Unix
{
    public class MemoryArea
    {
        private IntPtr ptr;
        private readonly uint size;

        public MemoryArea(IntPtr ptr, uint size)
        {
            this.ptr = ptr;
            this.size = size;
        }

        /// <summary>
        /// Write a byte array to the memory map
        /// </summary>
        /// <param name="data">Data.</param>
        public void Write(byte[] data)
        {
            Write(0, data);
        }

        /// <summary>
        /// Write a byte array to the memory map
        /// </summary>
        /// <param name="offset">Memory map offset</param>
        /// <param name="data">Data to write</param>
        public void Write(int offset, byte[] data)
        {
            if (offset + data.Length > size)
                throw new IndexOutOfRangeException($"Out of range accessing index {offset + data.Length}, max {size}");
            Marshal.Copy(data, 0, offset != 0 ? ptr.Add(offset) : ptr, data.Length);
        }

        /// <summary>
        /// Copy the whole memory map into an array and return it
        /// </summary>
        public byte[] Read()
        {
            return Read(0, (int)size);
        }

        /// <summary>
        /// Copy part of the memory map into an array and return it
        /// </summary>
        /// <param name="offset">Memory map offset</param>
        /// <param name="length">Number of bytes to read</param>
        public byte[] Read(int offset, int length)
        {
            if (offset + length > size)
                throw new IndexOutOfRangeException($"Out of range accessing index {offset + length}, max {size}");

            var reply = new byte[length];
            Marshal.Copy(offset != 0 ? ptr.Add(offset) : ptr, reply, 0, length);
            return reply;
        }
    }
}