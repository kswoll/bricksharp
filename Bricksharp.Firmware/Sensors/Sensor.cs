using System.IO;
using Bricksharp.Firmware.Classes;

namespace Bricksharp.Firmware.Sensors
{
    public abstract class Sensor : Class
    {
        public const string LegoSensorFolder = "lego-sensor";

        protected abstract void OnTriggered(string value);

        public string Value0 => value0.GetValue<string>();

        private readonly ClassProperty value0;

        protected Sensor(int port) : base(SensorDriver.GetSensorFolder(port))
        {
            value0 = new ClassProperty(Folder, "value0");

            SensorDriver.Register(this);
        }

        internal void Trigger(string value)
        {
            OnTriggered(value);
        }
    }
}