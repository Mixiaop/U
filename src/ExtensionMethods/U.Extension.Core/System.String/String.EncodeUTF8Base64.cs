// Copyright (c) 2015 ZZZ Projects. All rights reserved
using System;
using System.Text;

public static partial class Extensions
{
    
    /// <summary>
    /// EncodeBase64 中文支持（因为 EncodeBase64 “中文”编码出来值都是一样的）
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static string EncodeUTF8Base64(this string @this)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(@this);
        return Convert.ToBase64String(bytes);
    }
}
