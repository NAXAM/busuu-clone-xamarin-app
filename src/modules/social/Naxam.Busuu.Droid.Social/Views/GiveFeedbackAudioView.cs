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
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Graphics.Drawables;
using Android.Views.Animations;
using Com.Dinuscxj.Progressbar;
using Android.Animation;
using Android.Graphics;
using IO.Github.Douglasjunior.AndroidSimpleTooltip;
using Naxam.Busuu.Social.ViewModels;

namespace Naxam.Busuu.Droid.Social.Views
{
    //TODO Missing class GiveFeedbackAudioViewModel
    //[Activity(Label = "Correct Exercise", Theme = "@style/AppTheme.NoActionBar")]
    //public class GiveFeedbackAudioView : MvxAppCompatActivity<GiveFeedbackAudioViewModel>, View.IOnTouchListener, View.IOnLongClickListener, View.IOnClickListener, ValueAnimator.IAnimatorUpdateListener
    //{
    //    SimpleTooltip tooltip;
    //    GradientDrawable TrashDrawable, BackgroundDrawable;
    //    ScaleAnimation anim_zoom_in, anim_zoom_out;
    //    int progress;
    //    bool isRecorded;
    //    bool isInProgress;
    //    bool isLongClicked;
    //    FrameLayout mFrame;
    //    CircleProgressBar CirleProgressbar;
    //    ImageView imgMic, imgTrash, imgBackground;
    //    ValueAnimator AnimationProgressbar;
    //    RotateAnimation animRotateLongClick, animRotateClick, animRotateChangePicture, animRotatePlayButton;
    //    private ImageView btnSend;
    //    Dialog dialog;

    //    public void OnAnimationUpdate(ValueAnimator valueAnimator)
    //    {
    //        progress = (int)valueAnimator.AnimatedValue;
    //        CirleProgressbar.Progress = progress;
    //        if (progress == 100)
    //        {
    //            SetProgressColor("#FFFFFF");
    //        }
    //    }

    //    public void OnClick(View v)
    //    {
    //        if (isRecorded && isInProgress == false)
    //        {
    //            // do stuff when finishing record an audio
    //            isInProgress = true;
    //            SetProgressColor("#3BA7F5");
    //            // maybe check this later :))
    //            AnimationProgressbar.Start();
    //            imgMic.StartAnimation(animRotateClick);
    //        }
    //        else if (isRecorded && isInProgress == true)
    //        {
    //            AnimationProgressbar.Cancel();
    //            SetProgressColor("#FFFFFF");
    //            imgMic.StartAnimation(animRotateChangePicture);
    //            isInProgress = false;

    //        }
    //        else
    //        {
    //            tooltip = new SimpleTooltip.Builder(this)
    //                .AnchorView(CirleProgressbar)
    //                .Text("Tap and hold to record")
    //                .DismissOnInsideTouch(true)
    //                .DismissOnOutsideTouch(true)
    //                .ArrowColor(Color.ParseColor("#000000"))
    //                .Gravity((int)GravityFlags.Top)
    //                .BackgroundColor(Color.ParseColor("#000000"))
    //                .TextColor(Color.ParseColor("#FFFFFF"))
    //                .Build();
    //            tooltip.Show();
    //        }

    //    }

    //    public bool OnLongClick(View v)
    //    {
    //        if (tooltip.IsShowing)
    //            tooltip.Dismiss();
    //        AnimationProgressbar.AddUpdateListener(this);


    //        isLongClicked = true;
    //        if (isRecorded == false)
    //        {
    //            BackgroundDrawable.SetColor(Color.ParseColor("#F1C7CA"));
    //            imgBackground.StartAnimation(anim_zoom_in);
    //            imgMic.SetImageResource(Resource.Drawable.ic_red_mic);
    //            SetProgressColor("#FF0000");
    //            AnimationProgressbar.Start();

    //        }

    //        return true;


    //    }

    //    public bool OnTouch(View v, MotionEvent e)
    //    {
    //        if (e.Action == MotionEventActions.Up && isLongClicked == true && isRecorded == false)
    //        {
    //            BackgroundDrawable.SetColor(Color.Transparent);
    //            anim_zoom_out.Cancel();
    //            imgTrash.Visibility = ViewStates.Visible;
    //            imgTrash.Animate().TranslationX(250).SetDuration(500).Start();
    //            imgMic.SetImageResource(Resource.Drawable.ic_blue_mic);
    //            imgMic.SetImageResource(Resource.Drawable.ic_blue_play_button);
    //            imgMic.StartAnimation(animRotateLongClick);
    //            AnimationProgressbar.End();
    //            isLongClicked = false;
    //            isRecorded = true;
    //        }
    //        return false;
    //    }

    //    protected override void OnCreate(Bundle savedInstanceState)
    //    {
    //        base.OnCreate(savedInstanceState);
    //        SetContentView(Resource.Layout.activity_give_feedback_audio);
    //        Init();
           
             
    //        //dialog = new Dialog(this);
    //        //btnSend.Click += (s, e) =>
    //        //{
    //        //    dialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
    //        //    dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
    //        //    dialog.SetContentView(Resource.Layout.dialog_send);
    //        //    dialog.Show();

    //        //};

    //    }
    //    private void Init()
    //    {
    //        var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
    //        toolbar.SetTitleTextColor(Color.White);
    //        SetSupportActionBar(toolbar);
    //        SupportActionBar.SetDisplayHomeAsUpEnabled(true);

    //        anim_zoom_in = new ScaleAnimation(1f, 1.5f, 1f, 1.5f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
    //        anim_zoom_in.FillAfter = true;
    //        anim_zoom_in.Duration = 600;
    //        anim_zoom_in.AnimationEnd += (s, e) =>
    //        {
    //            imgBackground.StartAnimation(anim_zoom_out);
    //        };

    //        anim_zoom_out = new ScaleAnimation(1.5f, 1f, 1.5f, 1f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
    //        anim_zoom_out.FillAfter = true;
    //        anim_zoom_out.Duration = 600;
    //        anim_zoom_out.AnimationEnd += (s, e) =>
    //        {
    //            imgBackground.StartAnimation(anim_zoom_in);
    //        };


    //        //
    //        BackgroundDrawable = new GradientDrawable();
    //        BackgroundDrawable.SetShape(ShapeType.Rectangle);
    //        BackgroundDrawable.SetColor(Color.ParseColor("#F1C7CA"));
    //        BackgroundDrawable.SetCornerRadius(1000);
    //        //
    //        TrashDrawable = new GradientDrawable();
    //        TrashDrawable.SetShape(ShapeType.Rectangle);
    //        TrashDrawable.SetColor(Color.ParseColor("#FFFFFF"));
    //        TrashDrawable.SetCornerRadius(1000);
    //        //

    //        isInProgress = false;
    //        isRecorded = false;
    //        isLongClicked = false;
    //        //
    //        imgTrash = (ImageView)FindViewById(Resource.Id.ic_red_trash_button);
    //        //
    //        animRotateLongClick = new RotateAnimation(0.0f, 360.0f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
    //        animRotateLongClick.Duration = 500;
    //        animRotateLongClick.FillAfter = true;

    //        //
    //        animRotatePlayButton = new RotateAnimation(0f, 360.0f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
    //        animRotatePlayButton.Duration = 600;
    //        animRotatePlayButton.Interpolator = new LinearInterpolator();
    //        animRotatePlayButton.FillAfter = false;
    //        //
    //        animRotateClick = new RotateAnimation(0.0f, 360.0f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
    //        animRotateClick.Duration = 500;
    //        animRotateClick.FillAfter = true;
    //        animRotateClick.AnimationEnd += (s, e) =>
    //        {
    //            imgMic.SetImageResource(Resource.Drawable.ic_blue_pause_button);

    //        };


    //        //
    //        animRotateChangePicture = new RotateAnimation(0.0f, 360.0f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
    //        animRotateChangePicture.Duration = 500;
    //        animRotateChangePicture.FillAfter = true;
    //        animRotateChangePicture.AnimationEnd += (s, e) =>
    //        {

    //            imgMic.SetImageResource(Resource.Drawable.ic_blue_play_button);
    //        };
    //        imgTrash.Click += (s, e) =>
    //        {
    //            isRecorded = false;
    //            isInProgress = false;
    //            isLongClicked = false;
    //            imgTrash.Visibility = ViewStates.Invisible;
    //            imgTrash.Animate().TranslationX(0).Start();
    //            imgMic.SetImageResource(Resource.Drawable.ic_blue_mic);
    //            imgMic.StartAnimation(animRotatePlayButton);
    //            AnimationProgressbar.Cancel();
    //            SetProgressColor("#FFFFFF");

    //        };

    //        //
    //        imgBackground = (ImageView)FindViewById(Resource.Id.imgBackground);
    //        imgBackground.Background = BackgroundDrawable;

    //        mFrame = (FrameLayout)FindViewById(Resource.Id.mFame);
    //        mFrame.BringToFront();
    //        imgMic = (ImageView)FindViewById(Resource.Id.imgMic);
    //        imgTrash = (ImageView)FindViewById(Resource.Id.ic_red_trash_button);
    //        imgTrash.Background = TrashDrawable;
    //        imgTrash.Visibility = ViewStates.Invisible;
    //        CirleProgressbar = (CircleProgressBar)FindViewById(Resource.Id.line_progress);
    //        CirleProgressbar.ProgressBackgroundColor = Color.ParseColor("#FFFFFF");
    //        SetProgressColor("#FF0000");
    //        CirleProgressbar.SetOnClickListener(this);
    //        CirleProgressbar.SetOnLongClickListener(this);
    //        CirleProgressbar.SetOnTouchListener(this);
    //        //    //
    //        AnimationProgressbar = ValueAnimator.OfInt(0, 100);
    //        UpdateUIProgressLongClick update = new UpdateUIProgressLongClick
    //        {
    //            AnimationUpdate = (d) =>
    //            {
    //                int progress = (int)d.AnimatedValue;
    //                CirleProgressbar.Progress = progress;
    //                if (progress == 100)
    //                {
    //                    SetProgressColor("#FFFFFF");
    //                    isInProgress = false;
    //                    imgMic.StartAnimation(animRotateChangePicture);
    //                }
    //            }
    //        };

    //        AnimationProgressbar.AddUpdateListener(update);
    //        AnimationProgressbar.RepeatCount = 0;
    //        AnimationProgressbar.SetDuration(7000);

    //        //
    //        tooltip = new SimpleTooltip.Builder(this)
    //                    .AnchorView(CirleProgressbar)
    //                    .Text("Tap and hold to record")
    //                    .DismissOnInsideTouch(true)
    //                    .DismissOnOutsideTouch(true)
    //                    .ArrowColor(Color.ParseColor("#000000"))
    //                    .Gravity((int)GravityFlags.Top)
    //                    .BackgroundColor(Color.ParseColor("#000000"))
    //                    .TextColor(Color.ParseColor("#FFFFFF"))
    //                    .Build();
    //    }


    //    private void SetProgressColor(String color)
    //    {
    //        CirleProgressbar.ProgressStartColor = Color.ParseColor(color);
    //        CirleProgressbar.ProgressEndColor = Color.ParseColor(color);
    //    }

    //    public class UpdateUIProgressLongClick : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener
    //    {
    //        public Action<ValueAnimator> AnimationUpdate;


    //        public void OnAnimationUpdate(ValueAnimator animation)
    //        {
    //            AnimationUpdate?.Invoke(animation);
    //        }
    //    }

    //    public override bool OnSupportNavigateUp()
    //    {
    //        ViewModel.BackCmd?.Execute();
    //        return base.OnSupportNavigateUp();
    //    }
    //}
}