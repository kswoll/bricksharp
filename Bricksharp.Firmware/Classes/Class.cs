using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bricksharp.Firmware.Classes
{
    public abstract class Class
    {
        public DirectoryInfo Folder { get; }
        public IReadOnlyDictionary<string, ClassProperty> Properties => properties;

        private readonly Dictionary<string, ClassProperty> properties = new Dictionary<string, ClassProperty>();

        protected Class(string path)
        {
            Console.WriteLine(path);
            Folder = new DirectoryInfo(Path.Combine("/sys/class", path));
        }

        protected void AddProperty(ClassProperty property)
        {
            properties.Add(property.File.Name, property);
        }
    }
}