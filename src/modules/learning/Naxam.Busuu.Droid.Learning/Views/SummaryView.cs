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
using Android.Animation;
using static Android.Resource;
using Java.Lang;
using Android.Views.Animations;
using Naxam.Busuu.Droid.Learning.Control.Memo;
using Naxam.Busuu.Droid.Core.Listener;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;
using Naxam.Busuu.Learning.ViewModels;
using Naxam.Busuu.Droid.Core;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Content.Res;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Label = "", Theme = "@style/AppTheme.NoActionBar", ConfigurationChanges = Android.Content.PM.ConfigChanges.KeyboardHidden| Android.Content.PM.ConfigChanges.Orientation| Android.Content.PM.ConfigChanges.ScreenSize)]
    public class SummaryView : MvxAppCompatActivity<SummaryViewModel>
    {
        bool busy;
        TextView txtMark;
        RelativeLayout layoutMark;

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.summary_layout);
            InitComponent();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            SetContentView(Resource.Layout.summary_layout);
            InitComponent();
            base.OnConfigurationChanged(newConfig);
        }

        private void InitComponent()
        {
            txtMark = FindViewById<TextView>(Resource.Id.txtMark);
            layoutMark = FindViewById<RelativeLayout>(Resource.Id.layoutMark);

            float distance = Util.Util.PxFromDp(this, 2);
            ValueAnimator animator = ValueAnimator.OfInt(0, ViewModel.Correct);
            AnimatorSet mAnimatorSet = new AnimatorSet();
            var animx = ObjectAnimator.OfFloat(layoutMark, "TranslationX", distance, -distance, 0);
            animx.RepeatCount = 4;
            animx.RepeatMode = ValueAnimatorRepeatMode.Reverse;
            mAnimatorSet.Play(animx);


            mAnimatorSet.SetDuration(50);


            animator.SetDuration(500 * (ViewModel.Correct + 1));


            animator.AddUpdateListener(new AnimatorUpdateListener((anim) =>
            {
                if (txtMark.Text != anim.AnimatedValue + "")
                {
                    txtMark.Text = anim.AnimatedValue + "";
                    busy = false;
                }

                if (!busy && (int)anim.AnimatedValue > 0)
                {
                    busy = true;
                    mAnimatorSet.Start();

                }
            }));

            animator.AddListener(new AnimatorListener
            {
                AnimationEndHandle = (anim) =>
                {
                    layoutMark.ClearAnimation();
                }
            });

            animator.Start();
        }
         

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }
        
    }
}