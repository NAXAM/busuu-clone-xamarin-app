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
using Android.Content.Res;
using Android.Graphics;
using Naxam.Busuu.Droid.Core.Utils;


namespace Naxam.Busuu.Droid.Profile.Behavior
{
    [Android.Runtime.Register("Naxam.Busuu.Droid.Profile.Behavior.UserNameBehavior")]
    public class UserNameBehavior : CoordinatorLayout.Behavior
    {
        private Context mContext;

        public float StartPosition;
        public int StartXPosition;
        public float StartToolbarPosition;
        public int StartYPosition;
        public int FinalYPosition;
        public int StartHeight;
        public int FinalXPosition;
        public float ChangeBehaviorPoint;

        public UserNameBehavior(Context contex) : base()
        {
            mContext = contex;
        }

        public UserNameBehavior(Context contex, IAttributeSet attrs) : base()
        {
            mContext = contex;
        }

        public override bool LayoutDependsOn(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            return dependency.GetType() == typeof(Android.Support.V7.Widget.Toolbar);
        }


        public override bool OnDependentViewChanged(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            TextView txtView = (TextView)child;
            InitProperties(txtView, dependency);

            int maxScrollDistance = (int)(StartToolbarPosition);
            float expandedPercentageFactor = dependency.GetY() / maxScrollDistance;
            int color = (int)Math.Round(255 * (1 - expandedPercentageFactor));
            txtView.SetTextColor(Color.Rgb(color, color, color));
            if (expandedPercentageFactor < ChangeBehaviorPoint)
            {
                float heightFactor = (ChangeBehaviorPoint - expandedPercentageFactor) / ChangeBehaviorPoint;

                float distanceXToSubtract = ((StartXPosition - FinalXPosition) * (1f - expandedPercentageFactor)) + (txtView.Width / 2);
                float distanceYToSubtract = ((StartYPosition - FinalYPosition) * (1f - expandedPercentageFactor)) + (txtView.Height / 2);

                txtView.SetX(StartXPosition - distanceXToSubtract);
                txtView.SetY(StartYPosition - distanceYToSubtract);

            }
            else
            {
                float distanceYToSubtract = ((StartYPosition - FinalYPosition) * (1f - expandedPercentageFactor)) + (StartHeight / 2);
                float distanceXToSubtract = ((StartXPosition - FinalXPosition) * (1f - expandedPercentageFactor)) + (txtView.Width / 2);
                txtView.SetX(StartXPosition - distanceXToSubtract);
                txtView.SetY(StartYPosition - distanceYToSubtract);
            }
            return true;
        }

        private void InitProperties(TextView child, View dependency)
        {
            StartYPosition = StartYPosition == 0 ? (int)(dependency.GetY() + (int)Util.PxFromDp(mContext, 72)) : StartYPosition;
            FinalYPosition = FinalYPosition == 0 ? (dependency.Height / 2) + (int)Util.PxFromDp(mContext, 28) : FinalYPosition;
            StartHeight = StartHeight == 0 ? child.Height : StartHeight;
            StartXPosition = StartXPosition == 0 ? (int)(child.GetX() + Util.PxFromDp(mContext, 80)) : StartXPosition;
            FinalXPosition = FinalXPosition == 0 ? (int)Util.PxFromDp(mContext, 56) + child.Width / 2 : FinalXPosition;
            StartToolbarPosition = StartToolbarPosition == 0 ? dependency.GetY() : StartToolbarPosition;
            ChangeBehaviorPoint = ChangeBehaviorPoint == 0 ? 1 : ChangeBehaviorPoint;
        }
    }
}