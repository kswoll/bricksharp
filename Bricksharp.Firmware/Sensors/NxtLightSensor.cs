using System;
using Bricksharp.Firmware.Classes;

namespace Bricksharp.Firmware.Sensors
{
    public class NxtLightSensor : Sensor
    {
        public event Action<int> Changed;

        private ClassProperty mode;

        public NxtLightSensor(int port) : base(port)
        {
            mode = new ClassProperty(Folder, "mode");
        }

        public NxtLightSensorMode Mode
        {
            get
            {
                switch (mode.Value)
                {
                    case "REFLECT":
                        return NxtLightSensorMode.Reflect;
                    case "AMBIENT":
                        return NxtLightSensorMode.Ambient;
                    default:
                        throw new Exception();
                }
            }
            set
            {
                switch (value)
                {
                    case NxtLightSensorMode.Reflect:
                        mode.Value = "REFLECT";
                        break;
                    case NxtLightSensorMode.Ambient:
                        mode.Value = "AMBIENT";
                        break;
                    default:
                        throw new Exception();
                }
            }
        }

        protected override void OnTriggered(string value)
        {
            Changed?.Invoke(int.Parse(value));
        }
    }
}