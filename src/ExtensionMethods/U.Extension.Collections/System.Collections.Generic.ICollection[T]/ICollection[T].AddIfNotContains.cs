using System;
using System.Collections.Generic;

public static partial class Extensions
{
    /// <summary>
    /// 添加1项到集合中，如果存在则不添加
    /// </summary>
    /// <param name="source">集合</param>
    /// <param name="item">需要添加的对象</param>
    /// <typeparam name="T">集合类型</typeparam>
    /// <returns>返回是否添加成功，已存在则返回False</returns>
    public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
    {
        if (source == null)
        {
            throw new ArgumentNullException("source");
        }

        if (source.Contains(item))
        {
            return false;
        }

        source.Add(item);
        return true;
    }
}