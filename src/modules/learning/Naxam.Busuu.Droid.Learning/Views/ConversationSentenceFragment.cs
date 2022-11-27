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
using Android.Animation;
using Android.Views.Animations;
using Com.Bumptech.Glide;
using MvvmCross.Droid.Support.V4;
using Naxam.Busuu.Droid.Learning.Control;
using MvvmCross.Binding.Droid.BindingContext;
using Naxam.Busuu.Droid.Core;
using Naxam.Busuu.Learning.ViewModels;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [NxFragment(BusuuFragmentHosts.Memorise,   ViewModelType = typeof(ConversationSentenceViewModel))]
    public class ConversationSentenceFragment : MvxFragment<ConversationSentenceViewModel>
    { 
        private TextView txtHint;
        private TextView txtShowHint;
        private LinearLayout layoutInputMethod;
        private LinearLayout layoutInputWrite;
        private LinearLayout layoutInputSpeak;
        private Button btWrite;
        private Button btSpeak;
        private View viewSpace;
        private EditText edtAnswer;
        private RecorderButton btRecord;
        private RelativeLayout layoutSent;
        private TextView txtSuggestNumberWord;
        private Button btSent;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.conversation_question, container, false);
            InitInterface(view);
            return view;
        }

        public void InitInterface(View view)
        { 
            txtHint = view.FindViewById<TextView>(Resource.Id.txt_hint);
            txtShowHint = view.FindViewById<TextView>(Resource.Id.txt_show_hint);
            layoutInputMethod = view.FindViewById<LinearLayout>(Resource.Id.layout_input_method);
            layoutInputWrite = view.FindViewById<LinearLayout>(Resource.Id.layout_input_write);
            layoutInputSpeak = view.FindViewById<LinearLayout>(Resource.Id.layout_input_speak);
            btWrite = view.FindViewById<Button>(Resource.Id.bt_write);
            btSpeak = view.FindViewById<Button>(Resource.Id.bt_speak);
            edtAnswer = view.FindViewById<EditText>(Resource.Id.edt_answer);
            btRecord = view.FindViewById<RecorderButton>(Resource.Id.bt_record);
            layoutSent = view.FindViewById<RelativeLayout>(Resource.Id.layout_sent);
            txtSuggestNumberWord = view.FindViewById<TextView>(Resource.Id.txt_suggest_number_word);
            btSent = view.FindViewById<Button>(Resource.Id.bt_sent);
            viewSpace = view.FindViewById<View>(Resource.Id.view_space);
            txtShowHint.Click += (s, e) =>
            {
                txtHint.Visibility = txtHint.Visibility == ViewStates.Gone ? ViewStates.Visible : ViewStates.Gone;
            };

            LinearLayout.LayoutParams param = (LinearLayout.LayoutParams)viewSpace.LayoutParameters;

            btWrite.Click += (s, e) =>
            {
                layoutInputSpeak.Visibility = ViewStates.Gone;
                ValueAnimator animator = ValueAnimator.OfInt(64, 0);
                animator.SetDuration(500);
                animator.Update += (sa, ea) =>
                {
                    param.Width = (int)ea.Animation.AnimatedValue;
                    viewSpace.LayoutParameters = param;
                };

                ObjectAnimator animatorEdtAnswer = ObjectAnimator.OfFloat(edtAnswer, "ScaleX", 0, 1);
                animatorEdtAnswer.SetDuration(500);

                animator.AnimationEnd += (sa, ea) =>
                {
                    layoutInputMethod.Visibility = ViewStates.Gone;
                    edtAnswer.Visibility = ViewStates.Visible;
                    layoutSent.Visibility = ViewStates.Visible;
                    animatorEdtAnswer.Start();
                };

                animator.Start();

            };

            btSpeak.Click += (s, e) =>
            {
                layoutInputWrite.Visibility = ViewStates.Gone;
                ValueAnimator animator = ValueAnimator.OfInt(64, 0);
                animator.SetDuration(500);
                animator.Update += (sa, ea) =>
                {
                    param.Width = (int)ea.Animation.AnimatedValue;
                    viewSpace.LayoutParameters = param;
                };

                animator.AnimationEnd += (sa, es) =>
                {
                    layoutInputSpeak.Visibility = ViewStates.Gone;
                    btRecord.Visibility = ViewStates.Visible;
                };

                animator.Start();
            };

            edtAnswer.TextChanged += (s, e) =>
            {
                int wordCount = (edtAnswer.Text.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)).Length;
                if (3 - wordCount > 0) txtSuggestNumberWord.Text = (3 - wordCount) + " words to go";
                else txtSuggestNumberWord.Text = "Ready to go! :D";
            };
        }

        private void Animator_Update(object sender, ValueAnimator.AnimatorUpdateEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ResetToDefault()
        {
            txtHint.Visibility = ViewStates.Gone;
            layoutInputMethod.Visibility = ViewStates.Visible;
            btSpeak.Visibility = ViewStates.Visible;
            btWrite.Visibility = ViewStates.Visible;
            layoutSent.Visibility = ViewStates.Gone;
            txtSuggestNumberWord.Text = "3 words to complete";
            edtAnswer.Visibility = ViewStates.Gone;
            viewSpace.LayoutParameters.Width = 64;
        }


    }
}