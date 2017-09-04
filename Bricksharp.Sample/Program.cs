using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Bricksharp.Firmware.Buttons;
using Bricksharp.Firmware.Leds;
using Bricksharp.Firmware.Motors;
using Bricksharp.Firmware.Sensors;

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

//                var touchSensor = new TouchSensor(0);
//                touchSensor.Changed += b => Console.WriteLine($"Changed: {b}");

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

                var gyroSensor = new GyroSensor(1);
                gyroSensor.Changed += value => Console.WriteLine(value);

                var motor = new TachoMotor(0);
                motor.Speed = 500;
                for (var i = 0; i < 5; i++)
                {
                    motor.RunToAbsolutePosition(180);
                    Thread.Sleep(3000);
                    motor.RunToAbsolutePosition(0);
                    Thread.Sleep(3000);
                }

/*
                for (var i = 0; i < 100; i++)
                {
                    Led.Left.Red.Brightness = (byte)(255 - Led.Left.Red.Brightness);
                    Led.Left.Green.Brightness = (byte)(255 - Led.Left.Green.Brightness);
                    Thread.Sleep(1);
                }
                Console.WriteLine("Set led 2");
*/
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
