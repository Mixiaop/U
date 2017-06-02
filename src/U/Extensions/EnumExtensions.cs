using System;
using System.Reflection;
using U.CodeAnnotations;

namespace U
{
    public static class EnumExtensions
    {
        public static string ToAlias(this Enum _enum)
        {
            Type type = _enum.GetType();
            FieldInfo fd = type.GetField(_enum.ToString());
            if (fd == null) return string.Empty;
            object[] attrs = fd.GetCustomAttributes(typeof(EnumAliasAttribute), false);
            string name = string.Empty;
            foreach (EnumAliasAttribute attr in attrs)
            {
                name = attr.Alias;
            }
            return name;
        }

    }
}
