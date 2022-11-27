
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Naxam.Busuu.Social.ViewModels;
using MvvmCross.Droid.Support.V4;
using Android.Runtime;
using Naxam.Busuu.Droid.Core;
using MvvmCross.Droid.Shared.Presenter;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;
using System;
using MvvmCross.Droid.Shared.Caching;
using Android.Support.V4.App;
using Android.Widget;
using Android.Support.V7.App;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Support.V4.View;
using System.Collections.Generic;
using static MvvmCross.Droid.Support.V4.MvxCachingFragmentStatePagerAdapter;
using Naxam.Busuu.Droid.Social.Controls;
using Naxam.Busuu.Droid.Core.Controls;

namespace Naxam.Busuu.Droid.Social.Views
{
    [NxFragment(BusuuFragmentHosts.MainView, true, ViewModelType = typeof(SocialViewModel))]
    [Register("naxam.busuu.droid.social.views.SocialFragment")]
    public class SocialFragment : MvxFragment<SocialViewModel>
    {
        public static int ResultCodeFilter = 100;
        public static string ShowWriting = "ShowWriting";
        public static string ShowSpeaking = "ShowSpeaking";
        public static string FilterLanguage = "FilterLanguage";
        Android.Support.V7.Widget.Toolbar toolbar;
        TabLayout tabs;
        NXViewPager viewPager;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.activity_social, container, false);
            Init(view);
            return view;
        }




        protected void Init(View view)
        {
            tabs = view.FindViewById<TabLayout>(Resource.Id.tabs);
            toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Social";
            ((AppCompatActivity)Activity).SetSupportActionBar(toolbar);

            viewPager = view.FindViewById<NXViewPager>(Resource.Id.view_pager);
            var fragments = new List<FragmentInfo>
            {
                new FragmentInfo("Discover",typeof(DiscoverFragment),typeof(DiscoverViewModel)),
                new FragmentInfo("Friend",typeof(FriendsFragment),typeof(FriendsViewModel)),
            };

            viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(Activity, ChildFragmentManager, fragments);
            tabs.SetupWithViewPager(viewPager);
        }
    }
}