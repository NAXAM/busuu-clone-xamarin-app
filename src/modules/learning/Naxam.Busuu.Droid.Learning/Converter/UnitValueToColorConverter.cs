using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.UI;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Learning.Converter
{
    public class UnitValueToColorConverter : MvxValueConverter<bool, Color>
    {
        protected override Color Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? Color.ParseColor("#8074B825") : Color.ParseColor("#80EE6253");
        }
    }
}