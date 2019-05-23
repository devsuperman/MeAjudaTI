using System;

namespace Dominio.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToShortTimeString(this TimeSpan time)
        {
            var str = $"{time.Minutes.ToString("00")}m";

            if (time.Hours > 0)
                str = $"{time.Hours.ToString("00")}h:" + str;

            if (time.Days > 0)            
                str = $"{time.Days}d, " + str;                

            return str;
        }
    }
}