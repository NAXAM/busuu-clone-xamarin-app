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
using Android.Text.Style;
using Android.Graphics;
using Java.Lang;

namespace Naxam.Busuu.Droid.Learning.Util
{

    public class DrawBackgroundSpan : ReplacementSpan
    {
        public Color textColor, backgroundColor, strokeColor;
        public int strokeWidth;
        public bool HasShadow;
        public override void Draw(Canvas canvas, ICharSequence text, int start, int end, float x, int top, int y, int bottom, Paint paint)
        {
            RectF rect = new RectF(x, top-Util.PxFromDp(Application.Context,1), x + MeasureText(paint, text, start, end), bottom+ Util.PxFromDp(Application.Context, 1));
            //draw background
            paint.Color = backgroundColor;
            if (HasShadow)
            {
                paint.SetShadowLayer(1.0f, 1.0f, 2.0f, Color.ParseColor("#CACBCD"));
            }
            canvas.DrawRect(rect, paint);
            //draw stroke
            if (strokeWidth > 0)
            {
                paint.Color = strokeColor;
                paint.SetShadowLayer(0, 0, 0, Color.Black);
                paint.StrokeWidth = strokeWidth;
                paint.SetStyle(Paint.Style.Stroke);
                canvas.DrawRect(rect, paint);
            }

            // in onDraw(Canvas)   
            //draw text
            paint.SetShadowLayer(0, 0, 0, Color.Black);
            paint.Color = textColor;
            canvas.DrawText(text, start, end, x, y, paint);
        }

        public override int GetSize(Paint paint, ICharSequence text, int start, int end, Paint.FontMetricsInt fm)
        {
            return (int)System.Math.Round(paint.MeasureText(text, start, end));

        }
        private float MeasureText(Paint paint, ICharSequence text, int start, int end)
        {
            return paint.MeasureText(text, start, end);
        }

    }
}