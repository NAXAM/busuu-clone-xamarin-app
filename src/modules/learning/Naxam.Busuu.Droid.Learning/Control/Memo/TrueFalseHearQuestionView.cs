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
using Android.Graphics;
using Com.Bumptech.Glide;
using Android.Content.Res;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Learning.Control.Memo
{
    public class TrueFalseHearQuestionView : MemoBaseView
    {
        public override event EventHandler<int> NextClick;
        private Button btWrong;
        private Button btRight;
        private Button btContinue;
        ImageView imgImage;
        TextView txtTitle, txtInput;

        bool correct;
        bool isCompleted;

        public TrueFalseHearQuestionView(Context context, UnitModel unit) : base(context)
        {
            Item = unit;
            InitInterface(context);
        }

        protected override void OnConfigurationChanged(Configuration newConfig)
        {
            InitInterface(Context);
            base.OnConfigurationChanged(newConfig);
        }

        public void InitInterface(Context context)
        {
            RemoveAllViews();
            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.hear_true_false_question, null);
            btContinue = view.FindViewById<Button>(Resource.Id.bt_continue);
            btRight = view.FindViewById<Button>(Resource.Id.bt_right);
            btWrong = view.FindViewById<Button>(Resource.Id.bt_wrong);
            txtTitle = view.FindViewById<TextView>(Resource.Id.txtTitle);
            txtInput = view.FindViewById<TextView>(Resource.Id.txtInput);
            imgImage = view.FindViewById<ImageView>(Resource.Id.imgImage);
            NXPlayButton btnPlay = view.FindViewById<NXPlayButton>(Resource.Id.btnPlay);
            btContinue.Visibility = ViewStates.Invisible;
            btContinue.Click += BtContinue_Click;

            txtTitle.Text = Item.Title;
            txtInput.Text = Item.Input;

            if (Item.Audio == null)
            {
                btnPlay.Visibility = ViewStates.Gone;
            }
            else
            {
                btnPlay.Visibility = ViewStates.Visible;
            }

            int measuredWidth = 0;
            int measuredHeight = 0;
            IWindowManager windowManager = context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.HoneycombMr2)
            {
                Point size = new Point();
                windowManager.DefaultDisplay.GetSize(size);
                measuredWidth = size.X;
                measuredHeight = size.Y;
            }
            else
            {
                Display d = windowManager.DefaultDisplay;
                measuredWidth = d.Width;
                measuredHeight = d.Height;
            }
            if (!string.IsNullOrEmpty(Item.Image))
            {
                var layoutImage = imgImage.LayoutParameters;
                if (context.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape)
                {
                    layoutImage.Width = (int)measuredWidth / 2;
                    layoutImage.Height = (int)measuredWidth / 5;
                }
                if (context.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Portrait)
                {
                    layoutImage.Width = (int)measuredWidth;
                    layoutImage.Height = (int)measuredWidth * 2 / 5;
                }

                imgImage.LayoutParameters = layoutImage;

                var options = new RequestOptions()
                    .CenterCrop();
                Glide.With(context).Load(Item.Image).Apply(options).Into(imgImage);
                imgImage.Visibility = ViewStates.Visible;
            }
            else
            {
                imgImage.Visibility = ViewStates.Gone;
            }
            if (isCompleted)
            {
                btRight.Enabled = false;
                btWrong.Enabled = false;
                btContinue.Visibility = ViewStates.Visible;
                if (Item.Answer.Value)
                {
                    if(correct)
                    {
                        btRight.SetBackgroundResource(Resource.Drawable.ic_right);
                        btRight.Text = "";
                    }
                    else
                    {
                        btWrong.SetBackgroundResource(Resource.Drawable.ic_wrong);
                        btWrong.Text = "";
                    }
                }
                else 
                {
                    if (correct)
                    {
                        btWrong.SetBackgroundResource(Resource.Drawable.ic_right);
                        btWrong.Text = "";
                    }
                    else
                    {
                        btRight.SetBackgroundResource(Resource.Drawable.ic_wrong);
                        btRight.Text = "";
                    }
                }
                 
            }
            else
            {
                btRight.Click += BtRightWrong_Click;
                btWrong.Click += BtRightWrong_Click;
            }
            AddView(view, new ViewGroup.LayoutParams(-1, -1));
        }

        private void BtContinue_Click(object sender, EventArgs e)
        {
            NextClick?.Invoke(this, 1);
        }

        private void BtRightWrong_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            isCompleted = true;
            btContinue.Visibility = ViewStates.Visible;
            if ((btn.Text.Equals("TRUE") && !Item.Answer.Value) || (btn.Text.Equals("FALSE") && Item.Answer.Value))
            {
                btn.SetBackgroundResource(Resource.Drawable.ic_wrong);
                btn.Text = "";
                btRight.Enabled = false;
                btWrong.Enabled = false;
                TranslateAnimation translate = new TranslateAnimation(0, 10, 0, 0);
                translate.Duration = 50;
                translate.FillAfter = true;
                btn.StartAnimation(translate);
                translate.SetAnimationListener(new AnimationListener()
                {
                    AnimationEnd = (a) =>
                    {
                        TranslateAnimation translate2 = new TranslateAnimation(0, -10, 0, 0);
                        translate2.Duration = 50;
                        translate2.FillAfter = true;
                        btn.StartAnimation(translate2);
                        translate2.SetAnimationListener(new AnimationListener()
                        {
                            AnimationEnd = (b) =>
                            {
                                TranslateAnimation translate3 = new TranslateAnimation(0, 10, 0, 0);
                                translate3.Duration = 50;
                                translate3.FillAfter = true;
                                btn.StartAnimation(translate3);
                            }
                        });
                    }
                });
            }
            else if ((btn.Text.Equals("TRUE") && Item.Answer.Value) || (btn.Text.Equals("FALSE") && !Item.Answer.Value))
            {
                btn.SetBackgroundResource(Resource.Drawable.ic_right);
                btn.Text = "";
                btRight.Enabled = false;
                btWrong.Enabled = false;
                correct = true;
            }
        }
    }
}