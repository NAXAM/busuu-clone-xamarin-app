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
using Android.Views.Animations;
using Android.Util;
using Android.Graphics;

using Naxam.Busuu.Droid.Core.Listener;
using Naxam.Busuu.Droid.Core.Utils;
namespace Naxam.Busuu.Droid.Core.Controls
{
    public class NXPlayButton : FrameLayout
    {
        private ImageView imIcon;
        private bool isPlay;
        public bool IsPlay { get { return isPlay; } }
        public event EventHandler<bool> PlayPause;
        public NXPlayButton(Context context) : base(context)
        {
            Init(context);
        }

        public NXPlayButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context);
        }

        public NXPlayButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context);
        }

        public NXPlayButton(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context);
        }


        protected NXPlayButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Init(Context);
        }


        private void Init(Context context)
        {

            if (ChildCount == 1)
                return;
            imIcon = new ImageView(context);
            int pxfromdp = (int)Util.PxFromDp(context, 4);
            FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(-2, -2);
            param.SetMargins(pxfromdp, pxfromdp, pxfromdp, pxfromdp);
            param.Gravity = GravityFlags.Center;
            imIcon.LayoutParameters = param;

            imIcon.SetPadding(pxfromdp, pxfromdp, pxfromdp, pxfromdp);
            imIcon.SetImageResource(Resource.Drawable.ic_play_arrow);
            imIcon.SetBackgroundResource(Resource.Drawable.corner_button);
            if (Android.OS.Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
            {
                imIcon.Elevation = Util.PxFromDp(context, 2);
            }
            AddView(imIcon);
            Click -= NXPlayButton_Click;
            Click += NXPlayButton_Click;
        }

        private void NXPlayButton_Click(object sender, EventArgs e)
        {
            OnClick(true);
        }

        public void OnClick(bool play)
        {
            RotateAnimation rotate = new RotateAnimation(0, !isPlay ? 180 : -180, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
            rotate.Duration = 250;
            rotate.FillAfter = true;
            StartAnimation(rotate);
            rotate.SetAnimationListener(new AnimationListener
            {
                AnimationStart = (start) =>
                {
                    isPlay = !isPlay;
                    imIcon.SetImageResource(isPlay ? Resource.Drawable.ic_play_arrow : Resource.Drawable.ic_pause);
                    if (play)
                        PlayPause?.Invoke(this, isPlay);
                },

                AnimationEnd = (a) =>
                {
                    imIcon.SetImageResource(!isPlay ? Resource.Drawable.ic_play_arrow : Resource.Drawable.ic_pause);
                    ScaleAnimation scale = new ScaleAnimation(1f, 1.05f, 1f, 1.05f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
                    scale.Duration = 250;
                    StartAnimation(scale);

                }
            });

        }
    }
}