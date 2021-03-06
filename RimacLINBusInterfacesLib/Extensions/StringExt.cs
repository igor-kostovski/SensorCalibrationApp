﻿using System.Linq;

namespace RimacLINBusInterfacesLib.Extensions
{
    public static class StringExt
    {
        public static string ToSentence(this string message)
        {
            return string.Concat(message.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }
    }
}
