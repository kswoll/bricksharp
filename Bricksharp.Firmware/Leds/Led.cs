using System.IO;

namespace Bricksharp.Firmware.Leds
{
    public class Led
    {
        public LedColor Color { get; }
        public LedPosition Position { get; }
        public string Folder => $"/sys/class/leds/ev3:{Position.ToString().ToLower()}:{Color.ToString().ToLower()}:ev3dev";

        private string BrightnessFile => Path.Combine(Folder, "brightness");

        public Led(LedColor color, LedPosition position)
        {
            Color = color;
            Position = position;
        }

        public byte Brightness
        {
            get { return byte.Parse(File.ReadAllText(BrightnessFile)); }
            set { File.WriteAllText(BrightnessFile, value.ToString()); }
        }
    }
}