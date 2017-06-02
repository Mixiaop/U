// Copyright (c) 2015 ZZZ Projects. All rights reserved
using System;
using System.Text;

public static partial class Extensions
{
    public static string DecodeUTF8Base64(this string @this)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(@this));
    }
}