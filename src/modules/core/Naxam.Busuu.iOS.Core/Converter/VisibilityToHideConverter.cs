using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.iOS.Core.Converter
{
    public class VisibilityToHideConverter : MvxValueConverter<bool, bool>
    {
		protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo culture)
		{
		  return !value;
		}
	}
}
