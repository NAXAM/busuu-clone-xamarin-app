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
using Com.Bumptech.Glide;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace Naxam.Busuu.Droid.Profile.Dialogs
{
    public class UserPhotoDialog : Dialog
    {
        private ImageView imgClose, imgAvatar;
        string source;
        public UserPhotoDialog(Context context,string source):base(context)
        {
            this.source = source;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.RequestFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.user_photo_layout);
            Window.SetBackgroundDrawable(new ColorDrawable(Color.Black));
            Window.SetLayout(ActionBar.LayoutParams.MatchParent, ActionBar.LayoutParams.MatchParent);
            Window.SetGravity(GravityFlags.Center);
            imgAvatar = FindViewById<ImageView>(Resource.Id.imgPhoto);
            Glide.With(Context).Load("https://scontent.fhan2-1.fna.fbcdn.net/v/t1.0-9/20246173_1323543431092186_392776523060866838_n.jpg?oh=d1fb3da1a138d710152f283e03c8a21c&oe=59EEA441").Into(imgAvatar);
           // Glide.With(Context).Load(source).Into(imgAvatar);
            imgClose = FindViewById<ImageView>(Resource.Id.imgClose);
            imgClose.Click += (s, e) => {
                Dismiss();
            };
            imgAvatar.Click += (s, e) => {
                Dismiss();
            };
        }

        
  

    }
}