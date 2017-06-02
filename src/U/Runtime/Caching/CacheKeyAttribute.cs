using System;
using System.Linq;

namespace U.Runtime.Caching
{
    /// <summary>
    /// Define cache key used to method
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheKeyAttribute : Attribute
    {
        public string Name { get; private set; }

        public string Format { get; private set; }

        public string Description { get; private set; }

        public CacheKeyAttribute(string name, string description) : this(name, "", description) { }

        public CacheKeyAttribute(string name, string format, string description)
        {
            Name = name;
            Format = format;
            Description = description;
        }

        public string GetLocalizedKey(params object[] args)
        {
            string localizedKey = Name;

            if (!string.IsNullOrEmpty(Format))
            {
                localizedKey += "[" + Format;
                int i = 0;
                foreach (var arg in args)
                {
                    localizedKey = localizedKey.Replace("{" + i + "}", arg != null ? arg.ToString() : "");
                    i++;
                }
                localizedKey += "]";
            }

            return localizedKey;
        }
    }
}
