using System.IO;
using Bricksharp.Firmware.Classes;

namespace Bricksharp.Firmware.Leds
{
    public class Led : Class
    {
        public static LedSet Left { get; } = new LedSet(LedPosition.Left);
        public static LedSet Right { get; } = new LedSet(LedPosition.Right);

        public LedColor Color { get; }
        public LedPosition Position { get; }

        private const string BrightnessKey = "brightness";

        public Led(LedColor color, LedPosition position) : base($"leds/ev3:{position.ToString().ToLower()}:{color.ToString().ToLower()}:ev3dev")
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

        public class LedSet
        {
            public Led Green { get; }
            public Led Red { get; }

            public LedSet(LedPosition position)
            {
                Green = new Led(LedColor.Green, position);
                Red = new Led(LedColor.Red, position);
            }
        }
    }
}