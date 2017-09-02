using System;
using System.IO;
using System.Threading.Tasks;

namespace Bricksharp.Firmware.Buttons
{
    /// <summary>
    /// Administers a thread which watches the key file (/dev/input/by-path/platform-gpio-keys.0-event) and emits
    /// events for key press/release to which consumerse may subscribe.
    /// </summary>
    internal class ButtonDriver
    {
        private static readonly FileInfo keys = new FileInfo(@"/dev/input/by-path/platform-gpio-keys.0-event");
        private static readonly object locker = new object();

        private static bool isInitialized;

        internal static void Initialize()
        {
            lock (locker)
            {
                if (isInitialized)
                    return;
            }

            isInitialized = true;
            Task.Run(() =>
            {
                Console.WriteLine("Started ButtonDriver");
                using (var reader = new BinaryReader(new FileStream(keys.FullName, FileMode.Open, FileAccess.Read)))
                {
                    while (true)
                    {
                        var seconds = reader.ReadUInt32();
                        var microseconds = reader.ReadUInt32();

                        // Two packets are sent each time a key is pressed and another two when it's released.  As the second packet
                        // has a type of zero and no useful data, I'm not sure why that is, but for our purposes, just ignore input
                        // with a type of zero.
                        var type = reader.ReadUInt16();

                        var code = reader.ReadUInt16();
                        var value = reader.ReadUInt32();
//                        Console.WriteLine(type);

                        if (type != 0)
                        {
//                            Console.WriteLine($"second: {seconds}, microseconds: {microseconds}, type: {type}, code: {code}, value: {value}");

                            var key = (ButtonKey)code;
                            var isPressed = value == 0;
                            if (isPressed)
                                OnPressed(key);
                            else
                                OnReleased(key);
                        }
                    }
                }
            });
        }

        private static Button GetButton(ButtonKey key)
        {
            switch (key)
            {
                case ButtonKey.Back:
                    return Button.Back;
                case ButtonKey.Left:
                    return Button.Left;
                case ButtonKey.Down:
                    return Button.Down;
                case ButtonKey.Right:
                    return Button.Right;
                case ButtonKey.Up:
                    return Button.Up;
                case ButtonKey.Enter:
                    return Button.Enter;
                default:
                    throw new Exception($"Unexpected key: {key}");
            }
        }

        private static void OnPressed(ButtonKey key)
        {
            GetButton(key).NotifyPressed();
        }

        private static void OnReleased(ButtonKey key)
        {
            GetButton(key).NotifyReleased();
        }
    }
}