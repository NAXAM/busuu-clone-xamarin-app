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
using MvvmCross.Binding;
using MvvmCross.Binding.Droid.Target;
using Naxam.Busuu.Droid.Profile.Controls;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Droid.Profile.TargetBindings
{
    public class LanguagesTextViewTargetBinding : MvxAndroidTargetBinding
    {
        public LanguagesTextViewTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(LanguagesTextView);

        protected override void SetValueImpl(object target, object value)
        {
            LanguagesTextView txtLang = (LanguagesTextView)target;
            txtLang.SetText((IList<LanguageModel>)value);
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }
}