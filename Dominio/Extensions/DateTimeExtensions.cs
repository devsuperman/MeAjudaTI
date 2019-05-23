using System;

namespace Dominio.Extensions
{
    public static class DateTimeExtensions
    {
        public static string NomeDoMes(this DateTime date)
        {
            var mesExtenso = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(date.Month).ToLower();
            mesExtenso =  char.ToUpper(mesExtenso[0]) + mesExtenso.Substring(1); 
            return mesExtenso;
        }
    }
}