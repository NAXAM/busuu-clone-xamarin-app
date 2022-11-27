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
    public class TintColorTargetBinding : MvxAndroidTargetBinding
    {
        public TintColorTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(ImageView);

        protected override void SetValueImpl(object target, object value)
        {
            ImageView img = (ImageView)target;
            img.SetColorFilter(Color.ParseColor(value.ToString()));
        }
        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }
}