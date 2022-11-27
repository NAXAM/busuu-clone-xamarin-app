using System;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.iOS.Core.Converter
{
	public class AddMinutesValueConverter : MvxValueConverter<int, string>
	{
		protected override string Convert(int value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value <= 1)
			{
				return value.ToString() + " minute";
			}
			else
			{
				return value.ToString() + " minutes";
			}
		}
	}
}
