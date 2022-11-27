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

namespace Naxam.Busuu.Droid.Social.TargetBinding
{
    public class UrlGlideTargetBinding : MvxAndroidTargetBinding
    {
        public UrlGlideTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(ImageView);

        protected override void SetValueImpl(object target, object value)
        {
            ImageView image = (ImageView)target;
            string url = value.ToString();
            Glide.With(image.Context).Load(url)
                 .Placeholder(Resource.Drawable.ic_culture_cover)
                 .Into(image);

        }
    }
}