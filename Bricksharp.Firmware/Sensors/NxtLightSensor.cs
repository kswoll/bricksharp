﻿using System;
using Bricksharp.Firmware.Classes;
using Bricksharp.Firmware.Utils;

namespace Bricksharp.Firmware.Sensors
{
    public class NxtLightSensor : Sensor
    {
        public event Action<int> Changed;

        private readonly EnumClassProperty<NxtLightSensorMode> mode;

        public NxtLightSensor(int port) : base(port)
        {
            mode = new EnumClassProperty<NxtLightSensorMode>(Folder, "mode");
        }

        public NxtLightSensorMode Mode
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