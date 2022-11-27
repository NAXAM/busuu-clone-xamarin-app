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
using Android.Support.V7.Widget;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Core.Utils
{
    public class SpacesItemDecoration: RecyclerView.ItemDecoration
    {
        private int space;
        public SpacesItemDecoration(int space)
        {
            this.space = space;
        }
        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            // just justify a margin of first item 
            if (parent.GetChildAdapterPosition(view) == 0)
            {
               // outRect.Top = space;
                //outRect.Left = space;
                //outRect.Right = space;
                //outRect.Bottom = space;
                
                var param = (RecyclerView.LayoutParams)view.LayoutParameters;
                param.BottomMargin = 40;
                view.LayoutParameters = param;
                
            }
            if (parent.GetChildAdapterPosition(view) == 1)
            {
                // outRect.Top = space;
                //outRect.Left = space;
                //outRect.Right = space;
               // outRect.Top = space;
            }
        }

    }
}