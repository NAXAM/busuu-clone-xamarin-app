using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Naxam.Busuu.Core.Converter
{
    public class IsMatchPatternBase64Converter : MvxValueConverter<string, bool>
    {
        protected override bool Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] textAsBytes = System.Convert.FromBase64String(parameter.ToString());
            string decoded = Encoding.UTF8.GetString(textAsBytes, 0, textAsBytes.Length);
            Regex regex = new Regex(decoded);
            return regex.IsMatch(value);
        }
    }

    public class EmailValidConverter : MvxValueConverter<string, bool>
    {
        protected override bool Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            Regex regex = new Regex("^[a-zA-Z0-9-_\\.]+@[a-z0-9]+\\.[a-z]{2,4}$");
            return regex.IsMatch(value);
        }
    }

    public class PhoneValidConverter : MvxValueConverter<string, bool>
    {
        protected override bool Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            Regex regex = new Regex("^+?[0-9]{9,13}$");
            return regex.IsMatch(value);
        }
    }

    public class EmailPhoneValidConverter : MvxValueConverter<string, bool>
    {
        protected override bool Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            Regex regex = new Regex("^[a-zA-Z0-9-_\\.]+@[a-z0-9]+\\.[a-z]{2,4}$");
            bool matchEmail = regex.IsMatch(value.Trim());
            Regex regex2 = new Regex("^+?[0-9]{9,13}$");
            bool matchPhone = regex2.IsMatch(value.Trim());
            return matchEmail || matchPhone;
        }
    }

}
