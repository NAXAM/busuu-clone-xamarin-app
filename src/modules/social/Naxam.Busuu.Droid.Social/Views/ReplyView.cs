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
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using System.Threading.Tasks;
using System.Threading;
using Android.Text;
using IO.Github.Douglasjunior.AndroidSimpleTooltip;
using Android.Graphics;
using Android.Views.InputMethods;
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Social.ViewModels;

namespace Naxam.Busuu.Droid.Social.Views
{
    [Activity(Label = "Reply", Theme = "@style/AppTheme.Reply")]
    public class ReplyView : MvxAppCompatActivity<ReplyViewModel>, View.IOnLongClickListener, View.IOnTouchListener
    {
        private int progress = 0;
        private TextView txtSwipe;
        private ProgressBar progressBar, circleProgressbar;
        private bool IsSend = false;
        private EditText edtMessage;
        private FloatingActionButton btnSend;
        private bool SendButtonLongPressed = false;

        public bool OnLongClick(View v)
        {
            if (IsSend == false)
            {
                txtSwipe.Visibility = ViewStates.Visible;
                progressBar.Visibility = ViewStates.Visible;
                Task.Run(async () =>
                {
                    while (true)
                    {
                        progressBar.Progress = progress;
                        await Task.Delay(100);
                        progress += 1;
                        if (progress == 100)
                        {

                            break;
                        }
                    }
                    txtSwipe.Visibility = ViewStates.Invisible;
                    progressBar.Visibility = ViewStates.Gone;
                    progress = 0;
                });
            }

            SendButtonLongPressed = true;
            return true;

        }

        public bool OnTouch(View pView, MotionEvent pEvent)
        {
            int THRESHOLD = 30;
            int initX = 0;
            int initY = 0;
            int initXtxt = 0;
            int initYtxt = 0;
            pView.OnTouchEvent(pEvent);

            if (pEvent.Action == MotionEventActions.Up)
            {



                if (SendButtonLongPressed)
                {
                    InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(pView.WindowToken, 0);
                    btnSend.Visibility = ViewStates.Gone;
                    circleProgressbar.Visibility = ViewStates.Visible;
                    // Do something when the button is released.
                    txtSwipe.SetX(initXtxt);
                    txtSwipe.SetY(initXtxt);
                    txtSwipe.Visibility = ViewStates.Gone;
                    progressBar.Visibility = ViewStates.Invisible;
                    progress = 0;
                    SendButtonLongPressed = false;
                }
            }
            else if (pEvent.Action == MotionEventActions.Move)
            {
                if (((int)pEvent.GetX() - initX) < -THRESHOLD)
                {

                    int a = (int)pEvent.GetX() - initX;
                    txtSwipe.TranslationX = (initXtxt + a);
                    if (a < -150)
                    {
                        txtSwipe.Visibility = ViewStates.Gone;
                        progressBar.Visibility = ViewStates.Invisible;
                        progress = 0;
                    }
                }
            }
            else if (pEvent.Action == MotionEventActions.Down)
            {
                initXtxt = (int)txtSwipe.GetX();
                initYtxt = (int)txtSwipe.GetY();
                //
                initX = (int)pEvent.GetX();
                initY = (int)pEvent.GetY();
            }


            return false;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_reply);
            Init();

        }
        private void Init()
        {
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.SetTitleTextColor(Color.Black);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            Window.SetSoftInputMode(SoftInput.AdjustResize);
            txtSwipe = FindViewById<TextView>(Resource.Id.txtSwipe);
            String str = "< swipe to cancle";
            txtSwipe.Text = str;
            circleProgressbar = FindViewById<ProgressBar>(Resource.Id.circleProgressbar);
            circleProgressbar.IndeterminateDrawable.SetColorFilter(Color.ParseColor("#33AAFA"), Android.Graphics.PorterDuff.Mode.Multiply);

            edtMessage = FindViewById<EditText>(Resource.Id.edtMessage);
            btnSend = FindViewById<FloatingActionButton>(Resource.Id.btnSend);
            progressBar = FindViewById<ProgressBar>(Resource.Id.mProgressBar);
            progressBar.Progress = 0;
            progressBar.Max = 100;
            //

            btnSend.SetOnLongClickListener(this);
            btnSend.SetOnTouchListener(this);
            //
            btnSend.Click += (s, e) =>
            {
                if (IsSend == true)
                {
                    InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(btnSend.WindowToken, 0);
                    btnSend.Visibility = ViewStates.Gone;
                    circleProgressbar.Visibility = ViewStates.Visible;

                }

            };
            edtMessage.TextChanged += (s, e) =>
            {
                if (e.Text.Count() == 0)
                {
                    btnSend.SetImageResource(Resource.Drawable.ic_mic);
                    IsSend = false;
                }
                else
                {
                    btnSend.SetImageResource(Resource.Drawable.ic_send_social);
                    IsSend = true;
                }
            };

        }

        public override bool OnSupportNavigateUp()
        {
            ViewModel.GoBackCommand?.Execute();
            return base.OnSupportNavigateUp();
        }


    }
}
