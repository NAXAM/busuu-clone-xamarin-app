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
using Android.Util;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;
using Android.Media;
using Java.IO;
using Com.Longtailvideo.Jwplayer;
using Com.Longtailvideo.Jwplayer.Configuration;
using Com.Longtailvideo.Jwplayer.Media.Playlists;
 
using Com.Google.Android.Exoplayer;

namespace Naxam.Busuu.Droid.Learning.Util
{
    public class Util
    {
        public static void ClearBackStack(Android.Support.V4.App.FragmentManager manager)
        {

            if (manager.BackStackEntryCount > 0)
            {
                var first = manager.GetBackStackEntryAt(0);
                manager.PopBackStack(first.Id, Android.Support.V4.App.FragmentManager.PopBackStackInclusive);
            }
        }
        public static float DpFromPx(Context context, float px)
        {
            return px / context.Resources.DisplayMetrics.Density;
        }

        public static float PxFromDp(Context context, float dp)
        {
            return dp * context.Resources.DisplayMetrics.Density;
        }
    }

    public class BackgroundUtil
    {
        public static Drawable BackgroundRound(Context context, int radius, Color color)
        {
            PaintDrawable background = new PaintDrawable(color);
            background.Shape = new RectShape();
            int radiusPX = (int)Util.PxFromDp(context, radius);
            background.SetCornerRadius(radiusPX);

            return background;
        }
    }
}