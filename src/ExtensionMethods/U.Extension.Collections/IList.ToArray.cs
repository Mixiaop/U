using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

public static partial class Extensions
{

    public static string[] ToArray<T>(this IList<T> @this, Func<T, string> func)
    {
        string[] result = new string[@this.Count];
        for (int i = 0; i < @this.Count; i++)
        {
            result[i] = func(@this[i]);
        }
        return result;
    }

    public static int[] ToArray<T>(this IList<T> @this, Func<T, int> func)
    {
        int[] result = new int[@this.Count];
        for (int i = 0; i < @this.Count; i++)
        {
            result[i] = func(@this[i]);
        }
        return result;
    }

    public static IList<TReturn> ToArray<TType, TReturn>(this IList<TType> @this, Func<TType, TReturn> func)
    {
        IList<TReturn> result = new List<TReturn>();
        for (int i = 0; i < @this.Count; i++)
        {
            result.Add(func(@this[i]));
        }
        return result;
    }
}
