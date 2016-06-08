using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bricksharp.Firmware.Classes
{
    public abstract class Class
    {
        public DirectoryInfo Folder { get; }
        public IReadOnlyDictionary<string, ClassProperty> Properties => properties;

        private readonly Dictionary<string, ClassProperty> properties;

        protected Class(DirectoryInfo folder, IEnumerable<ClassProperty> properties)
        {
            Folder = folder;
            this.properties = properties.ToDictionary(x => x.File.Name, x => x);
        }

        protected Class(DirectoryInfo folder, params ClassProperty[] properties)
        {
            Folder = folder;
            this.properties = properties.ToDictionary(x => x.File.Name, x => x);
        }

        protected void AddProperty(ClassProperty property)
        {
            properties.Add(property.File.Name, property);
        }
    }
}