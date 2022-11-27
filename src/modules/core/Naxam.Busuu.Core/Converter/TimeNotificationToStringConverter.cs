using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.Core.Converter
{
    public class TimeNotificationToStringConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Date == DateTime.Today)
            {
                return value.ToString("HH:mm");
            }
            return string.Format("{0:dd MMM yyyy HH:mm}", value);
        }
    }
}
