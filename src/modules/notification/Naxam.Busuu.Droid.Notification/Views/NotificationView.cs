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
using Android.Support.V7.Widget;
using Naxam.Busuu.Droid.Notification.Adapters;
using Naxam.Busuu.Core.Models;
using MvvmCross.Droid.Views;
using Naxam.Busuu.Notification.ViewModels;
using Naxam.Busuu.Droid.Core.Utils;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Naxam.Busuu.Droid.Notification.Utils;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;
using Naxam.Busuu.Droid.Core;
using Android.Support.V7.App;

namespace Naxam.Busuu.Droid.Notification.Views
{
    [NxFragment(BusuuFragmentHosts.MainView, true, ViewModelType = typeof(NotificationViewModel))]
    [Register("naxam.busuu.droid.Notification.views.NotificationView")]
    public class NotificationView : MvxFragment<NotificationViewModel>
    {
        MvxRecyclerView recyclerView;
        Android.Support.V7.Widget.Toolbar toolbar;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.activity_notification, container, false);
            toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            ParentActivity.SetSupportActionBar(toolbar);
            toolbar.Title = "Notifications";
            recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.people_recycler_view);
            recyclerView.ItemTemplateSelector = new NotificationTemplateSelector();
            return view; 
        }
        private AppCompatActivity ParentActivity
        {
            get
            {
                return ((AppCompatActivity)Activity);
            }
        }
    }
}