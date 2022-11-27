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
using Android.Text;
using static Android.Widget.TextView;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace Naxam.Busuu.Droid.Learning.Views
{
    public class TipDialog : Dialog
    {
        LinearLayout layoutTip;
        TextView txtTip,txtDetail;
        Button btnNext;
        TipModel tip;
        public TipDialog(Context context, TipModel tip) : base(context)
        {
            this.tip = tip;
            
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.RequestFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            base.OnCreate(savedInstanceState);
           
            SetContentView(Resource.Layout.tip_dialog_layout);
            Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));
            Window.SetLayout(ActionBar.LayoutParams.MatchParent, ActionBar.LayoutParams.WrapContent);
            Window.SetGravity(GravityFlags.Center);
            txtTip = FindViewById<TextView>(Resource.Id.txtTip);
            txtDetail = FindViewById<TextView>(Resource.Id.txtDetail);
            btnNext = FindViewById<Button>(Resource.Id.btnNext);
            layoutTip = FindViewById<LinearLayout>(Resource.Id.layoutTip);


            if (Build.VERSION.SdkInt < BuildVersionCodes.N)
            {
                txtDetail.SetText(Html.FromHtml(tip.Detail), BufferType.Normal);
                txtTip.SetText(Html.FromHtml(tip.Tip), BufferType.Normal);
            }
            else
            {
                txtDetail.SetText(Html.FromHtml(tip.Detail, FromHtmlOptions.ModeCompact), BufferType.Normal);
                txtTip.SetText(Html.FromHtml(tip.Tip, FromHtmlOptions.ModeCompact), BufferType.Normal);
            }

            btnNext.Click += (s, e) =>
            {
                this.Dismiss();
            };

            for (int i = 0; i < tip.Samples.Count; i++)
            {
                TextView txtSample = new TextView(Context);
                txtSample.Text = tip.Samples[i];
                txtSample.SetTextColor(Color.ParseColor("#AFB7BD"));
                int padding = (int)Util.Util.PxFromDp(Context, 8);
                txtSample.Gravity = GravityFlags.Center;

                layoutTip.AddView(new View(Context)
                {
                    Background = new ColorDrawable(Color.ParseColor("#D6DEE6"))
                }, new ViewGroup.LayoutParams(-1, padding / 8));
                layoutTip.AddView(txtSample, new ViewGroup.LayoutParams(-1, padding * 6));
                if (i == tip.Samples.Count - 1)
                {
                    layoutTip.AddView(new View(Context)
                    {
                        Background = new ColorDrawable(Color.ParseColor("#D6DEE6"))
                    }, new ViewGroup.LayoutParams(-1, padding / 8));
                }
            }
        }
    }
}