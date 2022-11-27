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
using Android.Support.V4.View;

namespace Naxam.Busuu.Droid.Core.Transform
{
    public class ForegroundToBackgroundTransformer : Java.Lang.Object,ViewPager.IPageTransformer
    {
        private static float MIN_SCALE = 0.85f;
        private static float MIN_ALPHA = 0.5f;

        public void TransformPage(View view, float position)
        {
            int pageWidth = view.Width;
            int pageHeight = view.Height;

            if (position < -1)
            { // [-Infinity,-1)
              // This page is way off-screen to the left.
                view.Alpha = 0;

            }
            else if (position <= 1)
            { // [-1,1]
                if (view.Tag?.Equals("1")==true)
                {

                    // Modify the default slide transition to shrink the page as well
                    float scaleFactor = Math.Max(MIN_SCALE, 1 - Math.Abs(position));
                    float vertMargin = pageHeight * (1 - scaleFactor);
                    float horzMargin = pageWidth * (1 - scaleFactor);
                    if (position < 0)
                    {
                        view.TranslationX = horzMargin - vertMargin;
                    }
                    else
                    {
                        view.TranslationX = -horzMargin + vertMargin;
                    }

                    // Scale the page down (between MIN_SCALE and 1)
                    view.ScaleX = scaleFactor;
                    view.ScaleY = scaleFactor;
                }


            }
            else
            { // (1,+Infinity]
              // This page is way off-screen to the right.
                view.Alpha = 0;
            }
        }

       
    }
}