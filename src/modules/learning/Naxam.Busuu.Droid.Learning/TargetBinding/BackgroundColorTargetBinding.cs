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
using Android.Graphics;
using MvvmCross.Binding;

namespace Naxam.Busuu.Droid.Learning.TargetBinding
{
    public class BackgroundColorTargetBinding : MvxAndroidTargetBinding
    {
        public BackgroundColorTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(View);

        protected override void SetValueImpl(object target, object value)
        {
            View view = (View)target;

           // Color color = Color.ParseColor((string)value);
           // var red = Color.GetRedComponent(color);
           // var blue = Color.GetBlueComponent(color);
          //  var green = Color.GetGreenComponent(color);
           // var alpha = Color.GetAlphaComponent(color);
            view.SetBackgroundColor((Color)value);
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }

    public class BackgroundColor160TargetBinding : MvxAndroidTargetBinding
    {
        public BackgroundColor160TargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(View);

        protected override void SetValueImpl(object target, object value)
        {
            View view = (View)target;
            Color color = Color.ParseColor((string)value);
            var red = Color.GetRedComponent(color);
            var blue = Color.GetBlueComponent(color);
            var green = Color.GetGreenComponent(color);
            view.SetBackgroundColor(Color.Argb(160, red, green, blue));
        }
        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }
}