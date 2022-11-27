using System;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using Naxam.Busuu.iOS.Core.Views;
using UIKit;

namespace Naxam.Busuu.iOS.Core.CustomBindings
{
    public class RippleColorTargetBinding : MvxTargetBinding
    {
        public RippleColorTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType
        {
            get
            {
                return typeof(string);
            }
        }

        public override MvxBindingMode DefaultMode
        {
            get
            {
                return MvvmCross.Binding.MvxBindingMode.OneWay;
            }
        }

        public override void SetValue(object value)
        {
            var rippleLayer = (RippleLayer)Target;

            if (value is UIColor color)
            {
                rippleLayer.AnimateColor = color;
                rippleLayer.FinishColor = color;
                rippleLayer.AnimationDuration = 0.3f;
            }
        }
    }
}
