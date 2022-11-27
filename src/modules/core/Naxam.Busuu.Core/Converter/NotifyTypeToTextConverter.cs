using MvvmCross.Platform.Converters;
using System;
using System.Globalization;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Core.Converter
{
    public class NotifyTypeToTextConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            // pass 
            NotificationType type = (NotificationType)value;
            if(type==NotificationType.Accpect)
            {
                return "has accepted your friend request";

            }

            if (type == NotificationType.Reply)    
            {
                return "replied";
            }

            if (type == NotificationType.Request)
            {
                return "Request ";

            }

            if (type == NotificationType.Like)
            {
                return "liked your comment";

            }

            if (type == NotificationType.Correct)
            {
                return "corrected your excise";

            }

            return "nothing to show";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
