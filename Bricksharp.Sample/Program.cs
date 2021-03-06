﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Bricksharp.Firmware.Buttons;
using Bricksharp.Firmware.Display;
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
//                LcdConsole.WriteLine("Hello world");
//                LcdConsole.WriteLine("Hello world2");
//                return;
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
                    Environment.Exit(0);
                };

//                var gyroSensor = new GyroSensor(1);
//                gyroSensor.Changed += value => Console.WriteLine(value);

//                var lightSensor = new NxtLightSensor(1);
//                lightSensor.Mode = NxtLightSensorMode.Reflect;
//                lightSensor.Changed += value => Console.WriteLine(value);

//                var ultrasonicSensor = new NxtUltrasonicSensor(1);
//                ultrasonicSensor.Mode = NxtUltrasonicSensorMode.CentimetersContinuous;
//                ultrasonicSensor.Changed += value => Console.WriteLine(value);

                var ledSensor = new Ev3InfraredSensor(1);
                ledSensor.Mode = Ev3InfraredSensorMode.Proximity;
                ledSensor.Value0Changed += value => Console.WriteLine(value);

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
