using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.iOS.Core.Converter
{
    public class HowDidTextConverter : MvxValueConverter<string, string>
    {
		protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
		{
			return "How did " + value + " do?";
		}
    }
}
