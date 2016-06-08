using System.IO;
using Bricksharp.Firmware.Classes;

namespace Bricksharp.Firmware.Leds
{
    public class Led : Class
    {
        public LedColor Color { get; }
        public LedPosition Position { get; }

        private const string BrightnessKey = "brightness";

        public Led(LedColor color, LedPosition position) : base(new DirectoryInfo($"/sys/class/leds/ev3:{position.ToString().ToLower()}:{color.ToString().ToLower()}:ev3dev"))
        {
            Color = color;
            Position = position;

            AddProperty(new ClassProperty(Folder, BrightnessKey));
        }

        public byte Brightness
        {
            get { return Properties[BrightnessKey].GetValue<byte>(); }
            set { Properties[BrightnessKey].SetValue(value); }
        }
    }
}