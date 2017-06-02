using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

public static partial class Extensions
{
    public static void ForEach<T>(this IEnumerable<T> @this, Action<T> func)
    {
        if (@this != null)
        {
            foreach (var x in @this)
            {
                func(x);
            }
        }

    }

    public static void ForEach<T>(this IList<T> @this, Action<T> func)
    {
        if (@this != null)
        {
            foreach (var x in @this)
            {
                func(x);
            }
        }
    }
}
