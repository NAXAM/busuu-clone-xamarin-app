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
using Naxam.Busuu.Learning.Models;
using Com.Google.Android.Flexbox;
using Android.Graphics.Drawables;
using Android.Graphics;
using static Android.Views.GestureDetector;
using MvvmCross.Droid.Support.V4;
using Naxam.Busuu.Droid.Core;
using Naxam.Busuu.Learning.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [NxFragment(BusuuFragmentHosts.Memorise,  ViewModelType = typeof(OrderWordViewModel))]
    public class OrderWordFragment : MvxFragment<OrderWordViewModel>
    {
        TextView txtGuide, txtTitle, txtAnswer;
        FlexboxLayout FillFlex, DisplayFlex;
        LinearLayout LayoutAnswer;
        Rect currentRect, fillRect;
        Button btnNext;
        List<string> answer, input;
        float dX, dY;
        float oX, oY;
        bool clicked, correct;
        int dpMargin;
        private GestureDetector gestureDetector;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.order_word_layout, container, false);
            gestureDetector = new GestureDetector(Context, new SimpleGestureListener());
            InitView(view);
            return view;
        }

        private void InitView(View view)
        {
            dpMargin = (int)Util.Util.PxFromDp(Context, 8);
            fillRect = new Rect();
            currentRect = new Rect();
            txtTitle = view.FindViewById<TextView>(Resource.Id.txtTitle);
            txtGuide = view.FindViewById<TextView>(Resource.Id.txtGuide);
            txtAnswer = view.FindViewById<TextView>(Resource.Id.txtAnswer);
            FillFlex = view.FindViewById<FlexboxLayout>(Resource.Id.FillFlexBox);
            DisplayFlex = view.FindViewById<FlexboxLayout>(Resource.Id.DisplayFlexBox);
            LayoutAnswer = view.FindViewById<LinearLayout>(Resource.Id.LayoutAnswer);
            btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            if (string.IsNullOrEmpty(ViewModel.Item.Title))
            {
                txtTitle.LayoutParameters.Height = 0;
                FillFlex.SetPadding(FillFlex.PaddingLeft, dpMargin * 5 - dpMargin / 2, FillFlex.PaddingRight, FillFlex.PaddingBottom);
            }

            txtAnswer.Text = ViewModel.Item.Inputs[0];
            FillFlex.ChildViewAdded += FillFlex_ChildViewAdded;
            FillFlex.ChildViewRemoved += FillFlex_ChildViewRemoved;
            Random random = new Random();
            txtGuide.SetX(0);
            txtGuide.SetY(0);
            answer = new List<string>(ViewModel.Item.Inputs[0].Split(' '));
            input = new List<string>();
            while (input.Count < answer.Count)
            {
                string temp = answer[random.Next(1, 100) % answer.Count];
                if (!input.Contains(temp))
                {
                    input.Add(temp);
                }
            }

            for (int i = 0; i < input.Count; i++)
            {
                LinearLayout layout = new LinearLayout(Context);
                TextView txtAnswer = new TextView(Context);
                txtAnswer.Background = Util.BackgroundUtil.BackgroundRound(Context, dpMargin / 8, Color.White);
                txtAnswer.Text = input[i];
                txtAnswer.SetPadding(dpMargin, dpMargin, dpMargin, dpMargin);
                if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                {
                    txtAnswer.Elevation = dpMargin / 4;
                }
                var param = new FlexboxLayout.LayoutParams(-2, dpMargin * 6);
                var textParam = new LinearLayout.LayoutParams(-2, -2);
                textParam.Gravity = GravityFlags.Top;
                textParam.SetMargins(dpMargin / 2, dpMargin / 2, dpMargin / 2, dpMargin / 2);

                layout.AddView(txtAnswer, textParam);
                DisplayFlex.AddView(layout, param);
                layout.Touch += Layout_Touch;
            }
        }


        private void FillFlex_ChildViewRemoved(object sender, ViewGroup.ChildViewRemovedEventArgs e)
        {
            if (((FlexboxLayout)sender).ChildCount > 0)
                return;
            txtGuide.Text = "Kéo từ vào đây";
        }

        private void FillFlex_ChildViewAdded(object sender, ViewGroup.ChildViewAddedEventArgs e)
        {
            if (((FlexboxLayout)sender).ChildCount == answer.Count)
            {
                correct = true;
                var view = (FlexboxLayout)sender;
                for (int i = 0; i < view.ChildCount; i++)
                {
                    LinearLayout child = (LinearLayout)view.GetChildAt(i);
                    TextView text = (TextView)child.GetChildAt(0);
                    text.SetBackgroundColor(text.Text == answer[i] ? Context.Resources.GetColor(Resource.Color.colorTextCorrect) : Context.Resources.GetColor(Resource.Color.colorTextIncorrect));
                    if (text.Text != answer[i])
                    {
                        correct = false;
                    }

                    text.SetTextColor(Color.White);
                    child.Enabled = false;
                    text.Enabled = false;
                }
                LayoutAnswer.Visibility = ViewStates.Visible;
                btnNext.Visibility = ViewStates.Visible;
                ViewModel.IsCompleted = true;
                ViewModel.IsCorrect = correct;
            }
            if (txtGuide.Text == "")
                return;
            txtGuide.Text = "";
        }

        private void Layout_Touch(object sender, View.TouchEventArgs e)
        {
            txtGuide.GetHitRect(fillRect);
            LinearLayout layout = (LinearLayout)sender;
            var param = new FlexboxLayout.LayoutParams(-2, dpMargin * 6);
            if (gestureDetector.OnTouchEvent(e.Event))
            {

                if (layout.Parent == DisplayFlex)
                {
                    ((ViewGroup)layout.Parent).RemoveView(layout);
                    FillFlex.AddView(layout, param);
                    clicked = true;
                }
                else
                {
                    ((ViewGroup)layout.Parent).RemoveView(layout);
                    DisplayFlex.AddView(layout, param);
                    clicked = true;
                }
            }
            else
            {
                switch (e.Event.Action)
                {
                    case MotionEventActions.Down:
                        clicked = false;
                        oX = layout.GetX();
                        oY = layout.GetY();
                        dX = layout.GetX() - e.Event.RawX;
                        dY = layout.GetY() - e.Event.RawY;
                        var paramT = layout.LayoutParameters;
                        paramT.Height = paramT.Height * 2;
                        layout.LayoutParameters = paramT;
                        break;
                    case MotionEventActions.Up:
                        if (clicked)
                            break;
                        if (layout.GetX() == oX && Math.Abs(layout.GetY() - oY) <= dpMargin * 6)
                        {
                            if (layout.Parent == DisplayFlex)
                            {
                                ((ViewGroup)layout.Parent).RemoveView(layout);
                                FillFlex.AddView(layout, param);

                            }
                            else
                            {
                                ((ViewGroup)layout.Parent).RemoveView(layout);
                                DisplayFlex.AddView(layout, param);

                            }
                            break;
                        }
                        var paramL = layout.LayoutParameters;
                        paramL.Height = paramL.Height / 2;
                        layout.LayoutParameters = paramL;
                        if (currentRect.Intersect(fillRect))
                        {
                            if (layout.Parent == DisplayFlex)
                            {
                                ((ViewGroup)layout.Parent).RemoveView(layout);
                                layout.TranslationX = 0;
                                layout.TranslationY = 0;
                                FillFlex.AddView(layout, param);
                            }
                            else
                            {
                                layout.TranslationX = 0;
                                layout.TranslationY = 0;
                            }
                        }
                        else
                        {
                            if (layout.Parent == DisplayFlex)
                            {
                                layout.TranslationX = 0;
                                layout.TranslationY = 0;
                            }
                            else
                            {
                                ((ViewGroup)layout.Parent).RemoveView(layout);
                                layout.TranslationX = 0;
                                layout.TranslationY = 0;
                                DisplayFlex.AddView(layout, param);
                            }
                        }
                        txtGuide.SetBackgroundColor(Color.ParseColor("#D8DDE7"));
                        break;
                    case MotionEventActions.Move:

                        layout.GetHitRect(currentRect);
                        if (currentRect.Intersect(fillRect))
                        {
                            txtGuide.SetBackgroundColor(Color.ParseColor("#A8AFB8"));
                        }
                        else
                        {
                            txtGuide.SetBackgroundColor(Color.ParseColor("#D8DDE7"));
                        }
                        layout.TranslationX = e.Event.RawX - oX + dX;
                        layout.TranslationY = e.Event.RawY - oY + dY;
                        break;
                }
            }
        }
    }
    class SimpleGestureListener : GestureDetector.SimpleOnGestureListener
    {
        public override bool OnSingleTapUp(MotionEvent e)
        {
            return true;
        }

        public override bool OnSingleTapConfirmed(MotionEvent e)
        {
            return base.OnSingleTapConfirmed(e);
        }
    }
}