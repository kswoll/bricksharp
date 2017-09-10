using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Bricksharp.Firmware.Utils
{
    public class EnumMemberCache<T> where T : struct
    {
        public static T GetValue(string name) => enumsByName[name];
        public static string GetName(T value) => enumsByValue[value];

        private static readonly Dictionary<string, T> enumsByName = new Dictionary<string, T>();
        private static readonly Dictionary<T, string> enumsByValue = new Dictionary<T, string>();

        static EnumMemberCache()
        {
            foreach (var field in typeof(T).GetFields().Where(x => x.IsStatic && x.IsPublic))
            {
                var value = field.GetValue(null);
                var attribute = field.GetCustomAttribute<EnumMemberAttribute>();
                enumsByName[attribute.Value] = (T)value;
                enumsByValue[(T)value] = attribute.Value;
            }
        }
    }
}