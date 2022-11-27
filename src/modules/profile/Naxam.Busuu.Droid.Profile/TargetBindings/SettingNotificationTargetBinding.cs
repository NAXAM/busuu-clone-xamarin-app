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

namespace Naxam.Busuu.Droid.Profile.TargetBindings
{
    public class SettingNotificationTargetBinding : MvxAndroidTargetBinding
    {
        public SettingNotificationTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(SettingNotificationItem);

        protected override void SetValueImpl(object target, object value)
        {
            SettingNotificationItem item = (SettingNotificationItem)target;
            if (item.Checked != (bool)value)
            {
                item.Checked = (bool)value;
            }
        }


        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;
    }
}