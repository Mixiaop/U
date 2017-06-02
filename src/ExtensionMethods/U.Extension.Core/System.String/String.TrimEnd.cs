using System;

public static partial class Extensions
{
    public static string TrimEnd(this string value, string forRemoving)
    {
        if (string.IsNullOrEmpty(value)) return value;
        if (string.IsNullOrEmpty(forRemoving)) return value;

        while (value.EndsWith(forRemoving, StringComparison.InvariantCultureIgnoreCase))
        {
            value = value.Remove(value.LastIndexOf(forRemoving, StringComparison.InvariantCultureIgnoreCase));
        }
        return value;
    }
}
