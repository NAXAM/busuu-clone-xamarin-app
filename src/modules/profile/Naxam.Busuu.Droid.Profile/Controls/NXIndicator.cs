using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;

namespace Naxam.Busuu.Droid.Profile.Controls
{
    public class NXIndicator : FrameLayout
    {
        public Context context;
        ImageView indicatorCurrentItem;
        public int Count { get; set; }
        public int CurrentItem { get; set; }
        private const int INDICATOR_ITEM_SIZE = 20;
        private int indicatorItemSpace = 0;
        private int widthScreen;
        private int heightScreen;

        public NXIndicator(Context context, int Count, int CurrentItem) : base(context)
        {
            this.context = context;
            this.Count = Count;
            this.CurrentItem = CurrentItem;
            

        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            base.OnLayout(changed, left, top, right, bottom);
            widthScreen = right - left;
            indicatorItemSpace = (widthScreen - INDICATOR_ITEM_SIZE * Count) / (Count - 1);

            for (int i = 0; i < Count; i++)
            {
                var indicatorItem = new ImageView(context);
                FrameLayout.LayoutParams paramItem = new FrameLayout.LayoutParams(INDICATOR_ITEM_SIZE, INDICATOR_ITEM_SIZE);
                paramItem.Gravity = GravityFlags.Start | GravityFlags.Top;
                paramItem.LeftMargin = i * INDICATOR_ITEM_SIZE + i * indicatorItemSpace;
                indicatorItem.LayoutParameters = paramItem;
                Glide.With(context).Load(Resource.Drawable.ic_indicator_unselected).Into(indicatorItem);
                //indicatorItem.SetBackgroundResource(Resource.Drawable.ic_indicator_unselected);
                this.AddView(indicatorItem);
            }

            indicatorCurrentItem = new ImageView(context);
            FrameLayout.LayoutParams paramCurrentItem = new FrameLayout.LayoutParams(INDICATOR_ITEM_SIZE, INDICATOR_ITEM_SIZE);
            paramCurrentItem.Gravity = GravityFlags.Start | GravityFlags.Top;
            paramCurrentItem.LeftMargin = CurrentItem * INDICATOR_ITEM_SIZE + CurrentItem * indicatorItemSpace;
            indicatorCurrentItem.LayoutParameters = paramCurrentItem;
            Glide.With(context).Load(Resource.Drawable.ic_indicator_selected).Into(indicatorCurrentItem);
            //indicatorCurrentItem.SetBackgroundResource(Resource.Drawable.ic_indicator_selected);
            this.AddView(indicatorCurrentItem);
        }

        public void UpdateIndicator(bool isSwipeLeft, float alpha)
        {
            float x = isSwipeLeft ? (indicatorCurrentItem.GetX() + indicatorItemSpace * alpha) : (indicatorCurrentItem.GetX() - indicatorItemSpace * alpha);

            int intX = (int)Math.Round(indicatorItemSpace * alpha);

            //indicatorCurrentItem.Measure(MeasureSpec.MakeMeasureSpec(INDICATOR_ITEM_SIZE, MeasureSpecMode.Exactly), MeasureSpec.MakeMeasureSpec(INDICATOR_ITEM_SIZE, MeasureSpecMode.Exactly));
            //indicatorCurrentItem.Layout(intX, 0, intX + indicatorCurrentItem.MeasuredWidth, indicatorCurrentItem.MeasuredHeight);
            //RequestLayout();

            FrameLayout.LayoutParams param = (FrameLayout.LayoutParams)indicatorCurrentItem.LayoutParameters;
            param.LeftMargin = intX;
            indicatorCurrentItem.LayoutParameters = param;
            Invalidate();

            //indicatorCurrentItem.Animate().X(intX * 100).Y(0).SetDuration(1).Start();
        }
    }
}