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
using Naxam.Busuu.Learning.Models;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Text;
using static Android.Widget.TextView;
using Naxam.Busuu.Droid.Learning.Control.Memo;

namespace Naxam.Busuu.Droid.Learning.Control.Vocabulary
{
    public class TipView : MemoBaseView
    {
        LinearLayout layoutTip;
        TextView txtTip;
        Button btnNext; 

        public TipView(Context context, UnitModel Item) : base(context)
        {
            this.Item = Item;
            Init(context);
        }

        private void Init(Context context)
        {
            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.tip_layout, null);
            txtTip = view.FindViewById<TextView>(Resource.Id.txtTip);
            btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            layoutTip = view.FindViewById<LinearLayout>(Resource.Id.layoutTip);
            btnNext.Click += (s, e) =>
            {
            };
            if (Build.VERSION.SdkInt < BuildVersionCodes.N)
            {
                txtTip.SetText(Html.FromHtml(Item.Tip.Tip), BufferType.Normal);
            }
            else
            {
                txtTip.SetText(Html.FromHtml(Item.Tip.Tip, FromHtmlOptions.ModeCompact), BufferType.Normal);
            }

            for (int i = 0; i < Item.Tip.Samples.Count; i++)
            {
                TextView txtSample = new TextView(Context);
                txtSample.Text = Item.Tip.Samples[i];
                txtSample.SetTextColor(Color.ParseColor("#AFB7BD"));
                int padding = (int)Util.Util.PxFromDp(Context, 8);
                txtSample.Gravity = GravityFlags.Center;

                layoutTip.AddView(new View(Context)
                {
                    Background = new ColorDrawable(Color.ParseColor("#D6DEE6"))
                }, new ViewGroup.LayoutParams(-1, padding / 8));
                layoutTip.AddView(txtSample, new ViewGroup.LayoutParams(-1, padding * 6));
                if (i == Item.Tip.Samples.Count - 1)
                {
                    layoutTip.AddView(new View(Context)
                    {
                        Background = new ColorDrawable(Color.ParseColor("#D6DEE6"))
                    }, new ViewGroup.LayoutParams(-1, padding / 8));
                }
            }
            AddView(view,new ViewGroup.LayoutParams(-1,-1));
        }
    }
}