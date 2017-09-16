using System;

namespace Bricksharp.Firmware.Utils
{
    public static class IntPtrExtensions
    {
        /// <summary>
        /// Add a value to a int pointer
        /// </summary>
        /// <param name="ptr">Pointer to add value to</param>
        /// <param name="val">Value to add</param>
        public static IntPtr Add(this IntPtr ptr, int val)
        {
            return new IntPtr(ptr.ToInt64() + val);
        }
    }
}