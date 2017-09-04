using Bricksharp.Firmware.Classes;

namespace Bricksharp.Firmware.Motors
{
    public class TachoMotor : Class
    {
        public int Position => position.GetValue<int>();

        public void Reset() => InvokeCommand("reset");
        public void Stop() => InvokeCommand("stop");
        public void RunForever() => InvokeCommand("run-forever");

        private readonly ClassProperty position;
        private readonly ClassProperty speed;
        private readonly ClassProperty positionSp;

        public TachoMotor(int port) : base($"tacho-motor/motor{port}")
        {
            position = new ClassProperty(Folder, "position");
            speed = new ClassProperty(Folder, "speed_sp");
            positionSp = new ClassProperty(Folder, "position_sp");
        }

        public int Speed
        {
            get => speed.GetValue<int>();
            set => speed.SetValue(value);
        }

        public void RunToAbsolutePosition(int position)
        {
            positionSp.SetValue(position);
            InvokeCommand("run-to-abs-pos");
        }

        public void RunToRelativePosition(int position)
        {
            positionSp.SetValue(position);
            InvokeCommand("run-to-rel-pos");
        }
    }
}