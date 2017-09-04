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

        private readonly ClassProperty brightness;

        public Led(LedColor color, LedPosition position) : base($"leds/ev3:{position.ToString().ToLower()}:{color.ToString().ToLower()}:ev3dev")
        {
            Color = color;
            Position = position;

            brightness = new ClassProperty(Folder, "brightness");
        }

        public byte Brightness
        {
            get { return brightness.GetValue<byte>(); }
            set { brightness.SetValue(value); }
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