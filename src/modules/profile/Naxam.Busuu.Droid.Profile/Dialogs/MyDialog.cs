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

namespace Naxam.Busuu.Droid.Profile.Utils
{
    public class MyDialog : Dialog
    {
        TextView txtClose;
        public MyDialog(Context c) :base(c)
        {
            this.RequestWindowFeature((int)WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.DownloadDialogActivity);
            txtClose = FindViewById<TextView>(Resource.Id.txtClose);
            this.Show();
            Window window = this.Window;
            window.SetGravity(GravityFlags.Center);
            window.SetLayout(ActionBar.LayoutParams.MatchParent, ActionBar.LayoutParams.WrapContent);
            txtClose.Click += (s, e) => {
                Dismiss();
            };

        }


    }
}