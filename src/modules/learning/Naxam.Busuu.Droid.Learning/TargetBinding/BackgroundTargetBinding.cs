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
using MvvmCross.Binding.Droid.Target;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;

namespace Naxam.Busuu.Droid.Learning.TargetBinding
{
    public class BackgroundTargetBinding : MvxAndroidTargetBinding
    {
        public BackgroundTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(View);

        protected override void SetValueImpl(object target, object value)
        {
            View view = (View)target;
            PaintDrawable paint = new PaintDrawable(Color.ParseColor(value.ToString()));
            paint.SetCornerRadius(1000);
            paint.Shape = new OvalShape();
            view.Background = paint;
        }
    }
}