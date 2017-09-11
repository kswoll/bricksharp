using System.Runtime.Serialization;

namespace Bricksharp.Firmware.Sensors
{
    public enum Ev3InfraredSensorMode
    {
        [EnumMember(Value = "IR-PROX")]
        Proximity,

        [EnumMember(Value = "IR-SEEK")]
        Seek,

        [EnumMember(Value = "IR-REMOTE")]
        Remote
    }
}