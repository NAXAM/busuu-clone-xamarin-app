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
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Droid.Learning.TargetBinding
{
    public class ExerciseClickCommandTargetBinding : MvxAndroidTargetBinding
    {
        public ExerciseClickCommandTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(NXMvxExpandableListView);

        protected override void SetValueImpl(object target, object value)
        {
            NXMvxExpandableListView view = (NXMvxExpandableListView)target;
            view.ExerciseClickCommand = (IMvxCommand)value;
        }
    }
}