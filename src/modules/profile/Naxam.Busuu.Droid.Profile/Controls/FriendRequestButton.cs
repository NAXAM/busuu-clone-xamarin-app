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
using Android.Graphics;

namespace Naxam.Busuu.Droid.Profile.Controls
{
    public class FriendRequestButton : FrameLayout
    {
        ImageView imgIcon;
        TextView txtTitle;
        public FriendRequestButton(Context context) : base(context)
        {
            Init(context);
        }

        public FriendRequestButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context);
        }

        public FriendRequestButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context);
        }

        public FriendRequestButton(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context);
        }

        protected FriendRequestButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Init(Context);
        }

        private void Init(Context context)
        {
            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.request_friend_layout, null);
            imgIcon = view.FindViewById<ImageView>(Resource.Id.imgIcon);
            txtTitle = view.FindViewById<TextView>(Resource.Id.txtTitle);
            FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(-2, -2);
            param.Gravity = GravityFlags.Center;
            AddView(view, param);
        }

        public void SetText(string text, Color color)
        {
            txtTitle.Text = text;
            txtTitle.SetTextColor(color);
        }

        public void SetIcon(int id, Color color)
        {
            imgIcon.SetImageResource(id);
            imgIcon.SetColorFilter(color);
        }

    }
}