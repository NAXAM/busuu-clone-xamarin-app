using System;
using System.Reflection;
using Foundation;
using MvvmCross.Binding.Bindings.Target;
using UIKit;

namespace Naxam.Busuu.iOS.Core.CustomBindings
{
    public class AttributedTextTargetBinding : MvxTargetBinding
    {
        public AttributedTextTargetBinding(object target)
        : base (target)
        {
            
        }

        public override MvvmCross.Binding.MvxBindingMode DefaultMode
        {
            get
            {
                return MvvmCross.Binding.MvxBindingMode.OneWay;
            }
        }

        public override Type TargetType
        {
            get
            {
                return typeof(string);
            }
        }

        public override void SetValue(object value)
		{
            var label = (UILabel)Target;

			if (value is NSAttributedString attributedString)
			{
				label.AttributedText = attributedString;
			}
        }
    }
}
