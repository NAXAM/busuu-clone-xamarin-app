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
using Naxam.Busuu.Core.Models;
using Android.Text;
using static Android.Widget.TextView;
using Android.Text.Style;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Core.TargetBinding
{
    public class TextNotifyTargetBinding : MvxAndroidTargetBinding
    {
        public TextNotifyTargetBinding(object target) : base(target)
        {
            // do something here later
        }

        public override Type TargetType => typeof(TextView);

        protected override void SetValueImpl(object target, object value)
        {
            TextView textview = (TextView)target;
            NotificationModel notify = (NotificationModel)value;
            string name = notify.User.Name;
            var type = notify.Type;
            string display = "nothing to show";

            switch (type)
            {
                case NotificationType.Accpect:
                    display = "has accepted your friend request";
                    break;
                case NotificationType.Reply:
                    display = "replied";
                    break;
                case NotificationType.Request:
                    display = "Request ";
                    break;
                case NotificationType.Like:
                    display = "liked your comment";
                    break;
                case NotificationType.Correct:
                    display = "corrected your excise";
                    break;
            }

            SpannableStringBuilder ssbt = new SpannableStringBuilder(name + " " + display);
            ssbt.SetSpan(new StyleSpan(TypefaceStyle.Bold), 0, name.Length, SpanTypes.InclusiveInclusive);
            textview.SetText(ssbt, BufferType.Normal);
        }
    }
}