using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.iOS.Core.Converter.LanguageSpeakCell
{
    public class lblLeverTopConverter : MvxValueConverter<bool, nfloat>
    {
        protected override nfloat Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!value)
                return 0;

            return 2;
        }
    }
}