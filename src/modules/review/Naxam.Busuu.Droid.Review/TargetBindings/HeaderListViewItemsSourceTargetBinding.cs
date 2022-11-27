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
using Naxam.Busuu.Droid.Core.Controls;
using Naxam.Busuu.Droid.Review.Adapter;
using Naxam.Busuu.Review.Models;

namespace Naxam.Busuu.Droid.Review.TargetBindings
{
    public class HeaderListViewItemsSourceTargetBinding : MvxAndroidTargetBinding
    {
        public HeaderListViewItemsSourceTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(HeaderListView);

        protected override void SetValueImpl(object target, object value)
        {
            HeaderListView lstView = (HeaderListView)target;
            var adapter = new ReviewListAdapter(lstView.Context, (IList<ReviewModel>)value);
            lstView.SetAdapter(adapter);
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }
}