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
using Com.Github.Lzyzsd.Circleprogress;
using MvvmCross.Binding;

namespace Naxam.Busuu.Droid.Learning.TargetBinding
{
    public class BorderTargetBinding : MvxAndroidTargetBinding
    {
        public BorderTargetBinding(object target) : base(target)
        {

        }

        public override Type TargetType => typeof(View);

        protected override void SetValueImpl(object target, object value)
        {
            View view = (View)target; 
            GradientDrawable drawable = new GradientDrawable();
            drawable.SetShape(ShapeType.Rectangle);
            drawable.SetStroke((int)Util.Util.PxFromDp(view.Context, 1), Color.ParseColor((string)value));
            drawable.SetCornerRadius(Util.Util.PxFromDp(view.Context, 1000));
            view.Background = drawable;
        }
        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }
}