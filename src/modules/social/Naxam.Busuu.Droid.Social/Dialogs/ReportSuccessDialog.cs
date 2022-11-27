
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Social.Dialogs
{
    public class ReportSuccessDialog : Dialog
    {
        Button btnOk;
        public ReportSuccessDialog(Context context) : base(context)
        {

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.RequestFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.report_dialog_success_layout);
            Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));
            Window.SetLayout(ActionBar.LayoutParams.MatchParent, ActionBar.LayoutParams.WrapContent);
            Window.SetGravity(GravityFlags.Center);
            btnOk = FindViewById<Button>(Resource.Id.btnOk);
            btnOk.Click += (s, e) =>
            {
                Dismiss();
            };
        }
    }
}