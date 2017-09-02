using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Bricksharp.Firmware.Buttons;
using Bricksharp.Firmware.Leds;

namespace Bricksharp.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Led.Left.Red.Brightness = 0;
                Led.Left.Green.Brightness = 0;

/*
                while (true)
                {
                    var key = Console.Read();
                    Console.WriteLine("You typed: " + key);
                }
                */
                Button.Back.Pressed += () =>
                {
                    Console.WriteLine("foo");
                };
                for (var i = 0; i < 100; i++)
                {
                    Led.Left.Red.Brightness = (byte)(255 - Led.Left.Red.Brightness);
                    Led.Left.Green.Brightness = (byte)(255 - Led.Left.Green.Brightness);
                    Thread.Sleep(1);
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
