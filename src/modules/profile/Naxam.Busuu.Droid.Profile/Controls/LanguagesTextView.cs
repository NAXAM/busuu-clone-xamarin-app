using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Text;
using Naxam.Busuu.Core.Models;
using Android.Text.Style;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Profile.Controls
{
    public class LanguagesTextView : TextView
    { 
        public LanguagesTextView(Context context) : base(context)
        {
        }

        public LanguagesTextView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public LanguagesTextView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public LanguagesTextView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected LanguagesTextView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public void SetText(IList<LanguageModel> Language)
        {
            SpannableStringBuilder ssb = new SpannableStringBuilder("Speaks ");
            for (int i = 0; i < Language.Count; i++)
            {
                string lang = Language[i].Language;
                SpannableStringBuilder sb = new SpannableStringBuilder(lang);
                sb.SetSpan(new StyleSpan(TypefaceStyle.Bold), 0, lang.Length, SpanTypes.ExclusiveExclusive);
                ssb.Append(i == 0 ? "" : i == Language.Count - 1 ? " and " : ", ");
                ssb.Append(sb);
            }
            SetText(ssb, BufferType.Normal);
        }
    }
}