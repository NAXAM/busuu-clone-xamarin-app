using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using Naxam.Busuu.Core.Helpers;

namespace Naxam.Busuu.Core.Converter
{
    public class PostedTimeToStringConverter : MvxValueConverter<DateTimeOffset, string>
    {
        protected override string Convert(DateTimeOffset value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.IsToday())
            {
                return "Posted today at " + value.ToString("HH:mm");
            }
            return string.Format("Posted {0}, {1:dd MMM yyyy HH:mm}", value.DayOfWeek, value);
        }
    }

    public class PostedToStringConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Date == DateTime.Today)
            {
                return "Posted today at " + value.ToString("HH:mm");
            }
            return string.Format("Posted {0}, {1:dd MMM yyyy HH:mm}", value.DayOfWeek, value);
        }
    }
}
