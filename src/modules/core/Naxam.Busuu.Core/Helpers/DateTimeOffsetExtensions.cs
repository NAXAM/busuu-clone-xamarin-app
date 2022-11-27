using System;
namespace Naxam.Busuu.Core.Helpers
{
    public static class DateTimeOffsetExtensions
    {
        public static bool IsToday(this DateTimeOffset dateTime) {
            var today = DateTimeOffset.Now;
            return dateTime.Year == today.Year && dateTime.Month == today.Month && dateTime.Day == today.Day;
        }
    }
}
