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

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class LessonHeaderTargetBinding : MvxAndroidTargetBinding
    {
        public LessonHeaderTargetBinding(object target):base(target)
        {

        }
        public override Type TargetType => typeof(LessonHeaderBackground);

        protected override void SetValueImpl(object target, object value)
        {
            LessonHeaderBackground view = (LessonHeaderBackground)target;
            view.BackgroundColor = Color.ParseColor((string)value);
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }
}