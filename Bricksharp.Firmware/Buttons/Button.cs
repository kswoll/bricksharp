using System;

namespace Bricksharp.Firmware.Buttons
{
    public class Button
    {
        public static readonly Button Back = new Button();
        public static readonly Button Up = new Button();
        public static readonly Button Down = new Button();
        public static readonly Button Left = new Button();
        public static readonly Button Right = new Button();
        public static readonly Button Enter = new Button();

        public event Action Pressed;
        public event Action Released;

        private Button()
        {
            ButtonDriver.Initialize();
        }

        internal void NotifyPressed()
        {
            Pressed?.Invoke();
        }

        internal void NotifyReleased()
        {
            Released?.Invoke();
        }
    }
}