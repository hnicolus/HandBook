using System;
using System.Collections.Generic;
using System.Text;

namespace HandBook.Core.Functions
{
    public static class StringExt
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) { return value; }

           if(value.Length <= maxLength)
                return value;
            else
                return value.Substring(0, Math.Min(value.Length, maxLength));
        }
    }
}
