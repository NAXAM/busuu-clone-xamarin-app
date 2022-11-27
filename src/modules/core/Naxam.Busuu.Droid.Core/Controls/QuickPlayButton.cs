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
using Android.Animation;
using Naxam.Busuu.Droid.Core.Listener;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Media;

namespace Naxam.Busuu.Droid.Core.Controls
{
    public class QuickPlayButton : LinearLayout
    {
        public int Icon { set; get; }
        public string AudioPath { set; get; }
        bool busy;

        public QuickPlayButton(Context context, int Icon) : base(context)
        {
            this.Icon = Icon;
            Init(context, null);
        }

        public QuickPlayButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public QuickPlayButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        public QuickPlayButton(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context, attrs);
        }

        protected QuickPlayButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }

        private void Init(Context context, IAttributeSet attrs)
        {

            RemoveAllViews();
            View view = LayoutInflater.From(context).Inflate(Resource.Layout.quick_play_button_layout, null);
            ImageView imgIcon = view.FindViewById<ImageView>(Resource.Id.imgIcon);

            if (attrs == null)
            {
                imgIcon.SetImageResource(Icon == 0 ? Resource.Drawable.play_btn : Icon);
            }
            else
            {
                TypedArray a = context.ObtainStyledAttributes(attrs, Resource.Styleable.QuickPlayButton);
                Drawable drawable = a.GetDrawable(Resource.Styleable.QuickPlayButton_qpb_icon);
                imgIcon.SetImageDrawable(drawable);
            }
            imgIcon.Click += (s, e) =>
            {
                if (busy || string.IsNullOrWhiteSpace(AudioPath) || string.IsNullOrEmpty(AudioPath))
                    return;
                View v = ((ViewGroup)((ImageView)s).Parent).FindViewById(Resource.Id.viewBackground);
                v.Visibility = ViewStates.Visible;
                ObjectAnimator anim = ObjectAnimator.OfFloat(v, "Alpha", 1f, 0);

                anim.AddListener(new AnimatorListener
                {
                    AnimationStartHandle = (aims) =>
                    {
                        busy = true;
                    },
                    AnimationEndHandle = (aims) =>
                    {
                        busy = false;
                        v.Visibility = ViewStates.Gone;
                    }
                });
                anim.SetDuration(200);
                anim.Start();

                try
                {
                    MediaPlayer media = new MediaPlayer();
                    media.SetDataSourceAsync(AudioPath);
                    media.PrepareAsync();
                    media.Start();
                }
                catch { }
            };
            AddView(view, new ViewGroup.LayoutParams(-1, -1));
        }
    }
}