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
using Naxam.Busuu.Profile.ViewModels;
using Android.Graphics;
using Naxam.Busuu.Droid.Core.Utils;
using MvvmCross.Binding;

namespace Naxam.Busuu.Droid.Profile.TargetBindings
{
    public class FriendRequestButtonStateTargetBinding : MvxAndroidTargetBinding
    {
        public FriendRequestButtonStateTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(FriendRequestButton);

        protected override void SetValueImpl(object target, object value)
        {
            FriendRequestButton view = (FriendRequestButton)target;
            FriendState state = (FriendState)value;
            switch (state)
            {
                case FriendState.Friend:
                    view.SetText("Friend", Color.White);
                    view.SetIcon(Resource.Drawable.remove_user, Color.White);
                    view.Background = BackgroundUtil.BackgroundRound(view.Context, 1000, Color.ParseColor("#38A9F6"));
                    break;
                case FriendState.Me:
                    view.Visibility = ViewStates.Gone;
                    break;
                case FriendState.Unfriend:
                    view.SetText("Add Friend", Color.ParseColor("#38A9F6"));
                    view.SetIcon(Resource.Drawable.add_user, Color.ParseColor("#A7B0B7"));
                    view.Background = BackgroundUtil.BackgroundRound(view.Context, 1000, Color.White);
                    break;

                case FriendState.RequestFriend:
                    view.SetText("Request Sent", Color.ParseColor("#38A9F6"));
                    view.SetIcon(Resource.Drawable.request_sent, Color.ParseColor("#38A9F6"));
                    view.Background = BackgroundUtil.BackgroundRound(view.Context, 1000, Color.White);
                    break;
            }
        }
        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
    }
}