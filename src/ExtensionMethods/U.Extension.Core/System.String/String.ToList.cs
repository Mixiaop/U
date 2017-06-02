using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class Extensions
{
    public static  List<string> ToList(this string input, char splitChar = ',') {
            List<string> list = new List<string>();
            if (input.IsNotNullOrEmpty())
            {
                foreach (var s in input.Split(splitChar))
                {
                    if (s.IsNotNullOrEmpty())
                        list.Add(s);
                }
            }

            return list;
    }
}