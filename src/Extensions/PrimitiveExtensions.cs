using System;
using System.Collections.Generic;

namespace Volte.Extensions
{
    public static class PrimitiveExtensions
    {
        #region String

        public static bool EqualsIgnoreCase(this string str, string otherString)
        {
            if (str is null) return false;
            return str.Equals(otherString, StringComparison.OrdinalIgnoreCase);
        }


        public static bool ContainsIgnoreCase(this string str, string value)
        {
            if (str is null) return false;
            return str.Contains(value, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsNullOrWhitespace(this string str) => string.IsNullOrWhiteSpace(str);

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static IEnumerable<int> GetUnicodePoints(this string emoji)
        {
            var pts = new List<int>(emoji.Length);
            for (var i = 0; i < emoji.Length; i++)
            {
                var pt = char.ConvertToUtf32(emoji, i);
                if (pt != 0xFE0F)
                    pts.Add(pt);
                if (char.IsHighSurrogate(emoji[i]))
                    i++;
            }

            return pts.ToArray();
        }

        #endregion

        #region Boolean

        public static bool ShouldBePlural(this int val) => val != 1;

        #endregion
    }
}