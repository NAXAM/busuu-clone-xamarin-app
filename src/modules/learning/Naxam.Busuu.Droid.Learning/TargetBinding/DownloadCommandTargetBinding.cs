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
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Droid.Learning.Control;
using MvvmCross.Binding;

namespace Naxam.Busuu.Droid.Learning.TargetBinding
{
    public class DownloadCommandTargetBinding : MvxAndroidTargetBinding
    {
        public DownloadCommandTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(NXMvxExpandableListView);

        protected override void SetValueImpl(object target, object value)
        {
            NXMvxExpandableListView view = (NXMvxExpandableListView)target;
            view.DownloadCommand = (IMvxCommand)value;
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }
}