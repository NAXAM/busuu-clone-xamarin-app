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

namespace Naxam.Busuu.Droid.Profile.Behavior
{
    [Android.Runtime.Register("Naxam.Busuu.Droid.Profile.Behavior.ToolbarBehavior")]
    public class ToolbarBehavior : CoordinatorLayout.Behavior
    {
        private Context mContext;


        public float StartToolbarPosition;

        public ToolbarBehavior(Context contex) : base()
        {
            mContext = contex;
        }

        public ToolbarBehavior(Context contex, IAttributeSet attrs) : base()
        {
            mContext = contex;
        }

        public override bool LayoutDependsOn(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            return dependency.GetType()==typeof(Android.Support.V7.Widget.Toolbar);
        }

        bool first = true;
        public override bool OnDependentViewChanged(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            if (first)
            {
                StartToolbarPosition = dependency.GetY();
            }
            first = false;
            LinearLayout layout = (LinearLayout)child;


            float expandedPercentageFactor = dependency.GetY() / StartToolbarPosition; 
            layout.Alpha = 1 - expandedPercentageFactor*1.5f+0.5f;

            return true;
        }


    }
}