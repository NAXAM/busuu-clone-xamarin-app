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
using Android.Views.Animations;
using Naxam.Busuu.Droid.Learning.Control;
using MvvmCross.Droid.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Droid.Core.Listener;
using MvvmCross.Droid.Support.V4;
using Naxam.Busuu.Learning.ViewModels;

namespace Naxam.Busuu.Droid.Learning.Views
{
    public class TrueFalseQuestionFragment : MvxFragment<TrueFalseHearQuestionViewModel>
    {

        private Button btWrong;
        private Button btRight;
        private Button btContinue;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.hear_true_false_question, container, false);
            InitInterface(view);
            return view;
        }



        public void InitInterface(View view)
        {
            btContinue = view.FindViewById<Button>(Resource.Id.bt_continue);
            btRight = view.FindViewById<Button>(Resource.Id.bt_right);
            btWrong = view.FindViewById<Button>(Resource.Id.bt_wrong);

            btContinue.Visibility = ViewStates.Invisible;
            btRight.Click += BtRightWrong_Click;
            btWrong.Click += BtRightWrong_Click;
        }

        private void BtRightWrong_Click(object sender, EventArgs e)
        {
            btContinue.Visibility = ViewStates.Visible;
            Random random = new Random();
            int i = random.Next(0, 2);
            if ((((Button)sender).Text.Equals("TRUE") && !Item.Answers[0].Value) || (((Button)sender).Text.Equals("FALSE") && Item.Answers[0].Value))
            {
                ((Button)sender).SetBackgroundResource(Resource.Drawable.ic_wrong);
                ((Button)sender).Text = "";
                btRight.Enabled = false;
                btWrong.Enabled = false;
                TranslateAnimation translate = new TranslateAnimation(0, 10, 0, 0);
                translate.Duration = 50;
                translate.FillAfter = true;
                ((Button)sender).StartAnimation(translate);
                translate.SetAnimationListener(new AnimationListener()
                {
                    AnimationEnd = (a) =>
                    {
                        TranslateAnimation translate2 = new TranslateAnimation(0, -10, 0, 0);
                        translate2.Duration = 50;
                        translate2.FillAfter = true;
                        ((Button)sender).StartAnimation(translate2);
                        translate2.SetAnimationListener(new AnimationListener()
                        {
                            AnimationEnd = (b) =>
                            {
                                TranslateAnimation translate3 = new TranslateAnimation(0, 10, 0, 0);
                                translate3.Duration = 50;
                                translate3.FillAfter = true;
                                ((Button)sender).StartAnimation(translate3);
                            }
                        });
                    }
                });
            }
            else if ((((Button)sender).Text.Equals("TRUE") && Item.Answers[0].Value) || (((Button)sender).Text.Equals("FALSE") && !Item.Answers[0].Value))
            {
                ((Button)sender).SetBackgroundResource(Resource.Drawable.ic_right);
                ((Button)sender).Text = "";
                btRight.Enabled = false;
                btWrong.Enabled = false;
            }
        }
    }
}