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
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Droid.Learning.Control;
using Naxam.Busuu.Learning.ViewModels;
using Naxam.Busuu.Droid.Learning.Control.Memo;
using Naxam.Busuu.Droid.Learning.Control.Vocabulary;
using Naxam.Busuu.Droid.Learning.Adapters;
using Naxam.Busuu.Droid.Learning.Transformers;
using static Android.Support.V4.View.ViewPager;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Label = "Vocabulary", Theme = "@style/AppTheme.NoActionBar", ParentActivity = typeof(LearnView))]
    public class VocabularyView : MvxCachingFragmentCompatActivity<VocabularyViewModel>
    {
        ProgressBar prgStep;
        LinearLayout layoutStep, layout;
        VocabularyViewPager viewPager;
        VocabularyPagerAdapter adapter;
        ImageView menuTip;
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.vocabulary_activity);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            viewPager = FindViewById<VocabularyViewPager>(Resource.Id.viewPager);
            viewPager.SetAllowedSwipeDirection(VocabularyViewPager.SwipeDirection.Right);
            viewPager.SetPageTransformer(true, new ForegroundToBackgroundTransformer());

            menuTip = FindViewById<ImageView>(Resource.Id.menuTip);

            prgStep = FindViewById<ProgressBar>(Resource.Id.prgStep);
            layoutStep = FindViewById<LinearLayout>(Resource.Id.layoutStep);
            layout = FindViewById<LinearLayout>(Resource.Id.layout);

            prgStep.Max = ViewModel.UnitCount;
            prgStep.Progress = ViewModel.CurrentPosition + 1;
            menuTip.Click += (s, e) =>
            {
                TipDialog dialog = new TipDialog(this, ViewModel.Exercise.Units[ViewModel.CurrentPosition].Tip);
                dialog.Show();
            };
            adapter = new VocabularyPagerAdapter(ViewModel.CurrentUnit);
            adapter.Next += Adapter_Next;
            viewPager.Adapter = adapter;
            viewPager.AddOnPageChangeListener(new OnPageChangeListener((position) =>
            {
                if (position == 1)
                {
                    viewPager.SetAllowedSwipeDirection(VocabularyViewPager.SwipeDirection.None);
                }
            }));
        }

        private void Adapter_Next(object sender, int e)
        {
            ViewModel.CurrentPosition++;
            if (ViewModel.CurrentPosition >= ViewModel.UnitCount)
            {
                return;
            }
            ViewModel.Correct += e;
            adapter.Next -= Adapter_Next;
            adapter = new VocabularyPagerAdapter(ViewModel.CurrentUnit);
            viewPager.Adapter = adapter;
            adapter.Next += Adapter_Next;
            prgStep.Progress = ViewModel.CurrentPosition + 1;
            viewPager.SetAllowedSwipeDirection(VocabularyViewPager.SwipeDirection.All);
        }

        public class OnPageChangeListener : Java.Lang.Object, IOnPageChangeListener
        {
            Action<int> PageSelected;
            public OnPageChangeListener(Action<int> PageSelected)
            {
                this.PageSelected = PageSelected;
            }
            public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
            {
            }

            public void OnPageScrollStateChanged(int state)
            {
            }

            public void OnPageSelected(int position)
            {
                PageSelected?.Invoke(position);
            }
        }
    }
}