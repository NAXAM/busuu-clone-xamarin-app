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
using Android.Support.V4.View;
using Naxam.Busuu.Droid.Social.Adapter;
using Naxam.Busuu.Droid.Core.Utils;
using Naxam.Busuu.Core.Models;
using MvvmCross.Droid.Support.V4;
using Naxam.Busuu.Droid.Core;
using Naxam.Busuu.Social.ViewModels;
using MvvmCross.Droid.Shared.Presenter;
using MvvmCross.Droid.Shared.Caching;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;

namespace Naxam.Busuu.Droid.Social.Views
{
    // [NxFragment(BusuuFragmentHosts.SocialFragment, true, ViewModelType = typeof(DiscoverViewModel))]
    //[Register("naxam.busuu.droid.social.views.DiscoverFragment")]
    public class DiscoverFragment : MvxFragment<DiscoverViewModel>
    {
        public DiscoverFragment()
        {

        }
        public DiscoverFragment(IList<SocialModel> Items)
        {
            this.Items = Items;
        } 
        IList<SocialModel> Items;
        DiscoverAdapter adapter;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Items = new List<SocialModel>();
            View view = inflater.Inflate(Resource.Layout.discover_layout, container, false);
            ViewPager viewPager = view.FindViewById<ViewPager>(Resource.Id.viewPager);
            viewPager.PageMargin = (int)Util.PxFromDp(Context, 14);
            int offset = (int)Util.PxFromDp(Context, 16);
            viewPager.SetClipToPadding(false);
            viewPager.SetClipChildren(false);
            //  viewPager.OffscreenPageLimit = 25;
            viewPager.OffsetLeftAndRight(offset);
            var spacingLeft = (int)Util.PxFromDp(Context, 32);
            var spacingTop = (int)Util.PxFromDp(Context, 88);
            viewPager.SetPadding(spacingLeft, 0, spacingLeft, 0);
            viewPager.SetPageTransformer(false, new CarouselEffectTransformer(Context));
            adapter = new DiscoverAdapter(ViewModel.DiscoverData);
            adapter.ItemPositionClick += Adapter_ItemPositionClick;
            viewPager.Adapter = adapter;
            return view;
        }
         
        private void Adapter_ItemPositionClick(object sender, int e)
        {
            ViewModel.ViewDisoverCommand?.Execute(Items[e]);
        }


    }
}