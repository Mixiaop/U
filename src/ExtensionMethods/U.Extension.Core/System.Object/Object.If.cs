using System;

public static partial class Extensions
{
    /// <summary>如果不是空的</summary>
    /// <param name="item">操作项</param>
    /// <param name="action">操作</param>
    /// <typeparam name="TItem">操作项类型</typeparam>
    public static void IfNotNull<TItem>(this TItem item, Action<TItem> action) where TItem : class
    {
        if (item != null)
        {
            action(item);
        }
    }

    /// <summary>
    /// 检查操作项不为空, 则返回操作项的方法或返回默认的一个值
    /// </summary>
    /// <typeparam name="TResult">结果类型</typeparam>
    /// <typeparam name="TItem">操作项类型</typeparam>
    /// <param name="item">操作项</param>
    /// <param name="action">操作项的方法</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    public static TResult IfNotNull<TResult, TItem>(this TItem item, Func<TItem, TResult> action, TResult defaultValue = default(TResult))
        where TItem : class
    {
        return item != null ? action(item) : defaultValue;
    }

    /// <summary>
    /// 检查如果值为null:返回指定的值, 否则返回 non-null 值
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="item"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static TItem IfNull<TItem>(this TItem item, Func<TItem, TItem> action)
        where TItem : class
    {
        return item ?? action(item);
    }
}
