using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Bricksharp.Firmware.Sensors
{
    public class SensorDriver
    {
        private const int Frequency = 10;

        private static readonly List<Sensor> sensors = new List<Sensor>();
        private static readonly List<string> values = new List<string>();
        private static readonly object locker = new object();

        static SensorDriver()
        {
//            var value0 = File.ReadAllText($"{Folder}/value0");
//            Console.WriteLine(value0);

//            var lastValue = value0;
            var thread = new Thread(() =>
            {
//                Console.WriteLine("Started Sensor");
                while (true)
                {
                    lock (locker)
                    {
                        for (var i = 0; i < sensors.Count; i++)
                        {
//                        Console.WriteLine("Iteration " + i);
                            var sensor = sensors[i];
//                        Console.WriteLine("Reading value0");
                            var lastValue = values[i];
                            var value = sensor.Value0;
                            if (lastValue != value)
                            {
//                                Console.WriteLine($"'{lastValue}' != '{value}'");
                                values[i] = value;
                                sensor.Trigger(value);
                            }
                        }
                    }

                    Thread.Sleep(Frequency);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        internal static void Register(Sensor sensor)
        {
            lock (locker)
            {
                sensors.Add(sensor);
                values.Add(sensor.Value0);
            }
        }
    }
}