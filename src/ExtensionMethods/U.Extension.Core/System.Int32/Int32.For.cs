using System;

public static partial class Extensions
{
    /// <summary>
    /// 循环执行操作
    /// </summary>
    /// <param name="n"></param>
    /// <param name="action"></param>
    public static void For(this int n, Action<int> action)
    {
        for (int i = 0; i < n; i++)
        {
            action(i);
        }
    }
}