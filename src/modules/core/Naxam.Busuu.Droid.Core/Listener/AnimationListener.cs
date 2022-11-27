using System;
using Android.Views.Animations;
using static Android.Views.Animations.Animation;

namespace Naxam.Busuu.Droid.Core.Listener
{
    public class AnimationListener : Java.Lang.Object, IAnimationListener
    {
        public Action<Animation> AnimationCancel;
        public Action<Animation> AnimationEnd;
        public Action<Animation> AnimationStart;

        public void OnAnimationCancel(Animation animation)
        {
            AnimationCancel?.Invoke(animation);
        }

        public void OnAnimationEnd(Animation animation)
        {
            AnimationEnd?.Invoke(animation);
        }

        public void OnAnimationRepeat(Animation animation)
        {
        }

        public void OnAnimationStart(Animation animation)
        {
            AnimationStart?.Invoke(animation);
        }
    }
}