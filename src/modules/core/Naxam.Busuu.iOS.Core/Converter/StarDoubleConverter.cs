using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.iOS.Core.Converter
{
    public class StarDoubleConverter : MvxValueConverter<double, double>
    {
        protected override double Convert(double value, Type targetType, object parameter, CultureInfo culture)
		{
            return System.Convert.ToDouble(String.Format("{0:0.#}", (value / 2) * 10));
		}
    }
}
