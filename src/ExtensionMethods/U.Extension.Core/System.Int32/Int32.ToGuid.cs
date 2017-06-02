using System;

public static partial class Extensions
{
    /// <summary>
    /// 创建一个基于interger值的GUID
    /// </summary>
    /// <param name="value"><see cref="int"/>要转的值</param>
    /// <returns><see cref="Guid"/></returns>
    public static Guid ToGuid(this int value)
    {
        byte[] bytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(bytes, 0);
        return new Guid(bytes);
    }
}