using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Core.Converter
{
    public class NotifyTypeToVisibleItemConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ViewType viewType = (ViewType)value;
            if (viewType == ViewType.Request)
            {
                return false;

            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
