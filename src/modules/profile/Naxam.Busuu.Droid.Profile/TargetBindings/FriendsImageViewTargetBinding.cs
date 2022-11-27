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
using Naxam.Busuu.Droid.Profile.Controls;
using Naxam.Busuu.Core.Models;
using MvvmCross.Binding;

namespace Naxam.Busuu.Droid.Profile.TargetBindings
{
    public class FriendsImageViewTargetBinding : MvxAndroidTargetBinding
    {
        public FriendsImageViewTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(FriendsImageView);

        protected override void SetValueImpl(object target, object value)
        {
            FriendsImageView view = (FriendsImageView)target;
            view.Friends = (IList<UserModel>)value;
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }
}