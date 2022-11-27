using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.iOS.Core.Converter.ReviewTableViewCell
{
	public class StrengthLevelToImageConverter : MvxValueConverter<int, string>
	{
		protected override string Convert(int value, Type targetType, object parameter, CultureInfo culture)
		{
			switch (value)
			{
				case 1:
					return "res:strength_1";
				case 2:
					return "res:strength_2";
				case 3:
					return "res:strength_3";
				case 4:
					return "res:strength_4";
				default:
					return "res:strength_0";
			}
		}
	}

}
