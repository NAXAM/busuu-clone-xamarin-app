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
using Naxam.Busuu.Droid.Social.Adapter;
using Naxam.Busuu.Core.Models;
using Android.Support.V7.Widget;

namespace Naxam.Busuu.Droid.Social.TargetBindings
{

    public class ItemSourceSocialDetailListViewTargetBinding : MvxAndroidTargetBinding
    {
        public ItemSourceSocialDetailListViewTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(RecyclerView);

        protected override void SetValueImpl(object target, object value)
        {
            RecyclerView lst = (RecyclerView)target;
            lst.SetAdapter(new SocialDetailAdapter((SocialModel)value)); 
        }
        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }
}