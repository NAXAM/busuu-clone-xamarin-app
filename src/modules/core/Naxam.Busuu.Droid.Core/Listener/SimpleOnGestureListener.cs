using Android.Views;

namespace Naxam.Busuu.Droid.Core.Listener
{
    public class SimpleOnGestureListener : GestureDetector.SimpleOnGestureListener
    {
        public override bool OnSingleTapUp(MotionEvent e)
        {
            return true;
        }
    }

}