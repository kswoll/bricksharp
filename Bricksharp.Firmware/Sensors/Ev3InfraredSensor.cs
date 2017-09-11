using Bricksharp.Firmware.Classes;

namespace Bricksharp.Firmware.Sensors
{
    public class Ev3InfraredSensor : Sensor
    {
        private EnumClassProperty<Ev3InfraredSensorMode> mode;

        public Ev3InfraredSensor(int port) : base(port)
        {
            mode = new EnumClassProperty<Ev3InfraredSensorMode>(Folder, "mode");
        }

        protected override void OnTriggered(string value)
        {
            throw new System.NotImplementedException();
        }
    }
}