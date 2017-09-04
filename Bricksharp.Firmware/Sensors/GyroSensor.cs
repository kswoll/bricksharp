using System;
using System.IO;
using System.Threading;
using Bricksharp.Firmware.Classes;

namespace Bricksharp.Firmware.Sensors
{
    public class GyroSensor : Class
    {
        public event Action<int> Changed;

        public GyroSensor(int port) : base($"lego-sensor/sensor{port}")
        {
            var value0 = File.ReadAllText($"{Folder}/value0");
//            Console.WriteLine(value0);

            var lastValue = value0;
            var thread = new Thread(() =>
            {
//                Console.WriteLine("Started Sensor");
                while (true)
                {
                    var value = File.ReadAllText($"{Folder}/value0").Trim();
                    if (value != lastValue)
                    {
                        OnChanged(int.Parse(value));
                        lastValue = value;
                    }
                    Thread.Sleep(10);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        protected void OnChanged(int value)
        {
            Changed?.Invoke(value);
        }
    }
}