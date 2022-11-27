using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Support.V7.Widget;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Learning.Views
{
    public class LinearSpacingItemDecoration : RecyclerView.ItemDecoration
    {
        int spacing;
        public LinearSpacingItemDecoration(int spacing)
        {
            this.spacing = spacing;
        }

        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            int position = parent.GetChildAdapterPosition(view);
            if (position != 0)
            {
                outRect.Left = spacing;
            }
        }
    }

    public class GridSpacingItemDecoration : RecyclerView.ItemDecoration
    {
        private int spanCount;
        private int spacing;
        private bool includeEdge;

        public GridSpacingItemDecoration(int spanCount, int spacing, bool includeEdge)
        {
            this.spanCount = spanCount;
            this.spacing = spacing;

            this.includeEdge = includeEdge;
        }

        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            int position = parent.GetChildAdapterPosition(view); // item position 

            int column = (position - 1) % spanCount; // item column

            if (position == 0)
            {
                return;
            }
            else
            {
                if (includeEdge)
                {
                    outRect.Left = spacing - column * spacing / spanCount; // spacing - column * ((1f / spanCount) * spacing)
                    outRect.Right = (column + 1) * spacing / spanCount; // (column + 1) * ((1f / spanCount) * spacing)

                    outRect.Top = spacing;
                    outRect.Bottom = spacing;
                }
                else
                {
                    outRect.Right = column * spacing / spanCount; // column * ((1f / spanCount) * spacing)
                    outRect.Left = spacing - (column + 1) * spacing / spanCount; // spacing - (column + 1) * ((1f /    spanCount) * spacing)
                    outRect.Top = -48;
                    outRect.Bottom = spacing;
                }
            }

        }


    }


}