using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.iOS.Core.Converter.ReviewTableViewCell
{
	public class IsCheckToViewSampleTopConverter : MvxValueConverter<bool, nfloat>
	{
		protected override nfloat Convert(bool value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!value)
				return 0;

			return 14;
		}
	}
}
