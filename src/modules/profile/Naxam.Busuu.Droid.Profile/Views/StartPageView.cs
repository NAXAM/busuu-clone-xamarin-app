using System;
using System.Collections.Generic;
using System.Linq;
using Com.Bumptech.Glide;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Naxam.Busuu.Droid.Profile.Models;
using Android.Util;
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Droid.Profile.Controls;
using Android.Graphics;
using Android.Views.Animations;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity(Theme = "@style/NoActionBarTheme")]
    public class StartPageView : MvxAppCompatActivity
    {
        public static int NUM_PAGE = 3;
        private ViewPager viewPager;
        private PagerAdapter pagerAdapter;
        private ImageView imMainBackground;
        private ImageView imSecondBackground;
        private LinearLayout startLogo;
        private NXIndicator indicator;
        // public IPageIndicator mIndicator;

        private int oldPosition = 0;
        private float oldPositionOffset = 0;
        private bool isTouched = false;
        private float touchLocationX;
        private float screenWidth;
        private float screenHeight;



        private bool isSwipeLeft = true;
        private int[] listSourceBackground = new int[]
        {
            Resource.Drawable.background1,
            Resource.Drawable.background2,
            Resource.Drawable.background3
        };

        private List<StartPageModel> list;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.StartPage);

            DisplayMetrics displayMetrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(displayMetrics);
            screenHeight = displayMetrics.HeightPixels;
            screenWidth = displayMetrics.WidthPixels;

            imMainBackground = FindViewById<ImageView>(Resource.Id.im_main_background);
            imSecondBackground = FindViewById<ImageView>(Resource.Id.im_second_background);
            startLogo = FindViewById<LinearLayout>(Resource.Id.start_logo);

            FrameLayout.LayoutParams param = (FrameLayout.LayoutParams)startLogo.LayoutParameters;
            param.BottomMargin = 2 * (int)screenHeight / 3;
            startLogo.LayoutParameters = param;


            list = new List<StartPageModel>();
            list.Add(new StartPageModel()
            {
                title = "Full langyage courses",
                content = "Bite-sized lessons perfect for everyday life, work and travel"
            });
            list.Add(new StartPageModel()
            {
                title = "Corrections from native speakers",
                content = "Learn with 65 million users worldwide"
            });
            list.Add(new StartPageModel()
            {
                title = "It works!",
                content = "22 hours of busuu Premium = 1 college semester of languege study"
            });


            var options = new RequestOptions()
                    .CenterCrop();

            Glide.With(this).Load(listSourceBackground[oldPosition]).Apply(options).Into(imMainBackground);
            Glide.With(this).Load(listSourceBackground[oldPosition]).Apply(options).Into(imSecondBackground);
            imSecondBackground.Alpha = 0;

            viewPager = FindViewById<ViewPager>(Resource.Id.pager);
            pagerAdapter = new ScreenSlidePagerAdapter(SupportFragmentManager, list);
            viewPager.Adapter = pagerAdapter;

            viewPager.SetOnPageChangeListener(new OnPageChangeListener(
                (position, positionOffset, positionOffsetPixels) =>
                {
                    System.Diagnostics.Debug.WriteLine(positionOffsetPixels + "");
                    if (positionOffset != 0)
                    {
                        if (isTouched == false)
                        {
                            if ((positionOffset - 0.5) > 0)
                            {
                                Glide.With(this).Load(listSourceBackground[oldPosition - 1]).Apply(options).Into(imSecondBackground);
                                isTouched = true;
                                isSwipeLeft = false;
                            }
                            if ((positionOffset - 0.5) < 0)
                            {
                                Glide.With(this).Load(listSourceBackground[oldPosition + 1]).Apply(options).Into(imSecondBackground);
                                isTouched = true;
                                isSwipeLeft = true;
                            }
                        }
                        updateBackgroud(isSwipeLeft, positionOffset);
                    }
                },
                (state) =>
                {
                    if (state == 0)
                    {
                        System.Diagnostics.Debug.WriteLine("End");
                        Glide.With(this).Load(listSourceBackground[oldPosition]).Apply(options).Into(imMainBackground);
                        imMainBackground.Alpha = 1;
                        Glide.With(this).Load(listSourceBackground[oldPosition]).Apply(options).Into(imSecondBackground);
                        imSecondBackground.Alpha = 0;
                        isTouched = false;
                        oldPositionOffset = 0;
                    }
                    if (state == 2)
                    {
                        System.Diagnostics.Debug.WriteLine("Chaged");
                    }
                    if (state == 1)
                    {
                        System.Diagnostics.Debug.WriteLine("Start");
                    }
                },
                (position) =>
                {
                    oldPosition = position;
                }));
            viewPager.SetOnTouchListener(new OnTouchListener((s, e) =>
            {
                if (e.GetX() != 0 || e.GetX() != screenWidth)
                {
                    if (isTouched == false) touchLocationX = e.GetX();
                    else
                    {
                        if (Math.Abs(e.GetX() - touchLocationX) < 50)
                        {
                            isTouched = false;
                        }
                    }
                }
            }));

            //mIndicator = FindViewById<CirclePageIndicator>(Resource.Id.indicator);
            //mIndicator.SetViewPager(viewPager);
        }

        class OnTouchListener : Java.Lang.Object, View.IOnTouchListener
        {
            Action<View, MotionEvent> TouchEvent;
            public OnTouchListener(Action<View, MotionEvent> TouchEvent)
            {
                this.TouchEvent = TouchEvent;
            }
            public bool OnTouch(View v, MotionEvent e)
            {
                TouchEvent?.Invoke(v, e);
                return false;
            }
        }

        public void updateBackgroud(bool isSwipeLeft, float alpha)
        {
            imSecondBackground.Alpha = isSwipeLeft ? alpha : 1 - alpha;
            imMainBackground.Alpha = isSwipeLeft ? 1 - alpha : alpha;
        }

        public class OnPageChangeListener : Java.Lang.Object, ViewPager.IOnPageChangeListener
        {
            Action<int, float, int> pageScrolled;
            Action<int> pageScrollStateChanged;
            Action<int> pageSelected;
            public OnPageChangeListener(Action<int, float, int> pageScrolled, Action<int> pageScrollStateChanged, Action<int> pageSelected)
            {
                this.pageScrolled = pageScrolled;
                this.pageScrollStateChanged = pageScrollStateChanged;
                this.pageSelected = pageSelected;
            }

            public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
            {
                pageScrolled?.Invoke(position, positionOffset, positionOffsetPixels);
            }

            public void OnPageScrollStateChanged(int state)
            {
                pageScrollStateChanged?.Invoke(state);
            }

            public void OnPageSelected(int position)
            {
                pageSelected?.Invoke(position);
            }
        }
    }

    public class ScreenSlidePagerAdapter : FragmentStatePagerAdapter
    {
        List<StartPageModel> list;
        public ScreenSlidePagerAdapter(Android.Support.V4.App.FragmentManager fm, List<StartPageModel> list) : base(fm)
        {
            this.list = list;
        }
        public override int Count
        {
            get
            {
                return list.Count;
            }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return new ScreenSlidePageFragment(list.ElementAt(position));
        }
    }
}