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
using MvvmCross.Droid.Support.V7.RecyclerView;
using Android.Util;
using Naxam.Busuu.Droid.Learning.Views;
using Android.Support.V7.Widget;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class NXRecyclerView : MvxRecyclerView
    {
        LinearSpacingItemDecoration ItemDecoration;
        public NXRecyclerView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            SetItemDecoration();
        }
        public NXRecyclerView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            SetItemDecoration();
        }
        public NXRecyclerView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            SetItemDecoration();
        }
        public NXRecyclerView(Context context, IAttributeSet attrs, int defStyle, IMvxRecyclerAdapter adapter) : base(context, attrs, defStyle, adapter)
        {
            SetItemDecoration();
        }

        void SetItemDecoration()
        {
            if (ItemDecoration == null)
            {
                ItemDecoration = new LinearSpacingItemDecoration(30);
                AddItemDecoration(ItemDecoration);
            }
           // GridLayoutManager grid = new GridLayoutManager(this.Context, 1, GridLayoutManager.Horizontal, false);
            //SetLayoutManager(grid);
        }
    }
}