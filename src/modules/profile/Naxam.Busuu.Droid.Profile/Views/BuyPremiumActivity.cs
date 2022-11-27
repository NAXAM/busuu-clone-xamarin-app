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
using Android.Util;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity]
    public class BuyPremiumActivity : Activity
    {

        List<PremiumObject> listPremiumItem = null;
        PremiumArrayAdapter adapter = null;
        private ListView lvPremium;
        private ScrollView scPremium;
        int[] listImageSource = new int[]{
            Resource.Drawable.subscription_image_c_1,
            Resource.Drawable.subscription_image_c_2,
            Resource.Drawable.subscription_image_c_3,
            Resource.Drawable.subscription_image_c_4,
            Resource.Drawable.subscription_image_c_5,
            Resource.Drawable.vocab_trainer_icon,
            Resource.Drawable.yellow_star_d
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.buy_premium_page);

            DisplayMetrics displayMetrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(displayMetrics);
            int height = displayMetrics.HeightPixels;
            int width = displayMetrics.WidthPixels;

            scPremium = FindViewById<ScrollView>(Resource.Id.sv_premium);
            lvPremium = FindViewById<ListView>(Resource.Id.lv_premium_value);

            listPremiumItem = new List<PremiumObject>();
            adapter = new PremiumArrayAdapter(this, Resource.Layout.buy_premium_list_item, listPremiumItem);
            lvPremium.Adapter = adapter;

            for (int i = 0; i < listImageSource.Length; i++)
            {
                listPremiumItem.Add(new PremiumObject()
                {
                    iconId = listImageSource[i],
                    describe = "this is describe " + i
                });
            }
            adapter.NotifyDataSetChanged();

            View item = lvPremium.Adapter.GetView(0, null, lvPremium);
            item.Measure(0, 0);
            int itemHeight = item.MeasuredHeight;
            int itemWidth = item.MeasuredWidth;
            LinearLayout.LayoutParams layoutParam = new LinearLayout.LayoutParams((int)width, (int)listImageSource.Length * itemHeight);
            lvPremium.LayoutParameters = layoutParam;

            scPremium.SmoothScrollTo(0, 0);
        }

        public void InitInterface()
        {
            DisplayMetrics displayMetrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(displayMetrics);
            int height = displayMetrics.HeightPixels;
            int width = displayMetrics.WidthPixels;

            scPremium = FindViewById<ScrollView>(Resource.Id.sv_premium);
            lvPremium = FindViewById<ListView>(Resource.Id.lv_premium_value);

            listPremiumItem = new List<PremiumObject>();
            adapter = new PremiumArrayAdapter(this, Resource.Layout.buy_premium_list_item, listPremiumItem);
            lvPremium.Adapter = adapter;

            for (int i = 0; i < listImageSource.Length; i++)
            {
                listPremiumItem.Add(new PremiumObject()
                {
                    iconId = listImageSource[i],
                    describe = "this is describe " + i
                });
            }
            adapter.NotifyDataSetChanged();

            View item = lvPremium.Adapter.GetView(0, null, lvPremium);
            item.Measure(0, 0);
            int itemHeight = item.MeasuredHeight;
            int itemWidth = item.MeasuredWidth;
            LinearLayout.LayoutParams layoutParam = new LinearLayout.LayoutParams((int)width, (int)listImageSource.Length * itemHeight);
            lvPremium.LayoutParameters = layoutParam;

            scPremium.SmoothScrollTo(0, 0);
        }
    }
}