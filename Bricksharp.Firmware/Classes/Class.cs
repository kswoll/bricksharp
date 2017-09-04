using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bricksharp.Firmware.Classes
{
    public abstract class Class
    {
        public DirectoryInfo Folder { get; }

        protected Class(string path)
        {
//            Console.WriteLine(path);
            Folder = new DirectoryInfo(Path.Combine("/sys/class", path));
        }

        protected void InvokeCommand(string command)
        {
            File.WriteAllText(Path.Combine(Folder.FullName, "command"), command);
        }
    }
}