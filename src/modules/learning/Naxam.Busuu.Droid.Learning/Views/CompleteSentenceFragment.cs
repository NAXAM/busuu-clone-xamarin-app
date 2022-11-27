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
using MvvmCross.Droid.Support.V4;
using Naxam.Busuu.Droid.Core.Controls;
using MvvmCross.Binding.Droid.BindingContext;
using Naxam.Busuu.Learning.ViewModels;
using Naxam.Busuu.Droid.Core;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [NxFragment(BusuuFragmentHosts.Memorise,  ViewModelType = typeof(CompleteSentenceViewModel))]
    public class CompleteSentenceFragment : MvxFragment<CompleteSentenceViewModel>
    {
        private TextView txtTitle;
        private TextView txtQuestion;
        private TextView txtTrueAnswer;
        private EditText edtAnswer;
        private Button btContinue;

        bool correct;


        private string[] listInputString;
        private int targetIndex = 0;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.complete_sentence_question, container, false);
            InitInterface(view);
            return view;
        }

        protected override void Dispose(bool disposing)
        {
            InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(Activity.CurrentFocus.WindowToken, HideSoftInputFlags.None);
            base.Dispose(disposing);
        }


        public void InitInterface(View view)
        {
            txtTitle = view.FindViewById<TextView>(Resource.Id.txt_compelte_sentence_title);
            txtQuestion = view.FindViewById<TextView>(Resource.Id.txt_compelte_sentence_question);
            txtTrueAnswer = view.FindViewById<TextView>(Resource.Id.txt_compelte_sentence_true_answer);
            edtAnswer = view.FindViewById<EditText>(Resource.Id.edt_compelte_sentence_question);
            btContinue = view.FindViewById<Button>(Resource.Id.bt_complete_sentence_continue);
            listInputString = SplitStringToList(ViewModel.Item.Inputs[0]);
            targetIndex = GetTargetIndex(ViewModel.Item.Inputs[0]);

            txtQuestion.SetText(CreateStringBuilder(listInputString, targetIndex), BufferType.Normal);
            txtTrueAnswer.SetText(CreateTrueAnswer(listInputString, ViewModel.Item.Answers[0].Text, targetIndex), BufferType.Normal);
            txtTrueAnswer.Visibility = ViewStates.Invisible;

            txtQuestion.Click += (s, e) =>
            {
                InputMethodManager imm = (InputMethodManager)view.Context.GetSystemService(Context.InputMethodService);
                imm.ShowSoftInput(edtAnswer, ShowFlags.Implicit);
            };

            edtAnswer.TextChanged += (s, e) =>
            {
                if (edtAnswer.Text.Trim().Length != 0)
                {
                    txtQuestion.SetText(FillingAnswer(edtAnswer.Text, Color.ParseColor("#38A9F6")), BufferType.Normal);
                }
                else
                {
                    txtQuestion.SetText(CreateStringBuilder(listInputString, targetIndex), BufferType.Normal);
                }
                    
            };

            btContinue.Click += (s, e) =>
            {
                if (ViewModel.IsCompleted)
                {
                    ViewModel.NextCommand?.Execute();
                    return;
                }
                ViewModel.IsCompleted = true;
                txtQuestion.SetText(CheckAnswer(edtAnswer.Text.Trim().ToLower(), ViewModel.Item.Answers[0].Text.Trim().ToLower()), BufferType.Normal);
                txtTrueAnswer.Visibility = ViewStates.Visible;
                txtQuestion.Enabled = false;
                btContinue.Enabled = true;
                ViewModel.IsCorrect = correct;
            };
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
                        spstring.SetSpan(new ForegroundColorSpan(Resources.GetColor(Resource.Color.colorAnswer)), 0, inputString.Length, SpanTypes.ExclusiveExclusive);
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
                        spstring.SetSpan(new ForegroundColorSpan(Color.ParseColor("#ee6253")), 0, inputString.Length, SpanTypes.ExclusiveExclusive);
                        spstring.SetSpan(new StrikethroughSpan(), 0, inputString.Length, SpanTypes.ExclusiveExclusive);
                        spstring.SetSpan(new ForegroundColorSpan(Resources.GetColor(Resource.Color.colorAnswer)), inputString.Length + 1, inputString.Length + 1 + compareString.Length, SpanTypes.ExclusiveExclusive);
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