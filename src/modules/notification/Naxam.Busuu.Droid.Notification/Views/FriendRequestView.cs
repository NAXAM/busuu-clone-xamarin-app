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
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Naxam.Busuu.Droid.Notification.Adapters;
using MvvmCross.Droid.Views;
using Naxam.Busuu.Droid.Notification.Models;
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Notification.ViewModels;

namespace Naxam.Busuu.Droid.Notification.Views
{
    [Activity(Label = "FriendRequestView", Theme = "@style/AppTheme")]
    public class FriendRequestView : MvxAppCompatActivity<FriendRequestViewModel>
    {
        private RecyclerView FriendRecyclerView;
        private AdapterFriend adapterFriend;
        Android.Support.V7.Widget.Toolbar toolbar;

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.activity_friend_request);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Friend requests";
            BindViews();
        }

        private void BindViews()
        {
            FriendRecyclerView = (RecyclerView)FindViewById(Resource.Id.recyclerFriend);
            adapterFriend = new AdapterFriend(ViewModel.FriendsRequest, this);
            LinearLayoutManager layoutManager = new LinearLayoutManager(ApplicationContext);
            layoutManager.Orientation = LinearLayoutManager.Vertical;
            FriendRecyclerView.SetLayoutManager(layoutManager);
            FriendRecyclerView.SetAdapter(adapterFriend);
            adapterFriend.ConfirmRequest += (s, e) =>
            {
                ViewModel.ViewFriendsYesCommand?.Execute(e);
            };
            adapterFriend.DeleteRequest += (s, e) =>
            {
                ViewModel.ViewFriendsNoCommand?.Execute(e);
            };
        }

        public override bool OnSupportNavigateUp()
        {
            ViewModel.GoBackCommand?.Execute();
            return base.OnSupportNavigateUp();
        }
    }

}