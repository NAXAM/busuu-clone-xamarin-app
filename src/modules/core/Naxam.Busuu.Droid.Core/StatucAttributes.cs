using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Core
{
    public class ConstantAttributes
    {
        public const string ColorHexCorrect = "#74B826";
        public const string ColorHexIncorrect = "#E54532";

        public static Color ColorCorrect = Color.ParseColor("#74B826");
        public static Color ColorIncorrect = Color.ParseColor("#E54532");
        public static Color ColorPrimary = Color.ParseColor("#38A9F6");
        public static Color ColorPrimaryLight = Color.ParseColor("#CDEBF9");
    }
}