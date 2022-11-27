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
using Android.Text;
using static Android.Widget.TextView;

namespace Naxam.Busuu.Droid.Learning.Util
{
    public class ClickableSpanNoUnderline : ClickableSpan
    {

        public event EventHandler<object> Clicked;

        public ClickableSpanNoUnderline()
        {

        }

        public override void UpdateDrawState(TextPaint ds)
        {
            base.UpdateDrawState(ds);
            ds.UnderlineText = false;
        }

        public override void OnClick(View widget)
        {
            TextView txtView = (TextView)widget;
            SpannableString s = new SpannableString(txtView.TextFormatted);
            int start = s.GetSpanStart(this);
            int end = s.GetSpanEnd(this);
            s.SetSpan(new DrawBackgroundSpan {
                backgroundColor = Color.ParseColor("#CFEAFC"),
                strokeColor = Color.ParseColor("#42ACF6"),
                strokeWidth = 4
            }, start, end, SpanTypes.InclusiveInclusive);
           // txtView.SetText(s,BufferType.Normal);
            Clicked?.Invoke(txtView, this);
        }
    }
}