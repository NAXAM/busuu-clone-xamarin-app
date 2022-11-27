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
using Android.Graphics.Drawables;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Social.Dialogs
{
    public class ReportDialog : Dialog
    {
        Button btnCancel;
        TextView txtSpam, txtNotHelpfull, txtHarmfull;
        public ReportDialog(Context context) : base(context)
        {

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.RequestFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.report_dialog_layout);
            Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));
            Window.SetLayout(ActionBar.LayoutParams.MatchParent, ActionBar.LayoutParams.WrapContent);
            Window.SetGravity(GravityFlags.Center);
            btnCancel = FindViewById<Button>(Resource.Id.btnCancel);
            txtSpam = FindViewById<TextView>(Resource.Id.txtSpam);
            txtNotHelpfull = FindViewById<TextView>(Resource.Id.txtNotHelpfull);
            txtHarmfull = FindViewById<TextView>(Resource.Id.txtHarmfull);
            txtSpam.Click += TxtSpam_Click;
            txtNotHelpfull.Click += TxtSpam_Click;
            txtHarmfull.Click += TxtSpam_Click;
            btnCancel.Click += (s, e) =>
            {
                Dismiss();
            };
        }

        private void TxtSpam_Click(object sender, EventArgs e)
        {
            Dismiss();
            ReportSuccessDialog report = new ReportSuccessDialog(Context);
            report.Show();
        }
    }
}