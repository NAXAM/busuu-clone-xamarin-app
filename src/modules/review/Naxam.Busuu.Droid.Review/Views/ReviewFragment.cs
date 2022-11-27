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
using Android.Support.V4.View;
using Naxam.Busuu.Droid.Core.Adapter;
using Naxam.Busuu.Review.ViewModels;
using Com.Getbase.Floatingactionbutton;
using Android.Views.InputMethods;
using Android.Graphics;
using Naxam.Busuu.Droid.Core.Utils;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Support.V7.App;
using Naxam.Busuu.Droid.Review.Adapter;
using static MvvmCross.Droid.Support.V4.MvxCachingFragmentStatePagerAdapter;
using Naxam.Busuu.Droid.Core;
using Naxam.Busuu.Droid.Core.Controls;

namespace Naxam.Busuu.Droid.Review.Views
{
    [NxFragment(BusuuFragmentHosts.MainView, true, ViewModelType = typeof(ReviewViewModel))]
    [Register("Naxam.Busuu.Droid.Review.Views.ReviewFragment")]
    public class ReviewFragment : MvxFragment<ReviewViewModel>
    {
        Android.Support.Design.Widget.TabLayout tabLayout;
        FloatingActionsMenu btnFloating;
        View viewBackground;
        EditText txtSearch;
        ImageView imgBack, imgSearch;
        Android.Support.V7.Widget.Toolbar toolbar;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.review_activity, container, false);
            Init(view);
            return view;
        }

        private AppCompatActivity ParentActivity
        {
            get
            {
                return ((AppCompatActivity)Activity);
            }
        }

        protected void Init(View view)
        {
            tabLayout = view.FindViewById<Android.Support.Design.Widget.TabLayout>(Resource.Id.tab_layout);
            toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            ParentActivity.SetSupportActionBar(toolbar);
            ParentActivity.SupportActionBar.SetDisplayShowTitleEnabled(false);
            txtSearch = view.FindViewById<EditText>(Resource.Id.txtSearch);
            tabLayout.AddTab(tabLayout.NewTab().SetText("All"), true);
            tabLayout.AddTab(tabLayout.NewTab().SetText("Favorites"), false);
            txtSearch.Background = BackgroundUtil.BackgroundRound(Context, 0, Color.Transparent);
            viewBackground = view.FindViewById(Resource.Id.layoutBackground);
            imgBack = view.FindViewById<ImageView>(Resource.Id.imgBack);
            imgSearch = view.FindViewById<ImageView>(Resource.Id.imgSearch);
            imgBack.Click += ImgBack_Click;
            imgSearch.Click += ImgSearch_Click;
            tabLayout.TabSelected += TabLayout_TabSelected;

            btnFloating = view.FindViewById<FloatingActionsMenu>(Resource.Id.btnFloating);
            btnFloating.MenuCollapsed += (s, e) =>
            {
                viewBackground.Visibility = ViewStates.Gone;
            };
            btnFloating.MenuExpanded += (s, e) =>
            {
                viewBackground.Visibility = ViewStates.Visible;
            };

        }


        private void TabLayout_TabSelected(object sender, Android.Support.Design.Widget.TabLayout.TabSelectedEventArgs e)
        {
            //TODO Missing ViewModel.TabSelectCommand?.Execute(e.Tab.Position);
        }

        private void ImgBack_Click(object sender, EventArgs e)
        {
            //TODO Missing ViewModel?.BackCommand?.Execute();
            View view = Activity.CurrentFocus;
            if (view != null)
            {
                InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(view.WindowToken, 0);
            }
        }

        private void ImgSearch_Click(object sender, EventArgs e)
        {
            ViewModel?.SearchCommand?.Execute();
            txtSearch.SetCursorVisible(true);
            txtSearch.RequestFocus();
            InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
            imm.ToggleSoftInput(ShowFlags.Implicit, HideSoftInputFlags.None);
            imm.ShowSoftInput(txtSearch, ShowFlags.Forced);
        }
    }
}