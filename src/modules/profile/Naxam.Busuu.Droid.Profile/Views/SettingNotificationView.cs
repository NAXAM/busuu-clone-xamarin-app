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
using MvvmCross.Droid.Views;
using Android.Support.V7.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Naxam.Busuu.Profile.ViewModels;
using Naxam.Busuu.Droid.Profile.Controls;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity(Label = "Setting Notification", Theme = "@style/AppTheme")]
    public class SettingNotificationView : MvxAppCompatActivity<NotificationSettingViewModel>
    {
        SettingNotificationItem snPrivateMode, snCorectionAdded;
        SettingNotificationItem snNotifications, snFriendRequest;
        SettingNotificationItem snCorectionReceived, snReplies, snCorectionRequest;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_setting_notification);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            snPrivateMode = FindViewById<SettingNotificationItem>(Resource.Id.snPrivateMode);
            snNotifications = FindViewById<SettingNotificationItem>(Resource.Id.snNotifications);

            snCorectionAdded = FindViewById<SettingNotificationItem>(Resource.Id.snCorectionAdded);
            snFriendRequest = FindViewById<SettingNotificationItem>(Resource.Id.snFriendRequest);
            snCorectionReceived = FindViewById<SettingNotificationItem>(Resource.Id.snCorectionReceived);
            snReplies = FindViewById<SettingNotificationItem>(Resource.Id.snReplies);
            snCorectionRequest = FindViewById<SettingNotificationItem>(Resource.Id.snCorectionRequest);

            snPrivateMode.CheckedChange += SnPrivateMode_CheckedChange;
            snNotifications.CheckedChange += SnNotifications_CheckedChange;
            snCorectionAdded.CheckedChange += SnCorectionAdded_CheckedChange;
            snFriendRequest.CheckedChange += SnFriendRequest_CheckedChange;
            snCorectionReceived.CheckedChange += SnCorectionReceived_CheckedChange;
            snReplies.CheckedChange += SnReplies_CheckedChange;
            snCorectionRequest.CheckedChange += SnCorectionRequest_CheckedChange;
        }

        private void SnCorectionRequest_CheckedChange(object sender, bool e)
        {
            ViewModel.TurnOnCorrectionRequest = e;
        }

        private void SnReplies_CheckedChange(object sender, bool e)
        {
            ViewModel.TurnOnReplies = e;
        }

        private void SnCorectionReceived_CheckedChange(object sender, bool e)
        {
            ViewModel.TurnOnCorrectionReceived = e;
        }

        private void SnFriendRequest_CheckedChange(object sender, bool e)
        {
            ViewModel.TurnOnFriendRequests = e;
        }

        private void SnCorectionAdded_CheckedChange(object sender, bool e)
        {
            ViewModel.TurnOnCorrectionAdded = e;
        }

        private void SnNotifications_CheckedChange(object sender, bool e)
        {
            ViewModel.TurnOnNotification = e;
        }

        private void SnPrivateMode_CheckedChange(object sender, bool e)
        {
            ViewModel.IsPrivateMode = e;
        }

        public override bool OnSupportNavigateUp()
        {
            ViewModel.GoBackCommand?.Execute();
            return base.OnSupportNavigateUp();
        }

    }
}