using System;
using MvvmCross.Binding;
using MvvmCross.Binding.Droid.Target;
using Naxam.Busuu.Droid.Profile.Controls;

namespace Naxam.Busuu.Droid.Profile.TargetBindings
{
    public class SettingNotificationEnableTargetBinding : MvxAndroidTargetBinding
    {
        public SettingNotificationEnableTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(SettingNotificationItem);

        protected override void SetValueImpl(object target, object value)
        {
            SettingNotificationItem item = (SettingNotificationItem)target;
            if (item.IsEnabled != (bool)value)
            {
                item.IsEnabled = (bool)value;
            }
        }


        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;
    }
}