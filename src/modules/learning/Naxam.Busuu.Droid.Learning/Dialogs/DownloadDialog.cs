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
using Com.Bumptech.Glide;
using Naxam.Busuu.Droid.Core.Transform;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Learning.Dialogs
{
    public class DownloadDialog : Dialog
    {
        string Url;
        public DownloadDialog(Context context, string Url) : base(context)
        {
            this.Url = Url;
            Init();
        }

        public DownloadDialog(Context context) : base(context)
        {
        }

        private void Init()
        {
            this.RequestWindowFeature((int)WindowFeatures.NoTitle);
            View view = LayoutInflater.Inflate(Resource.Layout.download_dialog, null);
            ImageView myImage = view.FindViewById<ImageView>(Resource.Id.imgLesson);
            TextView txtClose = view.FindViewById<TextView>(Resource.Id.txtClose);
            txtClose.Click += (s, e) =>
            {
                this.Dismiss();
            };

            var options = new RequestOptions().Transform(new CircleTransform(this.Context));
            Glide.With(view.Context).Load(Url).Apply(options).Into(myImage);
            this.SetContentView(view);
            this.Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));

        }
    }
}