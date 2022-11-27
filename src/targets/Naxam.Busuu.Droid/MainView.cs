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
using Naxam.Busuu.Droid.Profile.Views;
using Android.Support.V7.App;
using Android.Support.V4.Content;
using Android.Graphics;
using Naxam.Busuu.Droid.Learning.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using Com.Ittianyu.Bottomnavigationviewex;
using Naxam.Busuu.Droid.Social.Views;
using MvvmCross.Droid.Support.V4;
using Naxam.Busuu.ViewModels;
using Naxam.Busuu.Droid.Core;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Shared.Presenter;
using Naxam.Busuu.Droid.Review.Views;
using Naxam.Busuu.Droid.Notification.Views;
using Naxam.Busuu.Droid.Core.Utils;
using Acr.UserDialogs;

namespace Naxam.Busuu.Droid
{
    [Activity(
        Label = "Busuu",
        Theme = "@style/AppTheme.NoActionBar",
        LaunchMode = Android.Content.PM.LaunchMode.SingleTop,
        ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize|Android.Content.PM.ConfigChanges.Orientation,
        Name = "naxam.busuu.droid.MainView")]
    public class MainView : MvxCachingFragmentCompatActivity<MainViewModel>
    {
        int fragmentSelected;
        int position;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            UserDialogs.Init(this);
        }

        private void Menu_NavigationItemSelected(object sender, Android.Support.Design.Widget.BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.menu_learn:
                    SetCommandMenuSelected(0);
                    break;
                case Resource.Id.menu_review:
                    SetCommandMenuSelected(1);
                    break;
                case Resource.Id.menu_social:
                    SetCommandMenuSelected(2);
                    break;
                case Resource.Id.menu_notification:
                    SetCommandMenuSelected(3);
                    break;
                case Resource.Id.menu_profile:
                    SetCommandMenuSelected(4);
                    break;
            }
        }
        private void SetCommandMenuSelected(int position)
        {
            if (this.position != position)
            {
                this.position = position;
                ViewModel.MenuSelectCommand?.Execute(position);
            }
        }
        BottomNavigationViewEx menu;
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Main);
            menu = FindViewById<BottomNavigationViewEx>(Resource.Id.menu_bottom);
            menu.SetTextSize(10);
            menu.SelectedItemId = fragmentSelected;
            menu.EnableShiftingMode(false);
            menu.NavigationItemSelected += Menu_NavigationItemSelected;
        }

        public override bool OnSupportNavigateUp()
        {
            SupportFragmentManager.PopBackStack();
            return base.OnSupportNavigateUp();
        }

        public override bool Show(MvxViewModelRequest request, Bundle bundle, Type fragmentType, MvxFragmentAttribute fragmentAttribute)
        {
            bool result = base.Show(request, bundle, fragmentType, fragmentAttribute);
            if (!result)
                return result;
            if (fragmentType == typeof(SocialFragment))
            {
                SetMenuSelected(Resource.Id.menu_social);
            }
            if (fragmentType == typeof(ReviewFragment))
            {
                SetMenuSelected(Resource.Id.menu_review);
            }
            if (fragmentType == typeof(ProfileFragment))
            {
                SetMenuSelected(Resource.Id.menu_profile);
            }
            if (fragmentType == typeof(NotificationView))
            {
                SetMenuSelected(Resource.Id.menu_notification);
            }
            if (fragmentType == typeof(LearnView))
            {
                SetMenuSelected(Resource.Id.menu_learn);
            }
            return result;
        }

        private void SetMenuSelected(int id)
        {
            if (menu == null)
            {
                fragmentSelected = id;
            }
            else
            {
                menu.SelectedItemId = id;
            }
        }
    }
}