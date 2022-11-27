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
using Naxam.Busuu.Droid.Learning.Adapters;
using Naxam.Busuu.Learning.Models;
using static Android.Support.V7.Widget.RecyclerView;
using Android.Graphics;
using Android.Views.Animations;
using Android.Animation;
using Android.Content.Res;

namespace Naxam.Busuu.Droid.Learning.Control.Memo
{
    public class SelectWordImageView : MemoBaseView
    {
        public override event EventHandler<int> NextClick;
        TextView txtQuestion;
        NXPlayButton btnPlay;
        Button btnNext;
        List<int> listChoice;
        RecyclerView recyclerView;

        bool correct, clicked;

        public SelectWordImageView(Context context, UnitModel unit) : base(context)
        {
            Item = unit;
            Init(context);
           
        }
        private void SetLayout(Android.Content.Res.Orientation orientation)
        {
            GridLayoutManager grid = new GridLayoutManager(Context, 1);
            switch (orientation)
            {
                case Android.Content.Res.Orientation.Portrait:
                    grid = new GridLayoutManager(Context, 1);
                    break;
                case Android.Content.Res.Orientation.Landscape:
                    grid = new GridLayoutManager(Context, 2);
                    break;
                case Android.Content.Res.Orientation.Square:
                    grid = new GridLayoutManager(Context, 1);
                    break;
                case Android.Content.Res.Orientation.Undefined:
                    grid = new GridLayoutManager(Context, 2);
                    break;
            }
            recyclerView.SetLayoutManager(grid);
        }
        protected override void OnConfigurationChanged(Configuration newConfig)
        {
            Init(Context); 
            SetLayout(newConfig.Orientation);
            base.OnConfigurationChanged(newConfig);
        }

        private void Init(Context context)
        {
            RemoveAllViews();
            listChoice = listChoice ?? new List<int>();
            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.select_word_with_image_layout, null);
            txtQuestion = view.FindViewById<TextView>(Resource.Id.txtQuestion);
            btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            SelectWordImageRecyclerViewAdapter adapter = new SelectWordImageRecyclerViewAdapter(Context, Item.Answers, listChoice);
            SetLayout(context.Resources.Configuration.Orientation);
            recyclerView.SetAdapter(adapter);
            ItemtouchListener touch = new ItemtouchListener(Context);
            
            


            btnNext.Click += (s, e) =>
            {
                NextClick?.Invoke(btnNext, correct ? 1 : 0);
            };
            touch.Clicked += (s, e) =>
            {
                if (clicked)
                    return;
                listChoice.Add(e);
                clicked = true;
                correct = true;
                View itemView = ((View)s);
                ImageView imgResult = itemView.FindViewById<ImageView>(Resource.Id.imgResult);
                TextView txtAnswer = itemView.FindViewById<TextView>(Resource.Id.txtAnswer);
                txtAnswer.SetTextColor(Color.White);
                imgResult.Visibility = ViewStates.Visible;
                if (Item.Answers[e].Value)
                {
                    itemView.SetBackgroundColor(Color.ParseColor("#74B825"));
                }
                else
                {
                    itemView.SetBackgroundColor(Color.ParseColor("#EE6253"));
                    float distance = Util.Util.PxFromDp(Context, 8);
                    AnimatorSet mAnimatorSet = new AnimatorSet();
                    var anim = ObjectAnimator.OfFloat(itemView, "TranslationX", distance, -distance, 0);
                    anim.RepeatCount = 10;
                    anim.RepeatMode = ValueAnimatorRepeatMode.Reverse;
                    mAnimatorSet.Play(anim);
                    correct = false;
                    mAnimatorSet.SetDuration(50);
                    mAnimatorSet.Start();
                }
                for (int i = 0; i < Item.Answers.Count; i++)
                {
                    var count = recyclerView.ChildCount;
                    if (i == e)
                    {
                        continue;
                    }
                    if (Item.Answers[i].Value)
                    {
                        recyclerView.GetChildAt(i).SetBackgroundColor(Color.ParseColor("#74B825"));
                        recyclerView.GetChildAt(i).FindViewById<TextView>(Resource.Id.txtAnswer).SetTextColor(Color.White);
                        recyclerView.GetChildAt(i).FindViewById<ImageView>(Resource.Id.imgResult).Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        recyclerView.GetChildAt(i).FindViewById<TextView>(Resource.Id.txtAnswer).Alpha = 0.8f;
                        var img = recyclerView.GetChildAt(i).FindViewById<ImageView>(Resource.Id.imgResult);
                        img.SetBackgroundColor(Color.ParseColor("#80ffffff"));
                        img.SetImageResource(0);
                        img.Visibility = ViewStates.Visible;
                    }
                }
            };
            if (listChoice.Count == 0)
            {
                recyclerView.AddOnItemTouchListener(touch);
            }

            txtQuestion.Text = Item.Title;
            AddView(view, new ViewGroup.LayoutParams(-1, -1));
            if (listChoice.Count > 0)
            {
                for (int i = 0; i < Item.Answers.Count; i++)
                {
                    continue;
                    if (listChoice.Contains(i))
                    {
                        if (!Item.Answers[i].Value)
                        {
                            recyclerView.GetChildAt(i).SetBackgroundColor(Color.ParseColor("#EE6253"));
                            recyclerView.GetChildAt(i).FindViewById<TextView>(Resource.Id.txtAnswer).SetTextColor(Color.White);
                            recyclerView.GetChildAt(i).FindViewById<ImageView>(Resource.Id.imgResult).Visibility = ViewStates.Visible;
                        }
                    }
                    if (Item.Answers[i].Value)
                    {
                        recyclerView.GetChildAt(i).SetBackgroundColor(Color.ParseColor("#74B825"));
                        recyclerView.GetChildAt(i).FindViewById<TextView>(Resource.Id.txtAnswer).SetTextColor(Color.White);
                        recyclerView.GetChildAt(i).FindViewById<ImageView>(Resource.Id.imgResult).Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        recyclerView.GetChildAt(i).FindViewById<TextView>(Resource.Id.txtAnswer).Alpha = 0.8f;
                        var img = recyclerView.GetChildAt(i).FindViewById<ImageView>(Resource.Id.imgResult);
                        img.SetBackgroundColor(Color.ParseColor("#80ffffff"));
                        img.SetImageResource(0);
                        img.Visibility = ViewStates.Visible;
                    }
                }
            }
        }
    }
    public class GestureListener : GestureDetector.SimpleOnGestureListener
    {
        public override bool OnSingleTapUp(MotionEvent e)
        {
            return true;
        }
    }

    public class ItemtouchListener : Java.Lang.Object, IOnItemTouchListener
    {
        public event EventHandler<int> Clicked;
        private GestureDetector gestureDetector;
        public ItemtouchListener(Context context)
        {
            var ges = new GestureDetector.SimpleOnGestureListener();

            gestureDetector = new GestureDetector(context, new GestureListener());
        }
        public bool OnInterceptTouchEvent(RecyclerView recyclerView, MotionEvent e)
        {
            View child = recyclerView.FindChildViewUnder(e.GetX(), e.GetY());
            if (child != null && gestureDetector.OnTouchEvent(e))
            {
                Clicked?.Invoke(child, recyclerView.GetChildAdapterPosition(child));
            }
            return false;
        }

        public void OnRequestDisallowInterceptTouchEvent(bool disallow)
        {

        }

        public void OnTouchEvent(RecyclerView recyclerView, MotionEvent @event)
        {

        }
    }
}