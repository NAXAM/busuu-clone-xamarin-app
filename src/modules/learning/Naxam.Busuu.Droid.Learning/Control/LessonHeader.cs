using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics;
using Android.Views.Animations;
using Android.Animation;
using Android.Content.Res;
using System.Threading.Tasks;
using Android.Gestures;
using MvvmCross.Binding.Droid.Target;
using Com.Github.Lzyzsd.Circleprogress;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class LessonHeaderBackground : FrameLayout
    {
        public Color BackgroundColor { get; set; }
        public Color SecondColor { get; set; }
        protected LessonHeaderBackground(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            SecondColor = Color.White;
        }
        public LessonHeaderBackground(Context context, IAttributeSet attrs) :
            base(context, attrs)
        { 
            Initialize(attrs);
        }

        public LessonHeaderBackground(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        { 
            Initialize(attrs);
        }

        private void Initialize(IAttributeSet attrs)
        {
            SecondColor = Color.White;
        }
 
    }
}