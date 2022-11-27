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
using Com.Github.Lzyzsd.Circleprogress;
using Android.Animation;
using static Android.Animation.ValueAnimator;
using IT.Sephiroth.Android.Library.Tooltip;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class RecorderButton : RelativeLayout
    {
        private Button btSpeak;
        private DonutProgress prgRecord;
        private Button btDelete;
        private Button btPlay;

        private int minTimeRecord = 0;
        private int maxTimeRecord = 30;
        private long startTimeRecord = 0;

        private bool isRecordComplete = false;
        private bool isPlay = false;
        bool isUpdate = false;

        public RecorderButton(Context context) : base(context)
        {
            Init(context);
        }

        public RecorderButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context);
        }

        public RecorderButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context);
        }

        public RecorderButton(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context);
        }

        protected RecorderButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public void Init(Context context)
        {
            View view = LayoutInflater.From(context).Inflate(Resource.Layout.recorder_button, null, true);
            AddView(view, new LayoutParams(-1, -1));

            btSpeak = view.FindViewById<Button>(Resource.Id.bt_speak);
            btDelete = view.FindViewById<Button>(Resource.Id.bt_delete);
            prgRecord = view.FindViewById<DonutProgress>(Resource.Id.prg_record);
            btPlay = view.FindViewById<Button>(Resource.Id.bt_play);

            //btn Delete layout param
            RelativeLayout.LayoutParams btnDeleteParam = (LayoutParams)btDelete.LayoutParameters;

            //set value for progress when record
            ValueAnimator progressAnimator = ValueAnimator.OfInt(0, 30);
            progressAnimator.SetDuration(30000);
            progressAnimator.AddUpdateListener(new NXAnimatorUpdateListener((a) =>
            {
                prgRecord.Progress = (int)a.AnimatedValue;
            }));

            //set animation for btn delete left to right
            ValueAnimator btnDeleteAnimatorL2R = ValueAnimator.OfInt(0, 64);
            btnDeleteAnimatorL2R.SetDuration(500);
            btnDeleteAnimatorL2R.AddUpdateListener(new NXAnimatorUpdateListener((a) =>
            {
                btnDeleteParam.MarginStart = ((int)a.AnimatedValue);
                btDelete.LayoutParameters = (btnDeleteParam);
            }));


            //set animation for btn delete right to left
            ValueAnimator btnDeleteAnimatorR2L = ValueAnimator.OfInt(64, -80);
            btnDeleteAnimatorR2L.SetDuration(500);
            btnDeleteAnimatorR2L.AddUpdateListener(new NXAnimatorUpdateListener((a) =>
            {
                btnDeleteParam.MarginStart = ((int)a.AnimatedValue);
                btDelete.LayoutParameters = (btnDeleteParam);
            }));

            //set animation for btn play rotate +
            ValueAnimator btnPLayAnimatorForward = ValueAnimator.OfInt(0, 360);
            btnPLayAnimatorForward.SetDuration(500);
            btnPLayAnimatorForward.AddUpdateListener(new NXAnimatorUpdateListener((a) =>
            {
                btPlay.Rotation = ((int)a.AnimatedValue);
            }));

            //set animation for btn play rotate -
            ValueAnimator btnPLayAnimatorBackward = ValueAnimator.OfInt(0, 360);
            btnPLayAnimatorBackward.SetDuration(500);
            btnPLayAnimatorBackward.AddUpdateListener(new NXAnimatorUpdateListener((a) =>
            {
                btPlay.Rotation = (-((int)a.AnimatedValue));
            }));

            btSpeak.SetOnTouchListener(new NXOnTouchListener((v, e) =>
            {
                switch (e.Action)
                {
                    case MotionEventActions.Down:
                        {
                            btSpeak.SetBackgroundResource(Resource.Drawable.ic_conversation_speak_red);
                            isUpdate = true;
                            prgRecord.Visibility = ViewStates.Visible;
                            progressAnimator.Start();
                        }
                        break;
                    case MotionEventActions.Up:
                        {
                            isUpdate = false;
                            progressAnimator.Cancel();

                            if (prgRecord.Progress < 3)
                            {
                                Tooltip.Make(context, new Tooltip.Builder(101)
                                        .Anchor(v, Tooltip.Gravity.Top)
                                        .ClosePolicy(Tooltip.ClosePolicy.TouchAnywhereNoConsume, 500)
                                        .Text("Tap and hold to record")
                                        .FadeDuration(200)
                                        .FitToScreen(false)
                                        .MaxWidth(400)
                                        .ToggleArrow(true)
                                        .Build()).Show();
                                btSpeak.SetBackgroundResource(Resource.Drawable.ic_conversation_speak_blue);
                                prgRecord.Visibility = ViewStates.Invisible;
                            }
                            else
                            {
                                btDelete.Visibility = ViewStates.Visible;
                                btPlay.Visibility = (ViewStates.Visible);
                                btSpeak.Visibility = (ViewStates.Invisible);
                                prgRecord.Visibility = (ViewStates.Invisible);
                                isRecordComplete = true;

                                AnimatorSet animatorSet = new AnimatorSet();
                                //animatorSet.Play(btnDeleteAnimatorL2R).With(btnPLayAnimatorForward);
                                //animatorSet.Start();
                                btnDeleteAnimatorL2R.Start();

                                prgRecord.Max = ((int)prgRecord.Progress);
                                prgRecord.Progress = 0;
                            }
                        }
                        break;
                }
            }));

            btPlay.Click += (s, e) =>
            {
                prgRecord.Progress = 0;
                btPlay.SetBackgroundResource(isPlay ? Resource.Drawable.ic_stop : Resource.Drawable.ic_play_blue);
                prgRecord.Visibility = ViewStates.Visible;
                isPlay = !isPlay;
            };

            btDelete.Click += (s, e) =>
            {
                AnimatorSet animatorSet = new AnimatorSet();
                animatorSet.Play(btnDeleteAnimatorR2L).With(btnPLayAnimatorBackward);
                animatorSet.Start();
                isRecordComplete = false;
                btPlay.Visibility = (ViewStates.Invisible);
                btSpeak.Visibility = (ViewStates.Visible);
                btSpeak.SetBackgroundResource(Resource.Drawable.ic_conversation_speak_blue);
                btPlay.SetBackgroundResource(Resource.Drawable.ic_play_blue);
                btDelete.Visibility = (ViewStates.Invisible);

                prgRecord.Max = (30);
                prgRecord.Progress = 0;
                prgRecord.Visibility = (ViewStates.Invisible);
            };
        }
    }

    class NXAnimatorUpdateListener : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener
    {
        Action<ValueAnimator> action;
        public NXAnimatorUpdateListener(Action<ValueAnimator> action)
        {
            this.action = action;
        }
        public void OnAnimationUpdate(ValueAnimator animation)
        {
            action?.Invoke(animation);
        }
    }

    class NXOnTouchListener : Java.Lang.Object, View.IOnTouchListener
    {
        Action<View, MotionEvent> action;
        public NXOnTouchListener(Action<View, MotionEvent> action)
        {
            this.action = action;
        }
        public bool OnTouch(View v, MotionEvent e)
        {
            action?.Invoke(v, e);
            return false;
        }
    }

    class NXClickListener : Java.Lang.Object, View.IOnClickListener
    {
        Action<View> action;
        public NXClickListener(Action<View> action)
        {
            this.action = action;
        }
        public void OnClick(View v)
        {
            action?.Invoke(v);
        }
    }
}