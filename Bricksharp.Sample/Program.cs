using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Bricksharp.Firmware.Leds;

namespace Bricksharp.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var redLeft = new Led(LedColor.Red, LedPosition.Left);
                var greenLeft = new Led(LedColor.Green, LedPosition.Left);
                redLeft.Brightness = 0;
                greenLeft.Brightness = 0;
                for (var i = 0; i < 100; i++)
                {
                    redLeft.Brightness = (byte)(255 - redLeft.Brightness);
                    greenLeft.Brightness = (byte)(255 - redLeft.Brightness);
                    Thread.Sleep(10);
                }
                Console.WriteLine("Set led 2");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
