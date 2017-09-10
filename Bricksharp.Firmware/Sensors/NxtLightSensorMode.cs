using System.Runtime.Serialization;

namespace Bricksharp.Firmware.Sensors
{
    public enum NxtLightSensorMode
    {
        [EnumMember(Value = "REFLECT")]
        Reflect,

        [EnumMember(Value = "AMBIENT")]
        Ambient
    }
}