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
using Com.Github.Lzyzsd.Circleprogress;
using MvvmCross.Binding;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Learning.TargetBinding
{
    public class FinishColorTargetBinding : MvxAndroidTargetBinding
    {
        public FinishColorTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(CircleProgress);

        protected override void SetValueImpl(object target, object value)
        {
            CircleProgress view = (CircleProgress)target;
            Color color = Color.ParseColor((string)value);
            var red = Color.GetRedComponent(color);
            var blue = Color.GetBlueComponent(color);
            var green = Color.GetGreenComponent(color);
            view.FinishedColor = Color.Argb(80, red, green, blue);
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }
}