using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Graphics;
using Android.Views.Animations;
using Naxam.Busuu.Droid.Profile.Utils;
using Android.Util;
using Android.Support.V4.View;
using Android.Graphics.Drawables;
using Java.Lang;
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Droid.Profile.Behavior;
using Android.Text;
using Android.Text.Style;
using static Android.Widget.TextView;
using Naxam.Busuu.Droid.Core.Utils;
using Com.Bumptech.Glide;
using Naxam.Busuu.Droid.Core.Transform;
using Naxam.Busuu.Droid.Profile.Dialogs;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Profile.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;
using Naxam.Busuu.Droid.Core;
using static MvvmCross.Droid.Support.V4.MvxCachingFragmentStatePagerAdapter;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [NxFragment(BusuuFragmentHosts.MainView, true, ViewModelType = typeof(ProfileViewModel))]
    [Register("naxam.busuu.droid.Profile.views.ProfileFragment")]
    public class ProfileFragment : MvxFragment<ProfileViewModel>
    {
        private TabLayout tabLayout;
        private ViewPager viewPager;

        private ImageView imgLanguage, imgAvatar;
        private PopupMenu popup;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.GeneralProfileActivity, container, false);
            FindView(view);
            BindView();
            //CreatFriends();
            return view;
        }



        private void FindView(View view)
        {
            tabLayout = view.FindViewById<TabLayout>(Resource.Id.tab_layout);
            viewPager = view.FindViewById<ViewPager>(Resource.Id.view_pager);



            imgLanguage = view.FindViewById<ImageView>(Resource.Id.imgLanguage);
            imgAvatar = view.FindViewById<ImageView>(Resource.Id.imgAvatar);

            popup = new PopupMenu(view.Context, imgAvatar);
            popup.MenuInflater.Inflate(Resource.Menu.popup_menu, popup.Menu);
            popup.MenuItemClick += Popup_MenuItemClick;
        }

        private void Popup_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            UserPhotoDialog dialog = new UserPhotoDialog(Activity, "");
            if (e.Item.ItemId == Resource.Id.menu_show_photo)
            {
                dialog.Show();
            }
            else
            {
                dialog.Show();
            }
        }

        private int ScreenHeight
        {
            get
            {
                int Measuredwidth = 0;
                int Measuredheight = 0;
                Point size = new Point();

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
                {
                    Activity.WindowManager.DefaultDisplay.GetSize(size);
                    Measuredwidth = size.X;
                    Measuredheight = size.Y;
                }
                else
                {
                    Display d = Activity.WindowManager.DefaultDisplay;
                    Measuredwidth = d.Width;
                    Measuredheight = d.Height;
                }
                return Measuredheight;
            }
        }
        private void BindView()
        {
            DisplayMetrics displayMetrics = new DisplayMetrics();
            Activity.WindowManager.DefaultDisplay.GetMetrics(displayMetrics);
            int height = displayMetrics.HeightPixels;
            Android.Support.V4.App.FragmentManager manager = Activity.SupportFragmentManager;
        

            var fragments = new List<FragmentInfo>
            {
                new FragmentInfo("Exercise",typeof(ExerciseFragment),typeof(ExerciseViewModel)),
               new FragmentInfo("Correction",typeof(CorrectionFragment),typeof(CorrectionViewModel)),
            };

            viewPager.LayoutParameters.Height = ScreenHeight - (int)Util.PxFromDp(Activity, 64 * 2);
            viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(Activity, ChildFragmentManager, fragments);
            tabLayout.SetupWithViewPager(viewPager);
            viewPager.AddOnPageChangeListener(new TabLayout.TabLayoutOnPageChangeListener(tabLayout));
            // tabLayout.SetTabsFromPagerAdapter(adapter);

            imgAvatar.Background = BackgroundUtil.BackgroundRound(Activity, 1000, Color.White);
            imgLanguage.Background = BackgroundUtil.BackgroundRound(Activity, 1000, Color.White);
            imgAvatar.Click += (s, e) =>
            {
                popup.Show();
            };
        }
    }
}