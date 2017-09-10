using System.IO;
using Bricksharp.Firmware.Utils;

namespace Bricksharp.Firmware.Classes
{
    public class EnumClassProperty<T> : ClassProperty where T : struct
    {
        public EnumClassProperty(DirectoryInfo parent, string file) : base(parent, file)
        {
        }

        public new T Value
        {
            get => EnumMemberCache<T>.GetValue(base.Value);
            set => base.Value = EnumMemberCache<T>.GetName(value);
        }
    }
}