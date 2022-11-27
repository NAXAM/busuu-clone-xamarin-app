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
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Social.TargetBindings
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

            var options = new RequestOptions()
                .Placeholder(Resource.Drawable.ic_culture_cover);

            Glide.With(image.Context).Load(url)
                 .Apply(options)
                 .Into(image);

        }
    }
}