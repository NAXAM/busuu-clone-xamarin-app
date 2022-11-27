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
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Droid.Core.Utils;
using Android.Graphics;
using Com.Bumptech.Glide;
using Naxam.Busuu.Droid.Core.Transform;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Profile.Controls
{
    public class FriendsImageView : LinearLayout
    { 
        int max;
        bool render;

        public IList<UserModel> Friends
        {
            set; get;
        }
        public FriendsImageView(Context context) : base(context)
        {
        }

        public FriendsImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public FriendsImageView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public FriendsImageView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected FriendsImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            base.OnLayout(changed, left, top, right, bottom);
            int imageWidth = (int)Util.PxFromDp(this.Context, 40);
            int realWidth = imageWidth + imageWidth / 6;
            max = realWidth > 0 ? (right - left) / realWidth : 0;
            Init();
        }

        public void Init()
        {
            if (render)
                return;

            var options = new RequestOptions()
                .Transform(new CircleTransform(this.Context));

            RemoveAllViews();
            render = true;
            int imageWidth = (int)Util.PxFromDp(this.Context, 40);
            int step = Friends.Count < max ? Friends.Count : max;
            for (int i = 0; i < step; i++)
            {
                LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(imageWidth, imageWidth);
                param.Gravity = GravityFlags.Center;
                param.LeftMargin = imageWidth / 6;
                if (i == max - 1)
                {
                    TextView text = new TextView(Context);
                    text.Gravity = GravityFlags.Center;
                    text.SetTextColor(Color.White);
                    text.Background = BackgroundUtil.BackgroundRound(Context, imageWidth, Color.ParseColor("#38A9F6"));
                    text.Text = "+ " + (Friends.Count - max + 1);
                    AddView(text, param);
                }
                else
                {
                    ImageView avatar = new ImageView(Context);
                    avatar.Background = BackgroundUtil.BackgroundRound(Context, imageWidth, Color.White);
                    avatar.SetImageResource(Resource.Drawable.usa_flag);
                    if (string.IsNullOrEmpty(Friends[i].Photo))
                    {
                        Glide.With(Context).Load(Resource.Drawable.user_avatar_placeholder).Apply(options).Into(avatar);
                    }
                    else
                    {
                        Glide.With(Context).Load(Friends[i].Photo).Apply(options).Into(avatar);
                    }
                    AddView(avatar, param);
                }

            }
            ForceLayout();
            Parent.RequestLayout();
        }
        

        protected override void Dispose(bool disposing)
        {
            render = false;
            base.Dispose(disposing);
        }
    }
}