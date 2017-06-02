using System;

public static partial class Extensions
{
    /// <summary>
    /// Gets index of nth occurence of a char in a string.
    /// </summary>
    /// <param name="str">source string to be searched</param>
    /// <param name="c">Char to search in <see cref="str"/></param>
    /// <param name="n">Count of the occurence</param>
    public static int NthIndexOf(this string str, char c, int n)
    {
        if (str == null)
        {
            throw new ArgumentNullException("str");
        }

        var count = 0;
        for (var i = 0; i < str.Length; i++)
        {
            if (str[i] != c)
            {
                continue;
            }

            if ((++count) == n)
            {
                return i;
            }
        }

        return -1;
    }
}
