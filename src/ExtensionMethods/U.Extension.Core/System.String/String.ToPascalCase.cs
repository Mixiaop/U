using System;
using System.Globalization;
public static partial class Extensions
{
    /// <summary>
    /// Converts camelCase string to PascalCase string.
    /// </summary>
    /// <param name="str">String to convert</param>
    /// <returns>PascalCase of the string</returns>
    public static string ToPascalCase(this string str)
    {
        return str.ToPascalCase(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts camelCase string to PascalCase string in specified culture.
    /// </summary>
    /// <param name="str">String to convert</param>
    /// <param name="culture">An object that supplies culture-specific casing rules</param>
    /// <returns>PascalCase of the string</returns>
    public static string ToPascalCase(this string str, CultureInfo culture)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        if (str.Length == 1)
        {
            return str.ToUpper(culture);
        }

        return char.ToUpper(str[0], culture) + str.Substring(1);
    }
}