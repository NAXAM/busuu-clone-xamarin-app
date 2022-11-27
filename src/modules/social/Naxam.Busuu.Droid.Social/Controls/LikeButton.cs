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
using Naxam.Busuu.Droid.Core.Utils;
using Android.Graphics;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Animation;
using Naxam.Busuu.Droid.Core.Listener;
using Android.Views.Animations;

namespace Naxam.Busuu.Droid.Social.Controls
{

    public class LikeButton : FrameLayout
    {
        public event EventHandler<bool> StateChange;
        bool _active;
        public bool Active
        {
            set
            {
                _active = value;
                if (value)
                {
                    view.SetBackgroundResource(Resource.Drawable.background_like_blue);
                    imgImage.SetColorFilter(Color.White);
                    txtText.SetTextColor(Color.White);
                }
                else
                {
                    view.SetBackgroundResource(Resource.Drawable.background_like);
                    imgImage.SetColorFilter(Color.ParseColor("#A7B0B7"));
                    txtText.SetTextColor(Color.ParseColor("#778086"));
                }
            }
            get
            {
                return _active;
            }
        }

        public int IconResource { get; }
        public string Text { get; }

        View view;
        TextView txtText;
        ImageView imgImage; 
        Drawable drawable;
        public LikeButton(Context context) : base(context)
        {
            Init(context, null);
        }

        public LikeButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs); 
        }

        public LikeButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs); 
        }

        public LikeButton(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context, attrs); 
        }

        protected LikeButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Init(Context, null);
        }


        public void Init(Context context, IAttributeSet attrs)
        {
            RemoveAllViews();
            view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.like_button_layout, null);
            txtText = view.FindViewById<TextView>(Resource.Id.txtText);
            imgImage = view.FindViewById<ImageView>(Resource.Id.imgImage);
            if (attrs == null)
            {
                if (drawable == null)
                {
                    imgImage.SetImageResource(IconResource == 0 ? Resource.Drawable.ic_comment_thumbsup : IconResource);
                }
                else
                {
                    imgImage.SetImageDrawable(drawable);
                }
            }
            else
            {
                TypedArray a = context.ObtainStyledAttributes(attrs, Resource.Styleable.LikeButton);
                drawable = a.GetDrawable(Resource.Styleable.LikeButton_lb_icon);
                imgImage.SetImageDrawable(drawable);
            }
            SetAtribute();
            view.Click += View_Click;
            var margin = (int)Util.PxFromDp(context, 4);
            var param = new FrameLayout.LayoutParams(-2, -2);
            param.SetMargins(margin, margin, margin, margin);
            param.Gravity = GravityFlags.Center;
            AddView(view, param);
        }

        private void View_Click(object sender, EventArgs e)
        {
            if (Active)
                return;
            Active = true;
            SetAtribute();
            Animation anim = new ScaleAnimation(1f, 1.1f, 1f, 1.2f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
            anim.FillAfter = false;
            anim.Duration = 100;
            view.StartAnimation(anim);
            StateChange?.Invoke(this, true);
        }

        private void SetAtribute()
        {
            if (Active)
            {
                view.SetBackgroundResource(Resource.Drawable.background_like_blue);
                imgImage.SetColorFilter(Color.White);
                txtText.SetTextColor(Color.White);
            }
            else
            {
                UnActive();
            }
        }
        public void SetIconResource(int resource)
        {
            imgImage.SetImageResource(resource);
            SetAtribute();
        }

        public void SetIconDrawable(Drawable drawable)
        {
            imgImage.SetImageDrawable(drawable);
            SetAtribute();

        }
        public void SetIconBitmap(Bitmap bitmap)
        {
            imgImage.SetImageBitmap(bitmap);
            SetAtribute();
        }

        public void SetText(string text)
        {
            txtText.Text = text;
        }

        public void UnActive()
        {
            Active = false;
            StateChange?.Invoke(this, false);
            view.SetBackgroundResource(Resource.Drawable.background_like);
            imgImage.SetColorFilter(Color.ParseColor("#A7B0B7"));
            txtText.SetTextColor(Color.ParseColor("#778086"));
        }
    }
}