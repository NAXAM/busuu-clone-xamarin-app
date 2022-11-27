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

namespace Naxam.Busuu.Droid.Core.TargetBinding
{
    public class TextHintTargetBinding : MvxAndroidTargetBinding
    {
        public TextHintTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(EditText);

        protected override void SetValueImpl(object target, object value)
        {
            EditText editText = (EditText)target;
            editText.Hint = value.ToString();
        }
    }
}