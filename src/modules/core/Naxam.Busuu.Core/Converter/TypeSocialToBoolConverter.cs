using MvvmCross.Platform.Converters;
using Naxam.Busuu.Core.Models;
using System;
using System.Globalization;

namespace Naxam.Busuu.Core.Converter
{
    public class TypeSocialToBoolConverter : MvxValueConverter<SocialModel.SocialType, bool>
    {
        protected override bool Convert(SocialModel.SocialType value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == SocialModel.SocialType.Writing ? (bool)parameter : !(bool)parameter;
        }
    }
}
