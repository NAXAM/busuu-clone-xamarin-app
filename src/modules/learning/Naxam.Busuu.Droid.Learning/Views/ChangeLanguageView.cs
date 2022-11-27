using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Android.Support.V7.Widget;
using static Android.Support.V7.Widget.GridLayoutManager;
using Android.Support.V4.View;
using Java.Util;
using Android.Support.V4.Text;
using Android.Util;
using Android.Content.Res;
using Naxam.Busuu.Droid.Learning.Views;
using Naxam.Busuu.Learning.ViewModels;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Label = "Languages", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, ParentActivity = typeof(LearnView))]
    public partial class ChangeLanguageView : MvxAppCompatActivity<ChangeLanguageViewModel>
    {
        MvxRecyclerView LanguageListview;
        GridSpacingItemDecoration ItemDecoration;
        int count;
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.ChangeLanguageActivity);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            LanguageListview = FindViewById<MvxRecyclerView>(Resource.Id.LanguageListview);
            count = LanguageListview.Adapter.ItemsSource.Cast<object>().Count();
            SetLayoutManager(2);
        }

        void SetLayoutManager(int column)
        {
            if (ItemDecoration != null)
            {
                LanguageListview.RemoveItemDecoration(ItemDecoration);
            }
            DisplayMetrics displayMetrics = ApplicationContext.Resources.DisplayMetrics;
            float dpHeight = displayMetrics.HeightPixels / displayMetrics.Density;
            float dpWidth = displayMetrics.WidthPixels / displayMetrics.Density;

            ItemDecoration = new GridSpacingItemDecoration(column, (int)(dpWidth - 136 * column) / column, false);
            LanguageListview.AddItemDecoration(ItemDecoration);
            StaggeredGridLayoutManager grid2 = new StaggeredGridLayoutManager(column, 1);

            GridLayoutManager grid = new GridLayoutManager(this, column);
            grid.SetSpanSizeLookup(new LanguageSpanSizeLookup(column, count));
            LanguageListview.SetLayoutManager(grid);
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                SetLayoutManager(3);
            }
            if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait)
            {
                SetLayoutManager(2);
            }

        }
        public override bool OnSupportNavigateUp()
        {
            ViewModel.GoBackCommand?.Execute();
            return base.OnSupportNavigateUp();
        }

        public int ToPixel(float dp)
        {
            float px = TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, Resources.DisplayMetrics);
            return (int)Math.Round(px);
        }
        class LanguageSpanSizeLookup : SpanSizeLookup
        {
            int column;
            int count;

            public LanguageSpanSizeLookup(int column, int count)
            {
                this.column = column;
                this.count = count;
            }

            public override int GetSpanSize(int position)
            {
                if (position == 0)
                {
                    return column;
                }
                int col = (count - 1) % column;

                if (position >= count - col)
                {
                    return column - col + 1;
                }

                return 1;
            }
        }

    }
}