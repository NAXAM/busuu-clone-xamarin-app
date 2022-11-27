using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using MvvmCross.Platform.UI;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Core.Converters
{
    public class BoolToColorConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool check = (bool)value;
            return check ?   Color.ParseColor("#FFFFFF") : Color.ParseColor("#FFFE00");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
