using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Naxam.Busuu.Droid.Core.Utils;
using Android.Graphics;
using Android.Views.Animations;

namespace Naxam.Busuu.Droid.Social.Controls
{
    public class AddFriendButton : FrameLayout
    {
        bool _active;
        public bool Active
        {
            set
            {
                _active = value;
                img.SetColorFilter(value ? Color.ParseColor("#37A7F3") : Color.ParseColor("#B0B7BC"));
            }
            get
            {
                return _active;
            }
        }
        public event EventHandler<bool> AddFriend;
        public AddFriendButton(Context context) : base(context)
        {
            Init(context);
        }

        public AddFriendButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context);
        }

        public AddFriendButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context);
        }

        public AddFriendButton(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context);
        }

        protected AddFriendButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Init(Context);
        }
        ImageView img;
        private void Init(Context context)
        {
            var margin = (int)Util.PxFromDp(context, 4);
            img = new ImageView(context);



            img.SetImageResource(Resource.Drawable.add_user);
            img.SetBackgroundResource(Resource.Drawable.background_add_friend);
            img.SetPadding(margin * 3 / 2, margin * 3 / 2, margin * 3 / 2, margin * 3 / 2);
            img.SetColorFilter(Active ? Color.ParseColor("#37A7F3") : Color.ParseColor("#B0B7BC"));

            var param = new FrameLayout.LayoutParams(margin * 12, margin * 7);
            param.SetMargins(margin, margin, margin, margin);
            param.Gravity = GravityFlags.Center;
            AddView(img, param);

            Click += AddFriendButton_Click;
        }

        private void AddFriendButton_Click(object sender, EventArgs e)
        {
            if (Active)
                return;
            Active = true;
            img.SetColorFilter(Active ? Color.ParseColor("#37A7F3") : Color.ParseColor("#B0B7BC"));
            Animation anim = new ScaleAnimation(1f, 1.1f, 1f, 1.2f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
            anim.FillAfter = false;
            anim.Duration = 100;
            img.StartAnimation(anim);
            AddFriend?.Invoke(this, true);
        }
    }
}