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
using Com.Bumptech.Glide;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Text;
using Android.Text.Style;
using static Android.Widget.TextView;
using System.Text.RegularExpressions;
using Naxam.Busuu.Droid.Learning.Control.Memo;
using Android.Content.Res;
using Naxam.Busuu.Droid.Core;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class FillSentenceView : MemoBaseView
    {
        public override event EventHandler<int> NextClick;
        private event EventHandler<AnswerModel> AnswerClick;
        bool result;
        List<string> listString;
        Button btnNext;
        List<int> listIndex;
        List<AnswerModel> listCorrect;
        Dictionary<TextView, AnswerModel> listChoice;
        List<string> lstStringChoice;
        int CountCorrect;
        FlexboxLayout flexAnswer;
        public object FlexBoxLayout { get; private set; }
        string input;
        TextView txtInput;
        public FillSentenceView(Context context, UnitModel unit) : base(context)
        {
            Item = unit;
            Init(context);
        }

        private Drawable GetBackground(Color color)
        {
            PaintDrawable background = new PaintDrawable(color);
            background.SetCornerRadius((int)Util.Util.PxFromDp(Context, 4));
            background.Shape = new RectShape();
            return background;
        }

        protected override void OnConfigurationChanged(Configuration newConfig)
        {
            AnswerClick -= FillSentenceView_AnswerClick;
            Init(Context);
            base.OnConfigurationChanged(newConfig);
        }

        public void Init(Context context)
        {
            RemoveAllViews();
            lstStringChoice = lstStringChoice ?? new List<string>();
            listString = new List<string>();
            listIndex = new List<int>();
            listChoice = new Dictionary<TextView, AnswerModel>();
            listCorrect = Item.Answers.Where(d => d.Value).OrderBy(d => d.Position).ToList();
            CountCorrect = Item.Answers.Where(d => d.Value).ToList().Count;

            View view = LayoutInflater.FromContext(context).Inflate(string.IsNullOrEmpty(Item.Image) ? Resource.Layout.fill_sentence_layout_non_image : Resource.Layout.fill_sentence_layout, null);

            ImageView imgImage = view.FindViewById<ImageView>(Resource.Id.imgImage);
            TextView txtQuestion = view.FindViewById<TextView>(Resource.Id.txtQuestion);
            txtInput = view.FindViewById<TextView>(Resource.Id.txtInput);
            flexAnswer = view.FindViewById<FlexboxLayout>(Resource.Id.flexAnswer);
            btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            NXPlayButton btnPlay = view.FindViewById<NXPlayButton>(Resource.Id.btnPlay);
            listString = Regex.Split(Item.Inputs[0], "%%").ToList();
            if (Item.Audio != null)
            {

            }
            else
            {
                btnPlay.Visibility = ViewStates.Gone;
            }
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
                if (context.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape || context.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Undefined)
                {
                    imgImage.LayoutParameters.Width = (int)measuredWidth / 2;
                    imgImage.LayoutParameters.Height = (int)measuredWidth / 5;
                }
                if (context.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Portrait || context.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Square)
                {
                    imgImage.LayoutParameters.Width = (int)measuredWidth;
                    imgImage.LayoutParameters.Height = (int)measuredWidth * 2 / 5;
                }
                Glide.With(Context).Load(Item.Image).Into(imgImage);
            }
            else
            {
                imgImage.Visibility = ViewStates.Gone;
            }

            txtQuestion.Text = Item.Title;
            input = "";
            for (int i = 0; i < listString.Count; i++)
            {
                if (i < listString.Count - 1)
                {
                    input = input + listString[i] + "_____";
                    listIndex.Add(input.Length - 5);
                }
                else
                {
                    input = input + listString[i];
                }
            }
            SpannableStringBuilder ssb = new SpannableStringBuilder(input);
            for (int i = 0; i < listIndex.Count; i++)
            {
                //ssb.SetSpan(new UnderlineSpan(), listIndex[i], listIndex[i] + 5, SpanTypes.InclusiveInclusive);
            }


            txtInput.SetText(ssb, BufferType.Normal);
            btnNext.Click += (s, e) =>
            {
                this.NextClick?.Invoke(btnNext, result ? 1 : 0);
            };
            btnNext.Visibility = ViewStates.Invisible;
            int margin = (int)Util.Util.PxFromDp(Context, 8);
            for (int i = 0; i < Item.Answers.Count; i++)
            {
                TextView btn = new TextView(Context);
                btn.SetTextColor(Color.Black);
                btn.SetPadding(margin * 2, margin, margin * 2, margin);
                if (Android.OS.Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                {
                    btn.Elevation = margin / 4;
                }
                btn.SetTextSize(ComplexUnitType.Sp, 14);
                btn.TransformationMethod = null;

                var answer = Item.Answers.ElementAt(i);
                btn.Text = answer.Text;
                flexAnswer.AddView(btn, new ViewGroup.LayoutParams(-2, -2));
                FlexboxLayout.LayoutParams layoutparam = (FlexboxLayout.LayoutParams)btn.LayoutParameters;

                layoutparam.SetMargins(margin, margin, margin, margin);

                btn.Background = GetBackground(Color.White);

                if (lstStringChoice.Count > 0)
                {
                    if (lstStringChoice.Contains(answer.Text))
                    {
                        listChoice.Add(btn, answer);
                        btn.Background = GetBackground(ConstantAttributes.ColorPrimaryLight);
                    }
                }
                btn.Click += (s, e) =>
                {
                    btn.Background = GetBackground(ConstantAttributes.ColorPrimaryLight);
                    AnswerClick?.Invoke(btn, answer);
                };
            }

            if (lstStringChoice.Count > 0)
            {
                if (lstStringChoice.Count == CountCorrect)
                {
                    btnNext.Visibility = ViewStates.Visible;
                    input = "";
                    listIndex = new List<int>();
                    for (int i = 0; i < listString.Count; i++)
                    {
                        if (i < listString.Count - 1)
                        {
                            if (listCorrect[i].Text == listChoice.Values.ElementAt(i).Text)
                            {
                                input += listString[i] + listCorrect[i].Text;
                                listIndex.Add(input.Length - listCorrect[i].Text.Length);

                            }
                            else
                            {
                                input += listString[i] + listChoice.Values.ElementAt(i).Text + " " + listCorrect[i].Text;
                                listIndex.Add(input.Length - 1 - listCorrect[i].Text.Length - listChoice.Values.ElementAt(i).Text.Length);
                            }
                        }
                        else
                        {
                            input += listString[i];
                        }
                    }
                    result = true;
                    SpannableStringBuilder ssbt = new SpannableStringBuilder(input);
                    for (int i = 0; i < listIndex.Count; i++)
                    {
                        if (listCorrect[i].Text == listChoice.Values.ElementAt(i).Text)
                        {
                            ssbt.SetSpan(new StyleSpan(TypefaceStyle.Bold), listIndex[i], listIndex[i] + listCorrect[i].Text.Length, SpanTypes.InclusiveInclusive);
                            ssbt.SetSpan(new ForegroundColorSpan(ConstantAttributes.ColorCorrect), listIndex[i], listIndex[i] + listCorrect[i].Text.Length, SpanTypes.InclusiveInclusive);
                        }
                        else
                        {
                            result = false;
                            ssbt.SetSpan(new StyleSpan(TypefaceStyle.Bold), listIndex[i], listIndex[i] + listCorrect[i].Text.Length + listChoice.Values.ElementAt(i).Text.Length + 1, SpanTypes.InclusiveInclusive);
                            ssbt.SetSpan(new ForegroundColorSpan(ConstantAttributes.ColorIncorrect), listIndex[i], listIndex[i] + listChoice.Values.ElementAt(i).Text.Length, SpanTypes.InclusiveInclusive);
                            ssbt.SetSpan(new StrikethroughSpan(), listIndex[i], listIndex[i] + listChoice.Values.ElementAt(i).Text.Length, SpanTypes.InclusiveInclusive);
                            ssbt.SetSpan(new ForegroundColorSpan(ConstantAttributes.ColorCorrect), listIndex[i] + listChoice.Values.ElementAt(i).Text.Length + 1, listIndex[i] + listCorrect[i].Text.Length + listChoice.Values.ElementAt(i).Text.Length + 1, SpanTypes.InclusiveInclusive);
                        }
                    }
                    txtInput.SetText(ssbt, BufferType.Normal);
                    for (int i = 0; i < flexAnswer.ChildCount; i++)
                    {
                        View viewChild = flexAnswer.GetChildAt(i);
                        if (viewChild.Enabled)
                        {
                            viewChild.Enabled = false;
                        }
                    }
                }
                else
                {
                    input = "";
                    listIndex = new List<int>();
                    for (int i = 0; i < listString.Count; i++)
                    {
                        if (i < listString.Count - 1)
                        {
                            if (i < listChoice.Keys.Count)
                            {
                                input += listString[i] + listChoice.Keys.ElementAt(i).Text;
                                listIndex.Add(input.Length - listChoice.Keys.ElementAt(i).Text.Length);
                            }
                            else
                            {
                                input += listString[i] + "_____";
                                listIndex.Add(input.Length - 5);
                            }
                        }
                        else
                        {
                            input += listString[i];
                        }
                    }
                    SpannableStringBuilder ssbt = new SpannableStringBuilder(input);
                    for (int i = 0; i < listIndex.Count; i++)
                    {
                        if (i < listChoice.Keys.Count)
                        {
                            ssbt.SetSpan(new StyleSpan(TypefaceStyle.Bold), listIndex[i], listIndex[i] + listChoice.Keys.ElementAt(i).Text.Length, SpanTypes.InclusiveInclusive);
                            ssbt.SetSpan(new ForegroundColorSpan(Color.ParseColor("#A7B0B7")), listIndex[i], listIndex[i] + listChoice.Keys.ElementAt(i).Text.Length, SpanTypes.InclusiveInclusive);
                        }
                        else
                        {
                            ssbt.SetSpan(new UnderlineSpan(), listIndex[i], listIndex[i] + 5, SpanTypes.InclusiveInclusive);
                        }
                    }
                    txtInput.SetText(ssbt, BufferType.Normal);
                }
            }
            AnswerClick += FillSentenceView_AnswerClick;

            AddView(view, new ViewGroup.LayoutParams(-1, -1));
        }

        private void FillSentenceView_AnswerClick(object sender, AnswerModel e)
        {
            TextView btn = (TextView)sender;
            if (listChoice.Keys.Contains(btn))
            {
                lstStringChoice.Remove(btn.Text);
                listChoice.Remove(btn);
                btn.Background = GetBackground(Color.White);

            }
            else
            {
                lstStringChoice.Add(btn.Text);
                listChoice.Add(btn, e);
            }

            if (CountCorrect == listChoice.Count)
            {
                input = "";
                listIndex = new List<int>();
                for (int i = 0; i < listString.Count; i++)
                {
                    if (i < listString.Count - 1)
                    {
                        if (listCorrect[i].Text == listChoice.Values.ElementAt(i).Text)
                        {
                            input += listString[i] + listCorrect[i].Text;
                            listIndex.Add(input.Length - listCorrect[i].Text.Length);

                        }
                        else
                        {
                            input += listString[i] + listChoice.Values.ElementAt(i).Text + " " + listCorrect[i].Text;
                            listIndex.Add(input.Length - 1 - listCorrect[i].Text.Length - listChoice.Values.ElementAt(i).Text.Length);
                        }
                    }
                    else
                    {
                        input += listString[i];
                    }
                }
                result = true;
                SpannableStringBuilder ssbt = new SpannableStringBuilder(input);
                for (int i = 0; i < listIndex.Count; i++)
                {
                    if (listCorrect[i].Text == listChoice.Values.ElementAt(i).Text)
                    {
                        ssbt.SetSpan(new StyleSpan(TypefaceStyle.Bold), listIndex[i], listIndex[i] + listCorrect[i].Text.Length, SpanTypes.InclusiveInclusive);
                        ssbt.SetSpan(new ForegroundColorSpan(ConstantAttributes.ColorCorrect), listIndex[i], listIndex[i] + listCorrect[i].Text.Length, SpanTypes.InclusiveInclusive);
                    }
                    else
                    {
                        result = false;
                        ssbt.SetSpan(new StyleSpan(TypefaceStyle.Bold), listIndex[i], listIndex[i] + listCorrect[i].Text.Length + listChoice.Values.ElementAt(i).Text.Length + 1, SpanTypes.InclusiveInclusive);
                        ssbt.SetSpan(new ForegroundColorSpan(ConstantAttributes.ColorIncorrect), listIndex[i], listIndex[i] + listChoice.Values.ElementAt(i).Text.Length, SpanTypes.InclusiveInclusive);
                        ssbt.SetSpan(new StrikethroughSpan(), listIndex[i], listIndex[i] + listChoice.Values.ElementAt(i).Text.Length, SpanTypes.InclusiveInclusive);
                        ssbt.SetSpan(new ForegroundColorSpan(ConstantAttributes.ColorCorrect), listIndex[i] + listChoice.Values.ElementAt(i).Text.Length + 1, listIndex[i] + listCorrect[i].Text.Length + listChoice.Values.ElementAt(i).Text.Length + 1, SpanTypes.InclusiveInclusive);
                    }
                }
                txtInput.SetText(ssbt, BufferType.Normal);
                for (int i = 0; i < flexAnswer.ChildCount; i++)
                {
                    View viewChild = flexAnswer.GetChildAt(i);
                    if (viewChild.Enabled)
                    {
                        viewChild.Enabled = false;
                    }
                }

                btnNext.Visibility = ViewStates.Visible;
            }
            else
            {
                input = "";
                listIndex = new List<int>();
                for (int i = 0; i < listString.Count; i++)
                {
                    if (i < listString.Count - 1)
                    {
                        if (i < listChoice.Keys.Count)
                        {
                            input += listString[i] + listChoice.Keys.ElementAt(i).Text;
                            listIndex.Add(input.Length - listChoice.Keys.ElementAt(i).Text.Length);
                        }
                        else
                        {
                            input += listString[i] + "_____";
                            listIndex.Add(input.Length - 5);
                        }
                    }
                    else
                    {
                        input += listString[i];
                    }
                }
                SpannableStringBuilder ssbt = new SpannableStringBuilder(input);
                for (int i = 0; i < listIndex.Count; i++)
                {
                    if (i < listChoice.Keys.Count)
                    {
                        ssbt.SetSpan(new StyleSpan(TypefaceStyle.Bold), listIndex[i], listIndex[i] + listChoice.Keys.ElementAt(i).Text.Length, SpanTypes.InclusiveInclusive);
                        ssbt.SetSpan(new ForegroundColorSpan(Color.ParseColor("#A7B0B7")), listIndex[i], listIndex[i] + listChoice.Keys.ElementAt(i).Text.Length, SpanTypes.InclusiveInclusive);
                    }
                    else
                    {
                        ssbt.SetSpan(new UnderlineSpan(), listIndex[i], listIndex[i] + 5, SpanTypes.InclusiveInclusive);
                    }
                }
                txtInput.SetText(ssbt, BufferType.Normal);
            }
        }
    }
}