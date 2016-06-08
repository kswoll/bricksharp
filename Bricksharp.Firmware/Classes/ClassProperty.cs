using System;
using System.IO;

namespace Bricksharp.Firmware.Classes
{
    public class ClassProperty
    {
        public FileInfo File { get; }

        public ClassProperty(DirectoryInfo parent, string file)
        {
            File = new FileInfo(Path.Combine(parent.FullName, file));
        }

        public string Value
        {
            get { return System.IO.File.ReadAllText(File.FullName); }
            set { System.IO.File.WriteAllText(File.FullName, value); }
        }

        public T GetValue<T>()
        {
            return (T)Convert.ChangeType(Value, typeof(T));
        }

        public void SetValue<T>(T value)
        {
            Value = (string)Convert.ChangeType(value, typeof(string));
        }
    }
}