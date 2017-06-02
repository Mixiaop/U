using System;

public static partial class Extensions
{
    public static bool ExistInEnum<T>(this int value)
    {
        Type enumType = typeof(T);
        bool isExists = false;
        foreach (string str in Enum.GetNames(enumType))
        {
            if (Enum.Format(enumType, Enum.Parse(enumType, str), "d").ToInt() == value)
            {
                isExists = true;
                break;
            }
        }

        return isExists;
    }

}