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
using Com.Google.Android.Flexbox;
using Naxam.Busuu.Learning.Models;
using Android.Graphics;
using Com.Bumptech.Glide;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Droid.Learning.Control.Memo;
using Android.Content.Res;
using Naxam.Busuu.Droid.Core;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class SelectWordView : MemoBaseView
    {
        public override event EventHandler<int> NextClick;
        private event EventHandler<AnswerModel> AnswerClick;
        Button btnNext;
        FlexboxLayout flexbox;
        Dictionary<int, string> choiceValue;
        List<TextView> listTextViewCorrect;
        List<TextView> listTextViewChoose;
        bool result;
        int CountAnswer;
        Android.Content.Res.Orientation orientation;
        public SelectWordView(Context context, UnitModel unit) : base(context)
        {
            orientation = context.Resources.Configuration.Orientation;
            Item = unit;
            Init(context);
        }

        protected override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            if (orientation == newConfig.Orientation)
            {
                return;
            }
            AnswerClick -= SelectWordView_AnswerClick;
            orientation = newConfig.Orientation;
            Init(Context);
        }

        public void Init(Context context)
        {
            RemoveAllViews();
            choiceValue = choiceValue ?? new Dictionary<int, string>();
            listTextViewCorrect = new List<TextView>();
            listTextViewChoose = new List<TextView>();
            CountAnswer = Item.Answers.Where(d => d.Value).ToList().Count;
            View view = LayoutInflater.FromContext(context).Inflate(string.IsNullOrEmpty(Item.Image) ? Resource.Layout.select_words_layout_non_image : Resource.Layout.select_words_layout, null);

            TextView txtQuestion = view.FindViewById<TextView>(Resource.Id.txtQuestion);
            ImageView imgImage = view.FindViewById<ImageView>(Resource.Id.imgImage);
            NXPlayButton btnPlay = view.FindViewById<NXPlayButton>(Resource.Id.btnPlay);
            flexbox = view.FindViewById<FlexboxLayout>(Resource.Id.flexAnswer);
            btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            btnNext.Visibility = ViewStates.Gone;
            btnNext.Click += BtnNext_Click;

            txtQuestion.Text = Item.Title;

            if (Item.Audio != null)
            {
                btnPlay.Visibility = ViewStates.Visible;
            }
            else
            {
                btnPlay.Visibility = ViewStates.Gone;
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
                    layoutImage.Height = (int)measuredWidth  / 5;
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

            int margin = (int)Util.Util.PxFromDp(context, 8);


            for (int i = 0; i < Item.Answers.Count; i++)
            {
                TextView btn = new TextView(context);
                btn.SetTextColor(Color.White);
                Typeface face = Typeface.CreateFromAsset(context.Assets, "fonts/museo_sans300.otf");
                btn.SetTypeface(face, TypefaceStyle.Normal);
                btn.SetPadding(margin * 2, margin, margin * 2, margin);
                if (Android.OS.Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                {
                    btn.Elevation = margin / 4;
                }
                btn.SetTextSize(ComplexUnitType.Sp, 24);
                btn.TransformationMethod = null;

                var answer = Item.Answers.ElementAt(i);
                btn.Text = answer.Text;
                btn.Tag = i;
                btn.Background = Util.BackgroundUtil.BackgroundRound(context, 4, ConstantAttributes.ColorPrimary);

                flexbox.AddView(btn, new ViewGroup.LayoutParams(-2, -2));
                FlexboxLayout.LayoutParams layoutparam = (FlexboxLayout.LayoutParams)btn.LayoutParameters;

                layoutparam.SetMargins(margin, margin, margin, margin);

                var choiced = choiceValue.Where(d => (int)d.Key == i).ToList();
                if (choiceValue.Count == CountAnswer)
                {
                    if (choiced.Count > 0 && answer.Value == false)
                    {
                        btn.Background = Util.BackgroundUtil.BackgroundRound(context, 4,ConstantAttributes.ColorIncorrect);
                    }
                    if (answer.Value)
                    {
                        btn.Background = Util.BackgroundUtil.BackgroundRound(context, 4, ConstantAttributes.ColorCorrect);
                    }
                    btnNext.Visibility = ViewStates.Visible;
                }
                else
                {
                    btn.Click += Btn_Click;
                    if (choiced.Count > 0)
                    {
                        btn.Background = Util.BackgroundUtil.BackgroundRound(context, 4, ConstantAttributes.ColorPrimaryLight);
                        listTextViewChoose.Add(btn);
                    }
                }


                if (answer.Value && !listTextViewCorrect.Contains(btn))
                {
                    listTextViewCorrect.Add(btn);
                }
            }

            AnswerClick += SelectWordView_AnswerClick;
            AddView(view, new ViewGroup.LayoutParams(-1, -1));
        }

        private void SelectWordView_AnswerClick(object sender, AnswerModel e)
        {
            TextView btn = (TextView)sender;
            if (!choiceValue.Keys.Contains<int>((int)btn.Tag))
            {
                choiceValue.Add((int)btn.Tag, btn.Text);
                listTextViewChoose.Add(btn);
                if (choiceValue.Count >= CountAnswer)
                {
                    btnNext.Visibility = ViewStates.Visible;
                    result = true;
                    if (CheckResult)
                    {
                        foreach (var item in listTextViewCorrect)
                        {
                            item.Background = Util.BackgroundUtil.BackgroundRound(btn.Context, 4, ConstantAttributes.ColorCorrect);
                        }
                    }
                    else
                    {
                        foreach (var item in listTextViewChoose)
                        {
                            item.Background = Util.BackgroundUtil.BackgroundRound(btn.Context, 4, ConstantAttributes.ColorIncorrect);
                        }
                        foreach (var item in listTextViewCorrect)
                        {
                            item.Background = Util.BackgroundUtil.BackgroundRound(btn.Context, 4, ConstantAttributes.ColorCorrect);
                        }
                        result = false;
                    }
                    for (int i = 0; i < flexbox.ChildCount; i++)
                    {
                        View viewChild = flexbox.GetChildAt(i);
                        if (viewChild.Enabled)
                        {
                            viewChild.Enabled = false;
                        }
                    }
                }
                else
                {
                    btn.Background = Util.BackgroundUtil.BackgroundRound(btn.Context, 4, ConstantAttributes.ColorPrimaryLight);
                }
            }
            else
            {
                listTextViewChoose.Remove(btn);
                choiceValue.Remove((int)btn.Tag);
                btn.Background = Util.BackgroundUtil.BackgroundRound(btn.Context, 4, ConstantAttributes.ColorPrimary);
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            AnswerClick?.Invoke(sender, null);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            NextClick?.Invoke(sender, result ? 1 : 0);
        }

        private bool CheckResult
        {
            get
            {
                bool result = true; ;
                foreach (var item in listTextViewChoose)
                {
                    if (listTextViewCorrect.Contains(item))
                    {
                        continue;
                    }
                    else
                    {
                        result = false;
                        break;
                    }
                }
                return result;
            }
        }

    }
}