using System;
using Bricksharp.Firmware.Classes;

namespace Bricksharp.Firmware.Sensors
{
    public class Ev3InfraredSensor : Sensor
    {
        public event Action<int> Value0Changed;

        private readonly EnumClassProperty<Ev3InfraredSensorMode> mode;

        public Ev3InfraredSensor(int port) : base(port)
        {
            mode = new EnumClassProperty<Ev3InfraredSensorMode>(Folder, "mode");
        }

        public Ev3InfraredSensorMode Mode
        {
            get => mode.Value;
            set => mode.Value = value;
        }

        protected override void OnTriggered(string value)
        {
            Value0Changed?.Invoke(int.Parse(value));
        }
    }
}