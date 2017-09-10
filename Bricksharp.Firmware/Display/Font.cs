using System;
using System.Reflection;

namespace Bricksharp.Firmware.Display
{
    public class Font
    {
        public static Font SmallFont { get; } = FromResource(Assembly.GetExecutingAssembly(), "font.profont_7");
        public static Font MediumFont { get; } = FromResource(Assembly.GetExecutingAssembly(), "font.info56_12");

        public uint MaxWidth { get; }
        public uint MaxHeight { get; }

        private readonly uint firstChar;
        private readonly uint charWordSize;
        private readonly uint[] data;

        public Font (uint[] data)
        {
            if (data[0] != 0x544E4F46)
                throw new ArgumentException("Invalid value in font data");

            MaxWidth = data[1];
            MaxHeight = data[2];
            charWordSize = data[3];
            firstChar = 4;
            this.data = data;
        }

        public static Font FromResource(Assembly asm, string resourceName)
        {
            var stream = asm.GetManifestResourceStream(resourceName);
            var bytedata = new byte[stream.Length];
            stream.Read(bytedata, 0, (int)stream.Length);
            var data = new uint[stream.Length/4];

            for (var i = 0; i != stream.Length/4; ++i)
                data[i] = BitConverter.ToUInt32(bytedata, i * 4);

            return new Font(data);
        }

        public CharStreamer GetChar(char c)
        {
            var index = c - 32;
            if (index < 0 || (index > 128-32))
                index = 0;

            var result = new CharStreamer(MaxHeight, data, (int)(firstChar+index*charWordSize));
            return result;
        }

        public Point TextSize(string text)
        {
            var width = 0;

            foreach (var c in text)
            {
                var cs = GetChar(c);
                width += (int)cs.Width;
            }

            return new Point(width, (int)MaxHeight);
        }
    }
}