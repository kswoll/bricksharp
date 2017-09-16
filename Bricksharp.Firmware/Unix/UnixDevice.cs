using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Bricksharp.Firmware.Unix
{
    public class UnixDevice : IDisposable
    {
        private static readonly ASCIIEncoding encoding = new ASCIIEncoding();

        private int fd;

        public UnixDevice(string name)
        {
            fd = Libc.open(encoding.GetBytes(name + char.MinValue), Libc.OpenFlags.O_RDWR);
            if (fd < 0)
                throw new InvalidOperationException("Couldn't open device: " + name);
        }


        public void Dispose()
        {
            Libc.close(fd);
            fd = -1;
        }

        public MemoryArea MMap(uint size, int offset)
        {
            var ptr = Libc.mmap(IntPtr.Zero, size, Libc.ProtectionFlags.PROT_READ | Libc.ProtectionFlags.PROT_WRITE,
                Libc.MMapFlags.MAP_SHARED, fd, offset);
            if ((int)ptr == -1)
                throw new InvalidOperationException("MMap operation failed");
            return new MemoryArea(ptr, size);
        }

        public void Write(byte[] data)
        {
            var pnt = IntPtr.Zero;
            var hasError = false;
            Exception inner = null;
            try
            {
                var size = Marshal.SizeOf(data[0]) * data.Length;
                pnt = Marshal.AllocHGlobal(size);
                Marshal.Copy(data, 0, pnt, data.Length);
                var bytesWritten = Libc.write(fd, pnt, (uint)size);
                if (bytesWritten == -1)
                    hasError = true;
            }
            catch (Exception e)
            {
                hasError = true;
                inner = e;
            }
            finally
            {
                if (pnt != IntPtr.Zero)
                    Marshal.FreeHGlobal(pnt);
            }
            if (hasError)
            {
                if (inner != null)
                    throw inner;
                else
                    throw new InvalidOperationException("Failed to write to Unix device");
            }
        }

        public byte[] Read(int length)
        {
            var reply = new byte[length];
            Exception inner = null;
            var pnt = IntPtr.Zero;
            var hasError = false;
            try
            {
                pnt = Marshal.AllocHGlobal(Marshal.SizeOf(reply[0]) * length);
                var bytesRead = Libc.read(fd, pnt, (uint)length);
                if (bytesRead == -1)
                {
                    hasError = true;
                    Marshal.FreeHGlobal(pnt);
                    pnt = IntPtr.Zero;
                }
                else
                {
                    if (bytesRead != length)
                        reply = new byte[bytesRead];
                    Marshal.Copy(pnt, reply, 0, bytesRead);
                    Marshal.FreeHGlobal(pnt);
                    pnt = IntPtr.Zero;
                }
            }
            catch (Exception e)
            {
                hasError = true;
                inner = e;
            }
            finally
            {
                if (pnt != IntPtr.Zero) Marshal.FreeHGlobal(pnt);
            }
            if (hasError)
                if (inner != null) throw inner;
                else
                    throw new InvalidOperationException("Failed to read from Unix device");
            return reply;
        }

        /// <summary>
        ///     IO control command that copies the IO output to a buffer
        /// </summary>
        /// <returns>Zero if successful</returns>
        /// <param name="cmd">IoCtl request code</param>
        /// <param name="input">Input arguments</param>
        /// <param name="output">Output buffer</param>
        /// <param name="indexToOutput">IO start index to copy to output buffer</param>
        public int IoCtl(int cmd, byte[] input, byte[] output, int indexToOutput)
        {
            var pnt = IntPtr.Zero;
            var hasError = false;
            Exception inner = null;
            var result = -1;
            try
            {
                var size = Marshal.SizeOf(typeof(byte)) * input.Length;
                pnt = Marshal.AllocHGlobal(size);
                Marshal.Copy(input, 0, pnt, input.Length);
                result = Libc.ioctl(fd, cmd, pnt);
                if (result == -1)
                {
                    hasError = true;
                }
                else
                {
                    output = new byte[input.Length - indexToOutput];
                    Marshal.Copy(pnt, output, indexToOutput, input.Length - indexToOutput);
                }
            }
            catch (Exception e)
            {
                hasError = true;
                inner = e;
            }
            finally
            {
                if (pnt != IntPtr.Zero) Marshal.FreeHGlobal(pnt);
            }
            if (hasError)
                if (inner != null) throw inner;
                else
                    throw new InvalidOperationException("Failed to excute IO control command");
            return result;
        }


        /// <summary>
        ///     IO control command. Output is copied back to buffer
        /// </summary>
        /// <returns>Zero if successful</returns>
        /// <param name="requestCode">IoCtl request code</param>
        /// <param name="arguments">IO arguments</param>
        public int IoCtl(int requestCode, byte[] arguments)
        {
            var pnt = IntPtr.Zero;
            var hasError = false;
            Exception inner = null;
            var result = -1;
            try
            {
                var size = Marshal.SizeOf(typeof(byte)) * arguments.Length;
                pnt = Marshal.AllocHGlobal(size);
                Marshal.Copy(arguments, 0, pnt, arguments.Length);
                result = Libc.ioctl(fd, requestCode, pnt);
                if (result == -1) hasError = true;
                else Marshal.Copy(pnt, arguments, 0, arguments.Length);
            }
            catch (Exception e)
            {
                hasError = true;
                inner = e;
            }
            finally
            {
                if (pnt != IntPtr.Zero) Marshal.FreeHGlobal(pnt);
            }
            if (hasError)
                if (inner != null) throw inner;
                else
                    throw new InvalidOperationException("Failed to excute IO control command");
            return result;
        }

        ~UnixDevice()
        {
            if (fd >= 0)
                Libc.close(fd);
        }
    }
}