using System;
using System.Globalization;
using Foundation;
using MvvmCross.Platform.Converters;
using Naxam.Busuu.Core.Models;
using UIKit;

namespace Naxam.Busuu.iOS.Core.Converter
{
    public class NotifyTextToFormattedTextConverter : MvxValueConverter<NotificationModel, NSAttributedString>
    {
		protected override NSAttributedString Convert(NotificationModel value, Type targetType, object parameter, CultureInfo culture)
		{
            string notificationTitle = "";

            if (value.Type == NotificationType.Accpect)
			{
                notificationTitle = value.User.Name + " has accepted your friend request";
			}
            else if (value.Type == NotificationType.Reply)
			{
				notificationTitle = value.User.Name + " replied";
			}
            else if (value.Type == NotificationType.Request)
			{
				notificationTitle = value.User.Name + " Request";
			}
            else if (value.Type == NotificationType.Like)
			{
				notificationTitle = value.User.Name + " liked your correction";
			}
            else if (value.Type == NotificationType.Correct)
			{
				notificationTitle = value.User.Name + " has asked you to correct their exercise";
			}
            else
            {
                notificationTitle = value.User.Name + " nothing to show";
            }

			var firstAttributes = new UIStringAttributes
			{
				Font = UIFont.BoldSystemFontOfSize(14f)
			};

            var prettyString = new NSMutableAttributedString(notificationTitle);

            prettyString.SetAttributes(firstAttributes.Dictionary, new NSRange(0, value.User.Name.Length));

			return prettyString;
		}
    }
}
