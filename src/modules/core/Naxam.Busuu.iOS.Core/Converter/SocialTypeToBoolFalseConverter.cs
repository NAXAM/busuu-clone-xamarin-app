using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.iOS.Core.Converter
{
    public class SocialTypeToBoolFalseConverter : MvxValueConverter<SocialModel.SocialType, bool>
    {
		protected override bool Convert(SocialModel.SocialType value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == SocialModel.SocialType.Speaking)
				return false;

			return true;
		}
    }
}
