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
using Android.Graphics;

namespace Naxam.Busuu.Droid.Profile.Utils
{
   public  class TriangleShapeView: Android.Support.V7.Widget.AppCompatImageView
    {
        public TriangleShapeView(Context context): base(context)
        {
            
        }

        public TriangleShapeView(Context context, IAttributeSet attrs, int defStyle):base(context, attrs, defStyle)
        {
            
        }

        public TriangleShapeView(Context context, IAttributeSet attrs): base(context, attrs)
        {
        }

        protected void onDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            int w = Width / 2;
            int h = Height;

            Path path = new Path();
            path.MoveTo(0, 0);
            path.LineTo(h, h);
            path.LineTo(0, h);
            path.MoveTo(0, 0);
            path.Close();

            Paint p = new Paint();
            p.Color=Color.White;

            canvas.DrawPath(path, p);
        }
    }
}