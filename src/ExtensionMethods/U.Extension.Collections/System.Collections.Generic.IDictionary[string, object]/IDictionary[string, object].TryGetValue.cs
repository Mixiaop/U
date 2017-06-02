using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

public static partial class Extensions
{
    /// <summary>
    /// This method is used to try to get a value in a dictionary if it does exists.
    /// </summary>
    /// <typeparam name="T">Type of the value</typeparam>
    /// <param name="dictionary">The collection object</param>
    /// <param name="key">Key</param>
    /// <param name="value">Value of the key (or default value if key not exists)</param>
    /// <returns>True if key does exists in the dictionary</returns>
    internal static bool TryGetValue<T>(this IDictionary<string, object> dictionary, string key, out T value)
    {
        object valueObj;
        if (dictionary.TryGetValue(key, out valueObj) && valueObj is T)
        {
            value = (T)valueObj;
            return true;
        }

        value = default(T);
        return false;
    }
}