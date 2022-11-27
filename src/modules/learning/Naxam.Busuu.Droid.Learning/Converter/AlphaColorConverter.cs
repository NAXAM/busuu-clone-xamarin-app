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
using Android.Graphics;
using Java.Lang;

namespace Naxam.Busuu.Droid.Learning.Converter
{
    public class AlphaColorConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.ParseColor((string)value);
            System.Diagnostics.Debug.WriteLine("color------------" + value.ToString());
            var red = Color.GetRedComponent(color);
            var blue = Color.GetBlueComponent(color);
            var green = Color.GetGreenComponent(color);
            var alpha = Integer.ParseInt(parameter.ToString());
            Color temp = Color.Argb(alpha, red, green, blue);
            return temp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}