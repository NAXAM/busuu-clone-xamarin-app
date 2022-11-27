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
using Android.Views.Animations;
using Android.Graphics.Drawables;
using static Android.Widget.ImageView;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Bindings;
using Naxam.Busuu.Droid.Core.Adapter;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Core.Controls
{
    public class HeaderListView : RelativeLayout 
    {
        private static int FADE_DELAY = 1000;
        private static int FADE_DURATION = 2000;

        private InternalListView mListView;
        private static SectionAdapter mAdapter;
        public static RelativeLayout mHeader;
        public static View mHeaderConvertView;
        private static FrameLayout mScrollView;
        private static AbsListView.IOnScrollListener mExternalOnScrollListener;
         
 


        public HeaderListView(Context context) : base(context)
        {
            init(context, null);
        }

        public HeaderListView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            init(context, attrs);
        }

        private void init(Context context, IAttributeSet attrs)
        {
            mListView = new InternalListView(Context, attrs);
            LayoutParams listParams = new LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);
            listParams.AddRule(LayoutRules.AlignParentTop);
            mListView.LayoutParameters = listParams;
            mListView.SetOnScrollListener(new HeaderListViewOnScrollListener(mListView));
            mListView.VerticalScrollBarEnabled = false;
            mListView.ItemClick += (s, e) =>
            {
                if (mAdapter != null)
                {
                    mAdapter.OnItemClick(e.Parent, e.View, e.Position, e.Id);
                }
            };
            AddView(mListView);

            mHeader = new RelativeLayout(Context);
            LayoutParams headerParams = new LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.WrapContent);
            headerParams.AddRule(LayoutRules.AlignParentTop);
            mHeader.LayoutParameters = headerParams;
            mHeader.SetGravity(GravityFlags.Bottom);
            AddView(mHeader);

            // The list view's scroll bar can be hidden by the header, so we display our own scroll bar instead
            Drawable scrollBarDrawable = Resources.GetDrawable(Resource.Drawable.abc_ab_share_pack_mtrl_alpha, this.Context.Theme);

           // Drawable scrollBarDrawable = Resources.GetDrawable(Resource.Drawable.scrollbar_handle_holo_light, this.Context.Theme);
            mScrollView = new FrameLayout(Context);
            LayoutParams scrollParams = new LayoutParams(scrollBarDrawable.IntrinsicWidth, RelativeLayout.LayoutParams.MatchParent);
            scrollParams.AddRule(LayoutRules.AlignParentRight);
            scrollParams.RightMargin = (int)dpToPx(2);
            mScrollView.LayoutParameters = scrollParams;

            ImageView scrollIndicator = new ImageView(context);
            scrollIndicator.LayoutParameters = new LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);
            scrollIndicator.SetImageDrawable(scrollBarDrawable);
            scrollIndicator.SetScaleType(ScaleType.FitXy);
            mScrollView.AddView(scrollIndicator);
            mScrollView.Visibility = ViewStates.Invisible;

            AddView(mScrollView);
        }



        public void SetAdapter(SectionAdapter adapter)
        {
            mAdapter = adapter;
            mListView.Adapter = adapter;
        }

        public void SetOnScrollListener(AbsListView.IOnScrollListener l)
        {
            mExternalOnScrollListener = l;
        }

        private class HeaderListViewOnScrollListener : Java.Lang.Object, AbsListView.IOnScrollListener
        {

            private int previousFirstVisibleItem = -1;
            private int direction = 0;
            private int actualSection = 0;
            private bool scrollingStart = false;
            private bool doneMeasuring = false;
            private int lastResetSection = -1;
            private int nextH;
            private int prevH;
            private View previous;
            private View next;
            private AlphaAnimation fadeOut = new AlphaAnimation(1f, 0f);
            private bool noHeaderUpToHeader = false;
            private bool didScroll = false;
            InternalListView mListView;

            public HeaderListViewOnScrollListener(InternalListView mListView)
            {
                this.mListView = mListView;
            }

            private void StartScrolling()
            {
                scrollingStart = true;
                doneMeasuring = false;
                lastResetSection = -1;
            }

            private void ResetHeader(int section)
            {
                scrollingStart = false;
                AddSectionHeader(section);
                mHeader.RequestLayout();
                lastResetSection = section;
            }

            private void SetMeasurements(int realFirstVisibleItem, int firstVisibleItem)
            {

                if (direction > 0)
                {
                    nextH = realFirstVisibleItem >= firstVisibleItem ? mListView.GetChildAt(realFirstVisibleItem - firstVisibleItem).MeasuredHeight : 0;
                }

                previous = mHeader.GetChildAt(0);
                prevH = previous != null ? previous.MeasuredHeight : mHeader.Height;

                if (direction < 0)
                {
                    if (lastResetSection != actualSection - 1)
                    {
                        AddSectionHeader(Math.Max(0, actualSection - 1));
                        next = mHeader.GetChildAt(0);
                    }
                    nextH = mHeader.ChildCount > 0 ? mHeader.GetChildAt(0).MeasuredHeight : 0;
                    mHeader.ScrollTo(0, prevH);
                }
                doneMeasuring = previous != null && prevH > 0 && nextH > 0;
            }

            private void UpdateScrollBar()
            {
                if (mHeader != null && mListView != null && mScrollView != null)
                {
                    int offset = mListView.ComputeVerticalScrollOffsetInternal;
                    int range = mListView.ComputeVerticalScrollRangeInternal;
                    int extent = mListView.ComputeVerticalScrollExtentIternal;
                    mScrollView.Visibility = extent >= range ? ViewStates.Invisible : ViewStates.Visible;
                    if (extent >= range)
                    {
                        return;
                    }
                    int top = range == 0 ? mListView.Height : mListView.Height * offset / range;
                    int bottom = range == 0 ? 0 : mListView.Height - mListView.Height * (offset + extent) / range;
                    mScrollView.SetPadding(0, top, 0, bottom);
                    fadeOut.Reset();
                    fadeOut.FillBefore = true;
                    fadeOut.FillAfter = true;
                    fadeOut.StartOffset = FADE_DELAY;
                    mScrollView.ClearAnimation();
                    mScrollView.StartAnimation(fadeOut);
                }
            }

            private void AddSectionHeader(int actualSection)
            {
                View previousHeader = mHeader.GetChildAt(0);
                if (previousHeader != null)
                {
                    mHeader.RemoveViewAt(0);
                }

                if (mAdapter.HasSectionHeaderView(actualSection))
                {
                    mHeaderConvertView = mAdapter.GetSectionHeaderView(actualSection, mHeaderConvertView, mHeader);
                    mHeaderConvertView.LayoutParameters = (new LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.WrapContent));

                    mHeaderConvertView.Measure(MeasureSpec.MakeMeasureSpec(mHeader.Width, MeasureSpecMode.Exactly), MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));

                    mHeader.LayoutParameters.Height = mHeaderConvertView.MeasuredHeight;
                    mHeaderConvertView.ScrollTo(0, 0);
                    mHeader.ScrollTo(0, 0);
                    mHeader.AddView(mHeaderConvertView, 0);
                }
                else
                {
                    mHeader.LayoutParameters.Height = 0;
                    mHeader.ScrollTo(0, 0);
                }

                mScrollView.BringToFront();
            }

            private int getRealFirstVisibleItem(int firstVisibleItem, int visibleItemCount)
            {
                if (visibleItemCount == 0)
                {
                    return -1;
                }
                int relativeIndex = 0, totalHeight = mListView.GetChildAt(0).Top;
                for (relativeIndex = 0; relativeIndex < visibleItemCount && totalHeight < mHeader.Height; relativeIndex++)
                {
                    totalHeight += mListView.GetChildAt(relativeIndex).Height;
                }
                int realFVI = Math.Max(firstVisibleItem, firstVisibleItem + relativeIndex - 1);
                return realFVI;
            }

            public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
            {
                if (mExternalOnScrollListener != null)
                {
                    mExternalOnScrollListener.OnScroll(view, firstVisibleItem, visibleItemCount, totalItemCount);
                }

                if (!didScroll)
                {
                    return;
                }

                firstVisibleItem -= mListView.HeaderViewsCount;
                if (firstVisibleItem < 0)
                {
                    mHeader.RemoveAllViews();
                    return;
                }

                //UpdateScrollBar();
                if (visibleItemCount > 0 && firstVisibleItem == 0 && mHeader.GetChildAt(0) == null)
                {
                    AddSectionHeader(0);
                    lastResetSection = 0;
                }

                int realFirstVisibleItem = getRealFirstVisibleItem(firstVisibleItem, visibleItemCount);
                if (totalItemCount > 0 && previousFirstVisibleItem != realFirstVisibleItem)
                {
                    direction = realFirstVisibleItem - previousFirstVisibleItem;

                    actualSection = mAdapter.GetSection(realFirstVisibleItem);

                    bool currIsHeader = mAdapter.IsSectionHeader(realFirstVisibleItem);
                    bool prevHasHeader = mAdapter.HasSectionHeaderView(actualSection - 1);
                    bool nextHasHeader = mAdapter.HasSectionHeaderView(actualSection + 1);
                    bool currHasHeader = mAdapter.HasSectionHeaderView(actualSection);
                    bool currIsLast = mAdapter.GetRowInSection(realFirstVisibleItem) == mAdapter.NumberOfRows(actualSection) - 1;
                    bool prevHasRows = mAdapter.NumberOfRows(actualSection - 1) > 0;
                    bool currIsFirst = mAdapter.GetRowInSection(realFirstVisibleItem) == 0;

                    bool needScrolling = currIsFirst && !currHasHeader && prevHasHeader && realFirstVisibleItem != firstVisibleItem;
                    bool needNoHeaderUpToHeader = currIsLast && currHasHeader && !nextHasHeader && realFirstVisibleItem == firstVisibleItem && Math.Abs(mListView.GetChildAt(0).Top) >= mListView.GetChildAt(0).Height / 2;

                    noHeaderUpToHeader = false;
                    if (currIsHeader && !prevHasHeader && firstVisibleItem >= 0)
                    {
                        ResetHeader(direction < 0 ? actualSection - 1 : actualSection);
                    }
                    else if ((currIsHeader && firstVisibleItem > 0) || needScrolling)
                    {
                        if (!prevHasRows)
                        {
                            ResetHeader(actualSection - 1);
                        }
                        StartScrolling();
                    }
                    else if (needNoHeaderUpToHeader)
                    {
                        noHeaderUpToHeader = true;
                    }
                    else if (lastResetSection != actualSection)
                    {
                        ResetHeader(actualSection);
                    }

                    previousFirstVisibleItem = realFirstVisibleItem;
                }

                if (scrollingStart)
                {
                    int scrolled = realFirstVisibleItem >= firstVisibleItem ? mListView.GetChildAt(realFirstVisibleItem - firstVisibleItem).Top : 0;

                    if (!doneMeasuring)
                    {
                        SetMeasurements(realFirstVisibleItem, firstVisibleItem);
                    }

                    int headerH = doneMeasuring ? (prevH - nextH) * direction * Math.Abs(scrolled) / (direction < 0 ? nextH : prevH) + (direction > 0 ? nextH : prevH) : 0;

                    mHeader.ScrollTo(0, -Math.Min(0, scrolled - headerH));
                    if (doneMeasuring && headerH != mHeader.LayoutParameters.Height)
                    {
                        LayoutParams p = (LayoutParams)(direction < 0 ? next.LayoutParameters : previous.LayoutParameters);
                        p.TopMargin = headerH - p.Height;
                        mHeader.LayoutParameters.Height = headerH;
                        mHeader.RequestLayout();
                    }
                }

                if (noHeaderUpToHeader)
                {
                    if (lastResetSection != actualSection)
                    {
                        AddSectionHeader(actualSection);
                        lastResetSection = actualSection + 1;
                    }
                    mHeader.ScrollTo(0, mHeader.LayoutParameters.Height - (mListView.GetChildAt(0).Height + mListView.GetChildAt(0).Top));
                }
            }

            public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
            {
                if (mExternalOnScrollListener != null)
                {
                    mExternalOnScrollListener.OnScrollStateChanged(view, scrollState);
                }
                didScroll = true;
            }
        }

        public ListView GetListView()
        {
            return mListView;
        }

        public void AddHeaderView(View v)
        {
            mListView.AddHeaderView(v);
        }

        private float dpToPx(float dp)
        {
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, Context.Resources.DisplayMetrics);
        }

        public class InternalListView : ListView
        {

            public InternalListView(Context context, IAttributeSet attrs) : base(context, attrs)
            { 
                Divider = Utils.BackgroundUtil.BackgroundRound(context,0,Color.Transparent);
                DividerHeight = 0;
            }

            public int ComputeVerticalScrollExtentIternal
            {
                get
                {
                    return ComputeVerticalScrollExtent();
                }
            }

            public int ComputeVerticalScrollOffsetInternal
            {
                get
                {
                    return ComputeVerticalScrollOffset();
                }
            }


            public int ComputeVerticalScrollRangeInternal
            {
                get
                {
                    return ComputeVerticalScrollRange();
                }
            }

        }
    }
}