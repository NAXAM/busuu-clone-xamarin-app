using System;
using Android.Views.Animations;
using static Android.Views.Animations.Animation;
using Android.Animation;
using static Android.Animation.Animator;

namespace Naxam.Busuu.Droid.Core.Listener
{
    public class AnimatorListener : Java.Lang.Object, IAnimatorListener
    {
        public Action<Animator> AnimationCancelHandle;
        public Action<Animator> AnimationEndHandle;
        public Action<Animator> AnimationStartHandle;

        public void OnAnimationCancel(Animator animation)
        {
            AnimationCancelHandle?.Invoke(animation);
        }

        public void OnAnimationEnd(Animator animation)
        {
            AnimationEndHandle?.Invoke(animation);
        }

        public void OnAnimationRepeat(Animator animation)
        {
        }

        public void OnAnimationStart(Animator animation)
        {
            AnimationStartHandle?.Invoke(animation);
        }
    }
}