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
using Android.Graphics;
using Naxam.Busuu.Learning.Models;
using Android.Views.Animations;
using Android.Animation;
using Com.Bumptech.Glide;
using Android.Util;
using Naxam.Busuu.Droid.Core;
using Android.Content.Res;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Learning.Control.Memo
{
    public class ChooseWordView : MemoBaseView
    {
        public override event EventHandler<int> NextClick;
        LinearLayout vocabularyQuestionLayout;
        ImageView imgImage;
        TextView tvQuestion;
        Button btVocabularyContinue;
        List<string> lstChoice;
        List<TextView> lstTextView;
        bool correct;

        public ChooseWordView(Context context, UnitModel unit) : base(context)
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
            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.vocabulary_question, null);
            lstChoice = lstChoice ?? new List<string>();
            lstTextView = new List<TextView>();
            vocabularyQuestionLayout = view.FindViewById<LinearLayout>(Resource.Id.vocabulary_question_layout);
            imgImage = view.FindViewById<ImageView>(Resource.Id.im_vocabulary_description);
            tvQuestion = view.FindViewById<TextView>(Resource.Id.tv_vocabulary_question);
            btVocabularyContinue = view.FindViewById<Button>(Resource.Id.bt_vocabulary_continue);
            btVocabularyContinue.Visibility = ViewStates.Gone;
            btVocabularyContinue.Click += (s, e) =>
            {
                NextClick?.Invoke(btVocabularyContinue, correct ? 1 : 0);
            };
            tvQuestion.Text = Item.Title;
            int measuredWidth = 0;
            int measuredHeight = 0;
            IWindowManager windowManager = Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

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
                if (Context.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape)
                {
                    layoutImage.Width = (int)measuredWidth / 2;
                    layoutImage.Height = (int)measuredWidth / 5;
                }
                if (Context.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Portrait)
                {
                    layoutImage.Width = (int)measuredWidth;
                    layoutImage.Height = (int)measuredWidth * 2 / 5;
                }
                imgImage.LayoutParameters = layoutImage;

                var options = new RequestOptions()
                    .CenterCrop();
                Glide.With(Context).Load(Item.Image).Apply(options).Into(imgImage);
                imgImage.Visibility = ViewStates.Visible;
            }
            else
            {
                imgImage.Visibility = ViewStates.Gone;
            }


            for (int i = 0; i < Item.Answers.Count; i++)
            {
                TextView btnAnswer = new TextView(Context);
                if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                {
                    btnAnswer.Elevation = Util.Util.PxFromDp(Context, 2);
                }
                var temp = Item.Answers[i];
                LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, (int)Util.Util.PxFromDp(Context, 48));
                param.Gravity = GravityFlags.Center;
                btnAnswer.Gravity = GravityFlags.Center;
                param.RightMargin = 64;
                param.LeftMargin = 64;
                param.BottomMargin = 32;
                btnAnswer.Text = temp.Text;
                btnAnswer.LayoutParameters = param;
                btnAnswer.SetBackgroundColor(Color.White);
                btnAnswer.SetTextColor(Color.Black);
                btnAnswer.TransformationMethod = null;
                vocabularyQuestionLayout.AddView(btnAnswer);
                btnAnswer.Click += (s, e) =>
                {
                    lstChoice.Add(temp.Text);
                    btnAnswer.SetBackgroundColor(temp.Value ? ConstantAttributes.ColorCorrect : ConstantAttributes.ColorIncorrect);
                    btnAnswer.SetTextColor(Color.White);

                    correct = temp.Value;
                    if (!temp.Value)
                    {
                        int translateDistance = (int)Util.Util.PxFromDp(Context, 8);
                        AnimatorSet mAnimatorSet = new AnimatorSet();
                        var animx = ObjectAnimator.OfFloat(btnAnswer, "TranslationX", translateDistance, -translateDistance, 0);
                        animx.RepeatCount = 8;
                        animx.RepeatMode = ValueAnimatorRepeatMode.Reverse;
                        mAnimatorSet.Play(animx);
                        mAnimatorSet.SetDuration(50);
                        mAnimatorSet.Start();
                        if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                        {
                            btnAnswer.Elevation = translateDistance / 4;
                        }
                    }

                    for (int j = 0; j < vocabularyQuestionLayout.ChildCount; j++)
                    {
                        View child = vocabularyQuestionLayout.GetChildAt(j);
                        child.Enabled = false;
                    }

                    btVocabularyContinue.Visibility = ViewStates.Visible;
                };
                lstTextView.Add(btnAnswer);
            }
            foreach (var item in lstTextView)
            {
                if(lstChoice.Count==0)
                {
                    break;
                }
                var answer = Item.Answers.Where(d => d.Text == item.Text).FirstOrDefault();
                if (lstChoice.Contains(item.Text))
                {
                    item.SetBackgroundColor(answer.Value ? ConstantAttributes.ColorCorrect : ConstantAttributes.ColorIncorrect);
                }
                if (answer?.Value == true)
                {
                    item.SetBackgroundColor(ConstantAttributes.ColorCorrect);
                    item.SetTextColor(Color.White);
                }
            }
            AddView(view, new ViewGroup.LayoutParams(-1, -1));
        }
    }
}