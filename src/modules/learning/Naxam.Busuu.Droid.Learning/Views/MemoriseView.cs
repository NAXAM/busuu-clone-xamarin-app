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
using Naxam.Busuu.Droid.Learning.Control;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Learning.ViewModels;
using Naxam.Busuu.Droid.Learning.Control.Memo;
using Naxam.Busuu.Droid.Learning.Control.Vocabulary;
using Naxam.Busuu.Droid.Core.Controls;
using MvvmCross.Droid.Support.V4;
using static MvvmCross.Droid.Support.V4.MvxCachingFragmentStatePagerAdapter;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;
using Naxam.Busuu.Droid.Learning.Adapters;
using Android.Content.Res;

namespace Naxam.Busuu.Droid.Learning.Views
{

    [Activity(Label = "Memorise", Theme = "@style/AppTheme", ParentActivity = typeof(LearnView), ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.KeyboardHidden)]
    public class MemoriseView : MvxAppCompatActivity<MemoriseViewModel>
    {
        int PositionStep;
        TextView txtStep;
        ProgressBar prgStep;
        NXViewPager viewPager;
        int Corrrect;
        int position;

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.memorise_activity);
            
            txtStep = FindViewById<TextView>(Resource.Id.txtStep);
            prgStep = FindViewById<ProgressBar>(Resource.Id.prgStep);
            viewPager = FindViewById<NXViewPager>(Resource.Id.view_pager);
            viewPager.SetAllowedSwipeDirection(NXViewPager.SwipeDirection.None);
            var adapter = new MemoViewPagerAdapter(this, ViewModel.Exercise.Units);
            adapter.Next += Adapter_Next;
            viewPager.Adapter = adapter;

            prgStep.Max = ViewModel.Exercise.Units.Count;
            txtStep.Text = 1 + "/" + prgStep.Max;
            prgStep.Progress = 1;

        }
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
        }

        private void Adapter_Next(object sender, int e)
        {
            position++;
            if (position >= ViewModel.Exercise.Units.Count)
            {
                ViewModel.FinishCommand?.Execute(Corrrect);
                return;
            }

            Corrrect += e;
            txtStep.Text = (position + 1) + "/" + prgStep.Max;
            prgStep.Progress = position + 1;
            viewPager.SetCurrentItem(position, true);
        }
    }
}