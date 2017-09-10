using System;
using Bricksharp.Firmware.Classes;
using Bricksharp.Firmware.Utils;

namespace Bricksharp.Firmware.Sensors
{
    public class NxtUltrasonicSensor : Sensor
    {
        public event Action<int> Changed;

        private readonly EnumClassProperty<NxtUltrasonicSensorMode> mode;

        public NxtUltrasonicSensor(int port) : base(port)
        {
            mode = new EnumClassProperty<NxtUltrasonicSensorMode>(Folder, "mode");
        }

        public NxtUltrasonicSensorMode Mode
        {
            get => mode.Value;
            set => mode.Value = value;
        }

        protected override void OnTriggered(string value)
        {
            Changed?.Invoke(int.Parse(value));
        }
    }
}