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
using Java.Lang;

namespace Naxam.Busuu.Droid.Profile.Behavior
{
    [Android.Runtime.Register("Naxam.Busuu.Droid.Profile.Behavior.LanguageBehavior")]
    public class LanguageBehavior : CoordinatorLayout.Behavior
    {
        private Context Context;

        public float StartPosition;
        public int StartXPosition;
        public float StartToolbarPosition;
        public int StartYPosition;
        public int FinalYPosition;
        public int StartHeight;
        public int FinalXPosition;
        public float ChangeBehaviorPoint;
        private float mCustomFinalHeight;
        public LanguageBehavior(Context contex) : base()
        {
            Context = contex;
        }

        public LanguageBehavior(Context contex, IAttributeSet attrs) : base()
        {
            Context = contex;
        }

        public override bool LayoutDependsOn(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            return dependency.GetType() == typeof(Android.Support.V7.Widget.Toolbar);
        }

       

        public override bool OnDependentViewChanged(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            ImageView imgAvatar = (ImageView)child;
            InitProperties(imgAvatar, dependency);
            mCustomFinalHeight = Util.PxFromDp(Context, 32);
            int maxScrollDistance = (int)(StartToolbarPosition);
            float expandedPercentageFactor = dependency.GetY() / maxScrollDistance;

            if (expandedPercentageFactor < ChangeBehaviorPoint)
            {
                float heightFactor = (ChangeBehaviorPoint - expandedPercentageFactor) / ChangeBehaviorPoint;

                float distanceXToSubtract = ((StartXPosition - FinalXPosition)
                        * heightFactor) + (imgAvatar.Height / 2);
                float distanceYToSubtract = ((StartYPosition - FinalYPosition) * (1f - expandedPercentageFactor)) + (imgAvatar.Height / 2);


                imgAvatar.SetX(StartXPosition - distanceXToSubtract);


                imgAvatar.SetY(StartYPosition - distanceYToSubtract);

                float heightToSubtract = ((StartHeight - mCustomFinalHeight) * heightFactor);
                imgAvatar.Alpha=expandedPercentageFactor - 0.5f;

            }
            else
            {
                float distanceYToSubtract = ((StartYPosition - FinalYPosition)
                        * (1f - expandedPercentageFactor)) + (StartHeight / 2);

                imgAvatar.SetX(StartXPosition - imgAvatar.Width / 2);
                imgAvatar.SetY(StartYPosition - distanceYToSubtract);
                imgAvatar.Alpha = expandedPercentageFactor;
            }
            return true;
        }

        private void InitProperties(ImageView child, View dependency)
        {
            if (StartYPosition == 0)
                StartYPosition = (int)(dependency.GetY());

            if (FinalYPosition == 0)
                FinalYPosition = (dependency.Height / 2) + (int)Util.PxFromDp(Context,28);

            if (StartHeight == 0)
                StartHeight = child.Height;

            if (StartXPosition == 0)
                StartXPosition = (int)(child.GetX() +  child.Width   + (int)Util.PxFromDp(Context, 88));

            if (FinalXPosition == 0)
                FinalXPosition =    ((int)mCustomFinalHeight / 2) + (int)Util.PxFromDp(Context, 64);

            if (StartToolbarPosition == 0)
                StartToolbarPosition = dependency.GetY();

            if (ChangeBehaviorPoint == 0)
            {
                ChangeBehaviorPoint = 0.8f;//;
            }
        }
    }
}