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
using Com.Bumptech.Glide;
using Android.Text;
using static Android.Widget.TextView;
using Android.Text.Style;
using Android.Graphics;
using Android.Views.InputMethods;
using Naxam.Busuu.Droid.Core;
using Android.Content.Res;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Learning.Control.Memo
{
    public class CompleteSentenceView : MemoBaseView
    {
        public override event EventHandler<int> NextClick;

        private ImageView imgImage;
        private NXPlayButton btnPlay;
        private TextView txtTitle;
        private TextView txtQuestion;
        private TextView txtTrueAnswer;
        private EditText edtAnswer;
        private Button btContinue;
        string textResult;
        private string[] listInputString;
        private int targetIndex = 0;
        bool correct, isCompleted;

        public CompleteSentenceView(Context context, UnitModel unit) : base(context)
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
            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.complete_sentence_question, null);
            imgImage = view.FindViewById<ImageView>(Resource.Id.img_complete_sentence_description);
            btnPlay = view.FindViewById<NXPlayButton>(Resource.Id.btn_complete_sentence_play);
            txtTitle = view.FindViewById<TextView>(Resource.Id.txt_compelte_sentence_title);
            txtQuestion = view.FindViewById<TextView>(Resource.Id.txt_compelte_sentence_question);
            txtTrueAnswer = view.FindViewById<TextView>(Resource.Id.txt_compelte_sentence_true_answer);
            edtAnswer = view.FindViewById<EditText>(Resource.Id.edt_compelte_sentence_question);
            btContinue = view.FindViewById<Button>(Resource.Id.bt_complete_sentence_continue);
            btContinue.Visibility = ViewStates.Gone;
            listInputString = SplitStringToList(Item.Inputs[0]);
            targetIndex = GetTargetIndex(Item.Inputs[0]);

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

            if (Item.Audio == null)
            {
                btnPlay.Visibility = ViewStates.Gone;
            }

            if (Item.Title != null)
            {
                txtTitle.Text = Item.Title;
            }
            else
            {
                txtTitle.Visibility = ViewStates.Gone;
            }
            InputMethodManager imm = (InputMethodManager)view.Context.GetSystemService(Context.InputMethodService);
            //imm.ToggleSoftInput(ShowFlags.Forced, 0);
            imm.HideSoftInputFromWindow(edtAnswer.WindowToken, HideSoftInputFlags.ImplicitOnly);
            imm.ShowSoftInput(edtAnswer, ShowFlags.Implicit);

            txtQuestion.SetText(CreateStringBuilder(listInputString, targetIndex), BufferType.Normal);
            txtTrueAnswer.SetText(CreateTrueAnswer(listInputString, Item.Answers[0].Text, targetIndex), BufferType.Normal);
            txtTrueAnswer.Visibility = ViewStates.Gone;

            txtQuestion.Click += (s, e) =>
            {
                imm.ShowSoftInput(edtAnswer, ShowFlags.Implicit);
                btContinue.Visibility = ViewStates.Gone;
            };
            edtAnswer.SetSingleLine(true);
            edtAnswer.FocusChange += (s, e) =>
            {
                if (e.HasFocus)
                {
                    btContinue.Visibility = ViewStates.Gone;
                }
            };
            edtAnswer.EditorAction += (s, e) =>
            {
                if (e.ActionId == ImeAction.Done)
                {
                    btContinue.Visibility = ViewStates.Visible;
                    //imm.HideSoftInputFromInputMethod(edtAnswer.WindowToken, HideSoftInputFlags.ImplicitOnly);
                    imm.HideSoftInputFromWindow(edtAnswer.WindowToken, HideSoftInputFlags.ImplicitOnly);
                }
            };
            edtAnswer.ImeOptions = ImeAction.Done;
            edtAnswer.InputType = InputTypes.ClassText;
            edtAnswer.TextChanged += (s, e) =>
            {
                textResult = e.Text.ToString();
                if (edtAnswer.Text.Trim().Length != 0)
                {
                    txtQuestion.SetText(FillingAnswer(edtAnswer.Text, ConstantAttributes.ColorPrimary), BufferType.Normal);
                    return;
                }
                txtQuestion.SetText(CreateStringBuilder(listInputString, targetIndex), BufferType.Normal);
            };

            if (IsCompleted)
            {
                txtQuestion.SetText(CheckAnswer(edtAnswer.Text.Trim().ToLower(), Item.Answers[0].Text.Trim().ToLower()), BufferType.Normal);
                txtTrueAnswer.Visibility = ViewStates.Visible;
                txtQuestion.Enabled = false;
            }
            if (string.IsNullOrEmpty(textResult)==false|| string.IsNullOrWhiteSpace(textResult)==false)
            {
                edtAnswer.Text = textResult;
                txtQuestion.SetText(FillingAnswer(edtAnswer.Text, ConstantAttributes.ColorPrimary), BufferType.Normal);
            }
            btContinue.Click += (s, e) =>
            {
                if (isCompleted)
                {
                    NextClick?.Invoke(btContinue, correct ? 1 : 0);
                    return;
                }
                txtQuestion.SetText(CheckAnswer(edtAnswer.Text.Trim().ToLower(), Item.Answers[0].Text.Trim().ToLower()), BufferType.Normal);
                txtTrueAnswer.Visibility = ViewStates.Visible;
                txtQuestion.Enabled = false;
                isCompleted = true;

            };

            AddView(view, new ViewGroup.LayoutParams(-1, -1));
        }

        public SpannableStringBuilder FillingAnswer(string inputString, Color color)
        {
            SpannableStringBuilder builder = new SpannableStringBuilder();
            for (int i = 0; i < listInputString.Length; i++)
            {
                builder.Append(listInputString[i]);
                if (i < listInputString.Length - 1)
                {
                    SpannableString spstring = new SpannableString(inputString);
                    spstring.SetSpan(new ForegroundColorSpan(color), 0, inputString.Length, SpanTypes.ExclusiveExclusive);
                    builder.Append(spstring);
                }
            }
            return builder;
        }

        public SpannableStringBuilder CheckAnswer(string inputString, string compareString)
        {
            SpannableStringBuilder builder = new SpannableStringBuilder();
            correct = true;
            if (inputString.Trim().Equals(compareString.Trim()))
            {
                for (int i = 0; i < listInputString.Length; i++)
                {
                    builder.Append(listInputString[i]);
                    if (i < listInputString.Length - 1)
                    {
                        SpannableString spstring = new SpannableString(inputString);
                        spstring.SetSpan(new ForegroundColorSpan(ConstantAttributes.ColorCorrect), 0, inputString.Length, SpanTypes.ExclusiveExclusive);
                        builder.Append(spstring);
                    }
                }
            }
            else
            {
                correct = false;
                for (int i = 0; i < listInputString.Length; i++)
                {
                    builder.Append(listInputString[i]);
                    if (i < listInputString.Length - 1)
                    {
                        SpannableString spstring = new SpannableString(inputString + " " + compareString);
                        spstring.SetSpan(new ForegroundColorSpan(ConstantAttributes.ColorIncorrect), 0, inputString.Length, SpanTypes.ExclusiveExclusive);
                        spstring.SetSpan(new StrikethroughSpan(), 0, inputString.Length, SpanTypes.ExclusiveExclusive);
                        spstring.SetSpan(new ForegroundColorSpan(ConstantAttributes.ColorCorrect), inputString.Length + 1, inputString.Length + 1 + compareString.Length, SpanTypes.ExclusiveExclusive);
                        builder.Append(spstring);
                    }
                }
            }

            return builder;
        }

        public SpannableStringBuilder CreateStringBuilder(string[] listInputString, int target)
        {
            SpannableStringBuilder builder = new SpannableStringBuilder();
            for (int i = 0; i < listInputString.Length; i++)
            {
                builder.Append(listInputString[i]);
                if (i < listInputString.Length - 1)
                {
                    builder.Append("_____");
                }
            }

            return builder;
        }

        public SpannableStringBuilder CreateTrueAnswer(string[] listInputString, string trueAnswer, int targetIndex)
        {

            SpannableStringBuilder builder = new SpannableStringBuilder();
            for (int i = 0; i < listInputString.Length; i++)
            {
                builder.Append(listInputString[i]);
                if (i < listInputString.Length - 1)
                {
                    SpannableString spstring = new SpannableString(trueAnswer);
                    spstring.SetSpan(new StyleSpan(TypefaceStyle.Bold), 0, trueAnswer.Length, SpanTypes.ExclusiveExclusive);
                    builder.Append(spstring);
                }
            }

            return builder;
        }
        public string[] SplitStringToList(string inputString)
        {
            inputString = " " + inputString + " ";
            return inputString.Split(new string[] { "%%" }, StringSplitOptions.None);
        }

        public int GetTargetIndex(string inputString)
        {
            int start = 0, count = 0;
            for (int i = 1; i < inputString.Length; i++)
            {
                if (inputString[i].Equals('%') && inputString[i].Equals('%'))
                {
                    return i - 1;
                }
            }
            return 0;
        }
    }
}