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

namespace Naxam.Busuu.Droid.Learning.TargetBinding
{
    public class PercentTargetBinding : MvxAndroidTargetBinding
    {
        public PercentTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(CircleProgress);

        protected override void SetValueImpl(object target, object value)
        {
            CircleProgress view = (CircleProgress)target;
            view.Progress = (int)value;
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }
}