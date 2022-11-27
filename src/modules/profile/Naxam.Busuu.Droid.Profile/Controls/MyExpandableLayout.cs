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
using Com.Github.Aakira.Expandablelayout;
using Android.Util;
using Android.Animation;
using EX = Com.Github.Aakira.Expandablelayout;

namespace Naxam.Busuu.Droid.Profile.Controls
{
    public class MyExpandableLayout : RelativeLayout
    {
        public SparseBooleanArray expandState = new SparseBooleanArray();

        public String Title;
        public String Detail;
        RelativeLayout buttonLayout, relativelayout;
        TextView txtdescription, txtTitle;
        ExpandableLinearLayout expandableLayout;

        public MyExpandableLayout(Context context) : base(context)
        {


        }

        public MyExpandableLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {


        }

        public MyExpandableLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {


        }

        public MyExpandableLayout(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {

        }

        public void Init()
        {
            View view = LayoutInflater.From(Context).Inflate(Resource.Layout.recycler_view_list_row, null);
            RemoveAllViews();
            relativelayout = (RelativeLayout)view.FindViewById(Resource.Id.relativelayout);
            buttonLayout = (RelativeLayout)view.FindViewById(Resource.Id.button);
            expandableLayout = (ExpandableLinearLayout)view.FindViewById(Resource.Id.expandableLayout);
            txtdescription = (TextView)view.FindViewById(Resource.Id.txtdescription);
            txtTitle = (TextView)view.FindViewById(Resource.Id.txtTitle);
            txtTitle.Text = Title;
            txtdescription.Text = Detail;
            relativelayout.Click += (s, e) =>
            {
                expandableLayout.Toggle();
            };
            expandableLayout.SetInRecyclerView(true);
            expandableLayout.SetInterpolator(EX.Utils.CreateInterpolator(EX.Utils.AccelerateDecelerateInterpolator));
            expandableLayout.Expanded = expandState.Get(0);
            expandableLayout.SetListener(new mExpandableLayoutListener(expandState, buttonLayout));
            buttonLayout.Rotation = expandState.Get(0) ? 180f : 0f; 

            this.AddView(view);
        }
       
        //
        private class mExpandableLayoutListener : ExpandableLayoutListenerAdapter
        {
            private SparseBooleanArray expandState;
            private RelativeLayout buttonLayout;
            public mExpandableLayoutListener(SparseBooleanArray expandState, RelativeLayout buttonLayout )
            {
                this.expandState = expandState;
                this.buttonLayout = buttonLayout;
            }
            public override void OnPreOpen()
            {
                     createRotateAnimator(buttonLayout, 0f, 180f).Start();
                     expandState.Put(0, true);
            }
            public override void OnPreClose()
            {
                createRotateAnimator(buttonLayout, 180f, 0f).Start();
                expandState.Put(0, false);
            }
            public ObjectAnimator createRotateAnimator(View target, float from, float to)
            {
                ObjectAnimator animator = ObjectAnimator.OfFloat(target, "rotation", from, to);
                animator.SetDuration(100);
                animator.SetInterpolator(EX.Utils.CreateInterpolator(EX.Utils.LinearInterpolator));
                return animator;
            }
        }



    }
}