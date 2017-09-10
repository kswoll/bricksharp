using System.Runtime.Serialization;

namespace Bricksharp.Firmware.Sensors
{
    public enum NxtUltrasonicSensorMode
    {
        [EnumMember(Value = "US-DIST-CM")]
        CentimetersContinuous,

        [EnumMember(Value = "US-SI-CM")]
        CentimetersSingle,

        [EnumMember(Value = "US-DIST-IN")]
        InchesContinuous,

        [EnumMember(Value = "US-SI-IN")]
        InchesSingle,

        [EnumMember(Value = "US-LISTEN")]
        Listen
    }
}