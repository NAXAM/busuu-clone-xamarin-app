using System;
using UIKit;
using CoreGraphics;
using System.Globalization;

namespace Naxam.Busuu.iOS.Core.Extensions
{
    public static class CGColorExtensions
    {
        public static UIColor ToUIColor(this CGColor color)
        {
            return UIColor.FromCGColor(color);
        }
    }

    public static class ColorUtils
    {
        public static UIColor ColorFromHex(string value)
        {
            //Remove # if present
            if (value.IndexOf('#') != -1)
                value = value.Replace("#", "");

            int red = 0;
            int green = 0;
            int blue = 0;
            int alpha = 0;

            if (value.Length == 4)
            {
                //#RGBA
                red = int.Parse(value[0].ToString() + value[0].ToString(), NumberStyles.AllowHexSpecifier);
                green = int.Parse(value[1].ToString() + value[1].ToString(), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(value[2].ToString() + value[2].ToString(), NumberStyles.AllowHexSpecifier);
                alpha = int.Parse(value[3].ToString() + value[3].ToString(), NumberStyles.AllowHexSpecifier);
                return UIColor.FromRGBA(red, green, blue, alpha);
            }

            if (value.Length == 8)
            {
                //#RGBA
                red = int.Parse(value.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                green = int.Parse(value.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(value.Substring(4, 2), NumberStyles.AllowHexSpecifier);
                alpha = int.Parse(value.Substring(6, 2), NumberStyles.AllowHexSpecifier);
                return UIColor.FromRGBA(red, green, blue, alpha);
            }

            if (value.Length == 6)
            {
                //#RRGGBB
                red = int.Parse(value.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                green = int.Parse(value.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(value.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            }
            else if (value.Length == 3)
            {
                //#RGB
                red = int.Parse(value[0].ToString() + value[0].ToString(), NumberStyles.AllowHexSpecifier);
                green = int.Parse(value[1].ToString() + value[1].ToString(), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(value[2].ToString() + value[2].ToString(), NumberStyles.AllowHexSpecifier);
            }
            return UIColor.FromRGB(red, green, blue);
        }
    }

    public static class UIColorExtensions
    {
        public static UIColor BrighterColor(this UIColor color)
        {
            nfloat r = 0, g = 0, b = 0, a = 1;
            color.GetRGBA(out r, out g, out b, out a);
            return UIColor.FromRGBA(
                r + 0.6f * (1 - r),
                 g + 0.6f * (1 - g),
                 b + 0.6f * (1 - b),
                a
            );
        }
    }
}