using System;

public static partial class Extensions
{
    /// <summary>
    /// Converts given object to a value type using <see cref="Convert.ChangeType(object,System.TypeCode)"/> method.
    /// </summary>
    /// <param name="obj">Object to be converted</param>
    /// <typeparam name="T">Type of the target object</typeparam>
    /// <returns>Converted object</returns>
    public static T To<T>(this object obj)
        where T : struct
    {
        return (T)Convert.ChangeType(obj, typeof(T));
    }
}