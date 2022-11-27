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
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Profile.ViewModels;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Naxam.Busuu.Droid.Profile.DataTemplateSelectors;
using Android.Support.V7.Widget;

namespace Naxam.Busuu.Droid.Profile.Views
{

    [Activity(Label = "Friends", Theme = "@style/AppTheme.NoActionBar")]
    public class FriendListView : MvxAppCompatActivity<FriendListViewModel>
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        MvxRecyclerView recyclerView;
        protected override void OnViewModelSet()
        {
            
            SetContentView(Resource.Layout.friend_list_layout);
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.recyclerView);
            //recyclerView.SetLayoutManager(new LinearLayoutManager(this));

            recyclerView.ItemTemplateSelector = new FriendListTemplateSelector();
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
        }

        public override bool OnSupportNavigateUp()
        {
            ViewModel.BackCommand?.Execute();
            return base.OnSupportNavigateUp();
        }

    }
}