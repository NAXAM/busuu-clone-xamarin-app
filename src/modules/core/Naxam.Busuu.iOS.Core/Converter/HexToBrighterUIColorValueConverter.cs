using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using UIKit;

namespace Naxam.Busuu.iOS.Core.Converter
{
    public class HexToBrighterUIColorValueConverter : MvxValueConverter<string, UIColor>
    {
        protected override UIColor Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
			//Remove # if present
			if (value.IndexOf('#') != -1)
				value = value.Replace("#", "");

            int r = 0;
            int g = 0;
            int b = 0;
            int a = 0;
			if (value.Length == 4)
			{
				//#RGBA
				r = int.Parse(value[0].ToString() + value[0].ToString(), NumberStyles.AllowHexSpecifier);
				g = int.Parse(value[1].ToString() + value[1].ToString(), NumberStyles.AllowHexSpecifier);
				b = int.Parse(value[2].ToString() + value[2].ToString(), NumberStyles.AllowHexSpecifier);
				a = int.Parse(value[3].ToString() + value[3].ToString(), NumberStyles.AllowHexSpecifier);
				return UIColor.FromRGBA(r, g, b, a);
			}

			if (value.Length == 8)
			{
				//#RGBA
				r = int.Parse(value.Substring(0, 2), NumberStyles.AllowHexSpecifier);
				g = int.Parse(value.Substring(2, 2), NumberStyles.AllowHexSpecifier);
				b = int.Parse(value.Substring(4, 2), NumberStyles.AllowHexSpecifier);
				a = int.Parse(value.Substring(6, 2), NumberStyles.AllowHexSpecifier);
				return UIColor.FromRGBA(r, g, b,a);
			}

			if (value.Length == 6)
			{
				//#RRGGBB
				r = int.Parse(value.Substring(0, 2), NumberStyles.AllowHexSpecifier);
				g = int.Parse(value.Substring(2, 2), NumberStyles.AllowHexSpecifier);
				b = int.Parse(value.Substring(4, 2), NumberStyles.AllowHexSpecifier);
			}
			else if (value.Length == 3)
			{
				//#RGB
				r = int.Parse(value[0].ToString() + value[0].ToString(), NumberStyles.AllowHexSpecifier);
				g = int.Parse(value[1].ToString() + value[1].ToString(), NumberStyles.AllowHexSpecifier);
				b = int.Parse(value[2].ToString() + value[2].ToString(), NumberStyles.AllowHexSpecifier);
			}
            return UIColor.FromRGB((r+ 0.6f  * (255 - r))/255,(g + 0.6f * (255 - g)) / 255, (b + 0.6f * (255 - b)) / 255);
        }
    }
}
