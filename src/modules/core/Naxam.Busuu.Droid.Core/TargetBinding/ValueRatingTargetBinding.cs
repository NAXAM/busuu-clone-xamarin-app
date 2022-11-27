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
using MvvmCross.Binding;
using Com.Iarcuschin.Simpleratingbar;

namespace Naxam.Busuu.Droid.Core.TargetBinding
{
    public class ValueRatingTargetBinding : MvxAndroidTargetBinding
    {
        public ValueRatingTargetBinding(object target) : base(target)
        {
        }
        public override Type TargetType => typeof(SimpleRatingBar);

        protected override void SetValueImpl(object target, object value)
        {
            SimpleRatingBar view = (SimpleRatingBar)target;
            view.Rating = float.Parse(value.ToString());
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }
}