using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.iOS.Core.Converter.ReviewTableViewCell
{
	public class IsFavoriteToImageStarConverter : MvxValueConverter<bool, string>
	{
		protected override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value)
			{
				return "res:Stars/yellow_star_d.png";
			}

			return "res:Stars/grey_star2.png";
		}
	}
}
