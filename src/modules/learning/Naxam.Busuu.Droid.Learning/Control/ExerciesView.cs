using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Naxam.Busuu.Learning.Models;
using Android.Graphics.Drawables;
using Android.Graphics;
using static Naxam.Busuu.Learning.Models.ExerciseModel;
using MvvmCross.Core.ViewModels;
using Android.Graphics.Drawables.Shapes;

namespace Naxam.Busuu.Droid.Learning.Control
{

    public class ExerciesView : LinearLayout
    {
        public Color Color;
        public IList<ExerciseModel> ItemsSource { get; set; }
        public event EventHandler<ExerciseClickEventArg> ExerciseClick;
        public ExerciesView(Context context) : base(context)
        {

        }

        public ExerciesView(Context context, IAttributeSet attrs) : base(context, attrs)
        {

        }

        public ExerciesView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {

        }

        public ExerciesView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {

        }

        protected ExerciesView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {


        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

        }




        public void Init()
        {
            RemoveAllViews();
            if (Color == new Color(0, 0, 0, 0) || ItemsSource == null)
                return;
            for (int i = 0; i < ItemsSource.Count; i++)
            {

                var temp = LayoutInflater.FromContext(Context).Inflate(Resource.Layout.layout_exercise_item, null);
                ImageView imgExercise = temp.FindViewById<ImageView>(Resource.Id.imgExercise);
                ImageView imgLock = temp.FindViewById<ImageView>(Resource.Id.imgLock);
                imgExercise.SetImageResource(GetSource(ItemsSource.ElementAt(i).Type));
                if (ItemsSource.ElementAt(i).IsDone)
                {
                    imgExercise.Background = GetBackground(Color);
                    imgExercise.SetColorFilter(Color.White);
                }
                else
                {
                    imgExercise.Background = GetBackground(Color.White);
                    imgExercise.SetColorFilter(Color);
                }
                var param = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
                if (i > 0)
                    param.LeftMargin = (int)Util.Util.PxFromDp(Context, 16);
                temp.Tag = i;
                temp.Click += ExerciseTapped;

                AddView(temp, param);

            }
            ForceLayout();
        }

        private void ExerciseTapped(object sender, EventArgs e)
        {
            var view = (View)sender;
            var tempIndex = (int)view.Tag;
            var tempEx = ItemsSource.ElementAt(tempIndex);

			ExerciseClick?.Invoke(this, new ExerciseClickEventArg
			{
				Exercise = tempEx,
				ExerciseIndex = tempIndex
			});

        }

        private int GetSource(ExerciseType type)
        {
            switch (type)
            {
                case ExerciseType.Conversation:
                    return Resource.Drawable.icon_bubbles;
                case ExerciseType.Discover:
                    return Resource.Drawable.icon_book_search;
                case ExerciseType.Evolution:
                    return Resource.Drawable.icon_book_side;
                case ExerciseType.Memorise:
                    return Resource.Drawable.icon_lightning;
                case ExerciseType.Practice:
                    return Resource.Drawable.icon_book_tick;
                case ExerciseType.Vocabulary:
                    return Resource.Drawable.icon_vocabulary;
                case ExerciseType.Dialogue:
                    return Resource.Drawable.icon_dialogue;

            }
            return Resource.Drawable.ic_component_memorise;
        }

        private Drawable GetBackground(Color color)
        {
            PaintDrawable gradient = new PaintDrawable(color);
            gradient.SetCornerRadius(1000);
            gradient.Shape = new OvalShape();
            return gradient;
        }
    }
}