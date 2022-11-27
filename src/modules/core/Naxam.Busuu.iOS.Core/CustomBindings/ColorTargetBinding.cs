using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using UIKit;

namespace Naxam.Busuu.iOS.Core.CustomBindings
{
    public class ColorTargetBinding : MvxTargetBinding
    {
        public ColorTargetBinding(object target) : base(target)
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
            if (value is UIColor color) {
				if (Target is CALayer layer)
				{

					{
						layer.BorderColor = color.CGColor;
						layer.BackgroundColor = UIColor.White.CGColor;
					}
				}
			
            }


        }
    }
}
