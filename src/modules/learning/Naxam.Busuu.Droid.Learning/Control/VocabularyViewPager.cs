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
using Android.Support.V4.View;
using Android.Util;

namespace Naxam.Busuu.Droid.Learning.Control.Vocabulary
{
    public class VocabularyViewPager : ViewPager
    {
        public enum SwipeDirection
        {
            All, Left, Right, None
        }

        private float initialXValue;
        private SwipeDirection Direction;

        public VocabularyViewPager(Context context, IAttributeSet attrs) : base(context, attrs)
        {

        }
        public VocabularyViewPager(Context context) : base(context)
        {
            Direction = SwipeDirection.All;
        }
        private bool IsSwipeAllowed(MotionEvent e)
        {
            if (Direction == SwipeDirection.All) return true;

            if (Direction == SwipeDirection.None)
                return false;

            if (e.Action == MotionEventActions.Down)
            {
                initialXValue = e.GetX();
                return true;
            }

            if (e.Action == MotionEventActions.Move)
            {
                try
                {
                    float diffX = e.GetX() - initialXValue;
                    if (diffX > 0 && Direction == SwipeDirection.Right)
                    {
                        // swipe from left to right detected
                        return false;
                    }
                    else if (diffX < 0 && Direction == SwipeDirection.Left)
                    {
                        // swipe from right to left detected
                        return false;
                    }
                }
                catch
                {
                    //exception.PrintStackTrace();
                }
            }

            return true;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (IsSwipeAllowed(e))
            {
                return base.OnTouchEvent(e);
            }

            return false;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (IsSwipeAllowed(ev))
            {
                return base.OnInterceptTouchEvent(ev);
            }
            return false;
        }
        public void SetAllowedSwipeDirection(SwipeDirection direction)
        {
            this.Direction = direction;
        }
    }
}