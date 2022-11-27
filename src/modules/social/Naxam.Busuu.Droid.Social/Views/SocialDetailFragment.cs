
using Android.App;
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Support.V7.Widget;
using MvvmCross.Droid.Support.V4;
using Android.OS;
using Android.Views;
using Naxam.Busuu.Droid.Core.Controls;
using System;
using Android.Runtime;
using Naxam.Busuu.Droid.Core;
using Naxam.Busuu.Social.ViewModels;
using Naxam.Busuu.Droid.Social.Adapter;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Support.V7.App;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Social.Views
{
    [NxFragment(BusuuFragmentHosts.MainView, true, ViewModelType = typeof(SocialDetailViewModel))]
    [Register("naxam.busuu.droid.social.views.SocialDetailFragment")]
    public class SocialDetailFragment : MvxFragment<SocialDetailViewModel>
    {
        Toolbar toolbar;
        RecyclerView recyclerView;
        SocialDetailAdapter adapter;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.activity_social_detail, container, false);
            Init(view);
            return view;
        }
        protected void Init(View view)
        {
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.SetTitleTextColor(Color.White);
            toolbar.SetSubtitleTextColor(Color.White);
            toolbar.Title = "Social";
            ((AppCompatActivity)Activity).SetSupportActionBar(toolbar);
            ((AppCompatActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            adapter = adapter ?? new SocialDetailAdapter(ViewModel.SocialDetailData);
            adapter.GiveFeedBack -= Adapter_GiveFeedBack;
            adapter.GiveFeedBack += Adapter_GiveFeedBack;
            adapter.Reply -= Adapter_Reply;
            adapter.Reply += Adapter_Reply;
            recyclerView?.SetAdapter(adapter);
            ((AppCompatActivity)Activity).SupportActionBar.Title = "Social";
        }

        private void Adapter_Reply(object sender, int e)
        {
            ViewModel.ReplyCommand?.Execute(e);
        }

        public override void OnViewModelSet()
        {
           
        }

        private void Adapter_GiveFeedBack(object sender, object e)
        {
            ViewModel.GiveFeedBackCommand?.Execute();
        }
    }
}