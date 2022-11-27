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
using Android.Support.Design.Widget;
using Android.Util;
using Naxam.Busuu.Droid.Core.Utils;
using Naxam.Busuu.Droid.Profile.Controls;

namespace Naxam.Busuu.Droid.Profile.Behavior
{
    [Android.Runtime.Register("Naxam.Busuu.Droid.Profile.Behavior.FriendRequestButtonBehavior")]
    public class FriendRequestButtonBehavior : CoordinatorLayout.Behavior
    {
        private Context Context;

        public float StartToolbarPosition;
        public int StartYPosition;
        public int FinalYPosition;
        public int StartHeight;
        public float ChangeBehaviorPoint;
        private float mCustomFinalHeight;
        public FriendRequestButtonBehavior(Context contex) : base()
        {
            Context = contex;
        }

        public FriendRequestButtonBehavior(Context contex, IAttributeSet attrs) : base()
        {
            Context = contex;
        }

        public override bool LayoutDependsOn(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            return dependency.GetType() == typeof(Android.Support.V7.Widget.Toolbar);
        }



        public override bool OnDependentViewChanged(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            RelativeLayout imgAvatar = (RelativeLayout)child;
            InitProperties(imgAvatar, dependency);
            mCustomFinalHeight = Util.PxFromDp(Context, 32);
            var expandedPercentageFactor = StartToolbarPosition == 0 ? 0f : dependency.GetY() / StartToolbarPosition;
            if (expandedPercentageFactor < ChangeBehaviorPoint)
            {
                var distanceYToSubtract = ((StartYPosition - FinalYPosition) * (1f - expandedPercentageFactor)) + (imgAvatar.Height / 2);
                imgAvatar.SetY(StartYPosition - distanceYToSubtract);
                imgAvatar.Alpha = expandedPercentageFactor - 0.5f;
            }
            else
            {
                var distanceYToSubtract = ((StartYPosition - FinalYPosition) * (1f - expandedPercentageFactor)) + (StartHeight / 2);
                imgAvatar.SetY(StartYPosition - distanceYToSubtract);
                imgAvatar.Alpha = expandedPercentageFactor;
            }
            return true;
        }

        private void InitProperties(RelativeLayout child, View dependency)
        {
            if (StartYPosition == 0)
                StartYPosition = (int)Math.Round(dependency.GetY());

            if (FinalYPosition == 0)
                FinalYPosition = (int)Math.Round((double)(dependency.Height / 2));

            if (StartHeight == 0)
                StartHeight = child.Height;


            if (StartToolbarPosition == 0)
                StartToolbarPosition = dependency.GetY();

            if (ChangeBehaviorPoint == 0)
            {
                ChangeBehaviorPoint = 0.8f;//;
            }
        }
    }
}