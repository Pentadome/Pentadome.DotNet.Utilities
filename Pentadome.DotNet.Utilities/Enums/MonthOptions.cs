using System;
using System.Globalization;

namespace Pentadome.DotNet.Utilities
{
    /// <summary>
    /// This enum consists of flags.
    /// </summary>
    [Flags]
    public enum MonthOptions
    {
        None = 0,
        January = 1 << 0,
        February = 1 << 1,
        March = 1 << 2,

        /// <summary>
        /// January | February | March
        /// </summary>
        Q1 = January | February | March,
        April = 1 << 3,
        May = 1 << 4,
        June = 1 << 5,

        /// <summary>
        /// April | May | June
        /// </summary>
        Q2 = April | May | June,
        July = 1 << 6,
        August = 1 << 7,
        September = 1 << 8,

        /// <summary>
        /// July | August | September
        /// </summary>
        Q3 = July | August | September,
        October = 1 << 9,
        November = 1 << 10,
        December = 1 << 11,

        /// <summary>
        /// October | November | December
        /// </summary>
        Q4 = October | November | December,
        All = Q1 | Q2 | Q3 | Q4
    }

#if NETCOREAPP
    public static class MonthOptionsExtensions
    {
        /// <summary>
        /// Get the total amount of days in the given months.
        /// </summary>
        /// <param name="this">The months</param>
        /// <param name="year">The year, if <see langword="null"/>, the current year will be chosen.</param>
        /// <param name="calendar">The calendar to use. If <see langword="null"/>, the calendar of the current UI culture will be chosen.</param>
        /// <returns></returns>
        public static TimeSpan GetAmountOfDays(this MonthOptions @this, int? year = null, Calendar? calendar = null)
        {
            year ??= DateTime.Now.Year;
            calendar ??= CultureInfo.CurrentUICulture.Calendar;
            var totalDays = 0;
            foreach (var index in @this.GetIndexesOfFlags())
            {
                //if (flag != MonthOptions.January && (int)flag % 2 != 0) continue;
                totalDays += calendar.GetDaysInMonth(year.Value, index + 1);
            }
            return TimeSpan.FromDays(totalDays);
        }
    }
#endif
}
