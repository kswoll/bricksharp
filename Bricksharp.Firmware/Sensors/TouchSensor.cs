// <copyright file="TouchSensor.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2017 PlanGrid, Inc. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Bricksharp.Firmware.Buttons;
using Bricksharp.Firmware.Classes;

namespace Bricksharp.Firmware.Sensors
{
    public class TouchSensor : Class
    {
        public event Action<bool> Changed;

        public TouchSensor(int port) : base($"lego-sensor/sensor{port}")
        {
            var value0 = File.ReadAllText($"{Folder}/value0");
            Console.WriteLine(value0);

            var lastValue = value0;
            var thread = new Thread(() =>
            {
                Console.WriteLine("Started Sensor");
                while (true)
                {
                    var value = File.ReadAllText($"{Folder}/value0").Trim();
                    if (value != lastValue)
                    {
                        OnChanged(value == "1");
                        lastValue = value;
                    }
                    Thread.Sleep(10);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        protected void OnChanged(bool pressed)
        {
            Changed?.Invoke(pressed);
        }
    }
}