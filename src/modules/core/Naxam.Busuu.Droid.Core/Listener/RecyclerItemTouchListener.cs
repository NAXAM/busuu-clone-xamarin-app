using System;
using Android.Content;
using Android.Views;
using Android.Support.V7.Widget;

namespace Naxam.Busuu.Droid.Core.Listener
{
    public class RecyclerItemTouchListener : Java.Lang.Object, RecyclerView.IOnItemTouchListener
    {
        private GestureDetector gestureDetector;
        Action<int> OnInterceptTouch;
        public RecyclerItemTouchListener(Context context, Action<int> OnInterceptTouch)
        {
            this.OnInterceptTouch = OnInterceptTouch;
            gestureDetector = new GestureDetector(context, new SimpleOnGestureListener());
        }


        public bool OnInterceptTouchEvent(RecyclerView rv, MotionEvent e)
        {

            View child = rv.FindChildViewUnder(e.GetX(), e.GetY());
            if (child != null && gestureDetector.OnTouchEvent(e))
            {
                OnInterceptTouch?.Invoke(rv.GetChildAdapterPosition(child));
            }
            return false;
        }


        public void OnTouchEvent(RecyclerView rv, MotionEvent e)
        {
        }


        public void OnRequestDisallowInterceptTouchEvent(bool disallowIntercept)
        {

        }
    }

}