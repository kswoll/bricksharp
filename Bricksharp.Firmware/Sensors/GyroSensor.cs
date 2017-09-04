using System;

namespace Bricksharp.Firmware.Sensors
{
    public class GyroSensor : Sensor
    {
        public event Action<int> Changed;

        public GyroSensor(int port) : base(port)
        {
        }

        protected override void OnTriggered(string value)
        {
            OnChanged(int.Parse(value));
        }

        protected void OnChanged(int value)
        {
            Changed?.Invoke(value);
        }
    }
}