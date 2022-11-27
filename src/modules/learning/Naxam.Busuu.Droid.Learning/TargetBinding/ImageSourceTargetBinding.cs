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
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Load.Engine;
using MvvmCross.Binding;
using Android.Support.V7.Widget;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Learning.TargetBinding
{
    public class ImageSourceTargetBinding : MvxAndroidTargetBinding
    {
        public ImageSourceTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(ImageView);

        protected override void SetValueImpl(object target, object value)
        {
            ImageView view = (ImageView)target;
            if (string.IsNullOrEmpty(value.ToString()))
                return;
            
            var options = new RequestOptions()
                .Transform(new CircleTransform(view.Context));
            Glide
                .With(view.Context)
                .Load(value.ToString())
                .Apply(options)
                .Into(view);
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneTime;
    }

    public class ImageSourceNormalTargetBinding : MvxAndroidTargetBinding
    {
        public ImageSourceNormalTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(ImageView);

        protected override void SetValueImpl(object target, object value)
        {
            if (string.IsNullOrEmpty((string)value))
                return;
            ImageView view = (ImageView)target;

            var options = new RequestOptions { }
                .CenterCrop();

            Glide
                .With(view.Context)
                .Load(value.ToString())
                .Apply(options)
                .Into(view);
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneTime;
    }
}