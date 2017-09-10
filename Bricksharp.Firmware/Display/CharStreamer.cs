namespace Bricksharp.Firmware.Display
{
    public class CharStreamer : BitStreamer
    {
        public uint Width { get; }
        public uint Height { get; }

        public CharStreamer(uint height, uint[] data, int offset) : base(data, offset)
        {
            Height = height;
            Width = GetBits(8);
        }
    }
}