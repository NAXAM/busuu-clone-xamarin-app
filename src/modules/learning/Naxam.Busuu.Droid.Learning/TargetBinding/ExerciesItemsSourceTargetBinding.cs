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
using Naxam.Busuu.Droid.Learning.Control;
using Naxam.Busuu.Learning.Models;
using MvvmCross.Binding;

namespace Naxam.Busuu.Droid.Learning.TargetBinding
{
    public class ExerciesItemsSourceTargetBinding : MvxAndroidTargetBinding
    {
        public ExerciesItemsSourceTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(ExerciesView);

        protected override void SetValueImpl(object target, object value)
        {
            ExerciesView view = (ExerciesView)target;
            view.ItemsSource = (IList<ExerciseModel>)value;
            view.Init();
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }
}