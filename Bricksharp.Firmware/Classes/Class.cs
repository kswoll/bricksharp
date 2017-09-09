using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bricksharp.Firmware.Classes
{
    public abstract class Class
    {
        public const string ClassRoot = "/sys/class";

        public DirectoryInfo Folder { get; }

        protected Class(string path)
        {
//            Console.WriteLine(path);
            Folder = new DirectoryInfo(Path.Combine(ClassRoot, path));
        }

        protected Class(DirectoryInfo folder)
        {
            Folder = folder;
        }

        protected void InvokeCommand(string command)
        {
            File.WriteAllText(Path.Combine(Folder.FullName, "command"), command);
        }
    }
}