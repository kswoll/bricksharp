using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bricksharp.Firmware.Leds;

namespace Bricksharp.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                new Led(LedColor.Red, LedPosition.Left).Brightness = 255;
                Console.WriteLine("Set led");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
