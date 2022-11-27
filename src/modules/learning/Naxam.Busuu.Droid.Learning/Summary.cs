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

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class Summary : MemoriseFragmentBase
    {
        public override event EventHandler<bool> NextClicked;
        public event EventHandler<bool> TryAgainClicked;
        public int Correct;
        public int Total;
        private bool IsCompleted;
        public Summary(int Correct, int Total)
        {
            this.Correct = Correct;
            this.Total = Total;
            if (Correct >= Total - 1)
            {
                IsCompleted = true;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.summary_layout, container, false);
            InitComponent(view);
            return view;
        }
        bool busy;
        TextView txtStatus, txtMark, txtTotal, txtTip, txtResult;
        Button btnNext, btnTryAgain;
        RelativeLayout layoutMark;
        private void InitComponent(View view)
        {
            txtStatus = view.FindViewById<TextView>(Resource.Id.txtStatus);
            txtMark = view.FindViewById<TextView>(Resource.Id.txtMark);
            txtTotal = view.FindViewById<TextView>(Resource.Id.txtTotal);
            txtTip = view.FindViewById<TextView>(Resource.Id.txtStatus);
            txtResult = view.FindViewById<TextView>(Resource.Id.txtResult);
            btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            btnTryAgain = view.FindViewById<Button>(Resource.Id.btnTryAgain);
            layoutMark = view.FindViewById<RelativeLayout>(Resource.Id.layoutMark);

            txtStatus.Text = IsCompleted ? "Rất Tốt" : "Ôi Không";
            txtTotal.Text = "trên " + Total;
            txtResult.Text = IsCompleted ? "Hãy tiếp tục!" : "bạn cần ít nhất " + (Total - 1) + " Điểm để vượt qua";
            btnTryAgain.Visibility = IsCompleted ? ViewStates.Gone : ViewStates.Visible;
            btnTryAgain.Click += (s, e) =>
            {
                TryAgainClicked?.Invoke(btnTryAgain, true);
            };
            btnNext.Click += (s, e) =>
            {
                NextClicked?.Invoke(btnNext, IsCompleted);
            };
            ValueAnimator animator = ValueAnimator.OfInt(0, Correct);
            ScaleAnimation scaleUp = new ScaleAnimation(1.0f, 1.03f, 1.0f, 1.03f, Android.Views.Animations.Dimension.RelativeToSelf, 0.5f, Android.Views.Animations.Dimension.RelativeToSelf, 0.5f);
            scaleUp.Duration = 75;
            ScaleAnimation scaleDown = new ScaleAnimation(1.03f, 1.0f, 1.03f, 1.0f, Android.Views.Animations.Dimension.RelativeToSelf, 0.5f, Android.Views.Animations.Dimension.RelativeToSelf, 0.5f);
            scaleUp.Duration = 75;
            scaleUp.SetAnimationListener(new AnimationListener
            {
                AnimationEnd = (anim) =>
                {
                    if (scaleDown != null)
                        layoutMark.StartAnimation(scaleDown);
                }
            });
            scaleDown.SetAnimationListener(new AnimationListener
            {
                AnimationEnd = (anim) =>
                {
                    if (scaleUp != null)
                        layoutMark.StartAnimation(scaleUp);
                }
            });

            AnimatorSet setAnim = new AnimatorSet();

            int dpDistance = (int)Util.Util.PxFromDp(Context, 2);
            animator.SetDuration(300 * (Correct + 1));


            animator.AddUpdateListener(new AnimatorUpdateListener((anim) =>
            {
                txtMark.Text = anim.AnimatedValue + "";
                if ((int)anim.AnimatedValue == Correct - 1 && !busy)
                {
                    busy = true;
                    layoutMark.StartAnimation(scaleUp);
                }
            }));

            animator.AddListener(new AnimatorListener
            {
                AnimationEndHandle = (anim) =>
                {
                    layoutMark.ClearAnimation();
                    scaleUp.Cancel();
                    scaleDown.Cancel();
                    scaleDown = null;
                    scaleUp = null;
                }
            });

            animator.Start();
        }
    }
}