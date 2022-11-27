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
using Naxam.Busuu.Droid.Learning.Control;
using Naxam.Busuu.Learning.Models;

namespace Naxam.Busuu.Droid.Learning.TargetBinding
{
    public class MemoriseTargetBinding : MvxAndroidTargetBinding
    {
        public MemoriseTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(MemoriseBodyView);

        protected override void SetValueImpl(object target, object value)
        {
            MemoriseBodyView view = (MemoriseBodyView)target;
            view.Item = (ExerciseModel)value;
        }
    }
}