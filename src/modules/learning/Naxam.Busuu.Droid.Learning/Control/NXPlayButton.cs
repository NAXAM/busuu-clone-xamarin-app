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
using Android.Util;
using Android.Graphics;
using Com.Google.Android.Exoplayer;
using Com.Longtailvideo.Jwplayer;
using Com.Longtailvideo.Jwplayer.Configuration;
using Naxam.Busuu.Droid.Learning.Util;
using Com.Longtailvideo.Jwplayer.Events.Listeners;
using static Naxam.Busuu.Droid.Learning.Control.NXPlayButton;
using Com.Longtailvideo.Jwplayer.Core;
using Com.Longtailvideo.Jwplayer.Events;
using Com.Longtailvideo.Jwplayer.Media.Playlists;
using Naxam.Busuu.Droid.Core.Listener;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class NXPlayButton : FrameLayout,
        IVideoPlayerEventsOnTimeListener,
        IVideoPlayerEventsOnPlayListener,
        IVideoPlayerEventsOnPauseListener,
        IVideoPlayerEventsOnCompleteListener,
        IVideoPlayerEventsOnErrorListenerV2
    {

        public event EventHandler<long> PositionChanged;
        private ImageView imIcon;
        public string Url;
        private bool isPlay;
        public bool IsPlay { get { return isPlay; } }
        private int widthControl = 0;
        private int heightControl = 0;
        public event EventHandler<bool> PlayPause;
        IExoPlayer mediaPlayer;
        JWPlayerView playerView;
        private long limit;
        public NXPlayButton(Context context) : base(context)
        {

        }

        public NXPlayButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public NXPlayButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public NXPlayButton(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }


        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            base.OnLayout(changed, left, top, right, bottom);
            widthControl = right - left;
            heightControl = bottom - top;
            Init();
        }

        protected NXPlayButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }

        class MediaListener : Java.Lang.Object, IExoPlayerListener
        {
            public void OnPlayerError(ExoPlaybackException p0)
            {

            }

            public void OnPlayerStateChanged(bool p0, int p1)
            {

            }

            public void OnPlayWhenReadyCommitted()
            {

            }
        }
        
        private void Init()
        {
            List<PlaylistItem> list = new List<PlaylistItem>();
            list.Add(new PlaylistItem(Url));
            //playerView = new JWPlayerView(Context, 
             //   new PlayerConfig.Builder()
             //   .Playlist(list)
             //   .Build());
            //playerView.AddOnTimeListener(this);
          //  playerView.AddOnPlayListener(this);
          //  playerView.AddOnPauseListener(this);
            if (ChildCount == 1)
                return;
            imIcon = new ImageView(Context);
            int pxfromdp = (int)Util.Util.PxFromDp(Context, 8);
            FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(-2, -2);
            param.BottomMargin = pxfromdp / 2;
            param.TopMargin = pxfromdp / 2;
            param.LeftMargin = pxfromdp / 2;
            param.RightMargin = pxfromdp / 2;
            param.Gravity = GravityFlags.Center;
            imIcon.LayoutParameters = param;

            imIcon.SetPadding(pxfromdp, pxfromdp, pxfromdp, pxfromdp);
            imIcon.SetImageResource(Resource.Drawable.ic_play_arrow);
            imIcon.SetBackgroundResource(Resource.Drawable.corner_button);
            if (Android.OS.Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
            {
                imIcon.Elevation = Util.Util.PxFromDp(Context, 2);
            }
            AddView(imIcon);
            imIcon.Click += (s, e) =>
            {
                //limit = playerView.Duration;
                //if (isPlay)
                //{
                // //   playerView.Pause();
                //}
                //else
                //{
                //    if (playerView.State == PlayerState.Paused)
                //    {
                //      //  playerView.Play();
                //    }
                //    else
                //    {
                //        playerView.Load(new PlaylistItem(Url));
                //     //   playerView.Play();
                //    }
                //}
                OnClick();
            };
        }


        public void OnClick()
        {
            RotateAnimation rotate = new RotateAnimation(0, !isPlay ? 180 : -180, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
            rotate.Duration = 250;
            rotate.FillAfter = true;
            StartAnimation(rotate);
            rotate.SetAnimationListener(new AnimationListener
            {
                AnimationStart = (start) =>
                {
                    if (!isPlay)
                    {
                        imIcon.SetImageResource(Resource.Drawable.ic_pause);
                    }
                    else
                    {
                        imIcon.SetImageResource(Resource.Drawable.ic_play_arrow);
                    }
                },

                AnimationEnd = (a) =>
                {
                    imIcon.SetImageResource(!isPlay ? Resource.Drawable.ic_play_arrow : Resource.Drawable.ic_pause);
                    ScaleAnimation scale = new ScaleAnimation(1f, 1.1f, 1f, 1.1f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
                    scale.Duration = 250;
                    StartAnimation(scale);
                }
            });

        }

        public void OnTime(long p0, long p1)
        {
            PositionChanged?.Invoke(this, p0);
           // if (p0 > limit)
            //    playerView.Pause();
        }
        public void Play(int from, int to)
        {
            //if (playerView.State == PlayerState.Paused)
            //{
            //  //  playerView.Seek((long)from * 1000);
            //  //  playerView.Play();
            //}
            //else
            //{
            //  //  playerView.Load(new PlaylistItem(Url));
            //  //  playerView.Seek((long)from * 1000);
            //  //  playerView.Play();
            //}
            //limit = to * 1000;
        }
        

        public void OnPause(PlayerState p0)
        {
            isPlay = false;
            OnClick();
        }

        public void OnPlay(PlayerState p0)
        {
            isPlay = true;
            OnClick();
        }

        public void OnError(ErrorEvent p0)
        {
            isPlay = false;
            OnClick();
        }

        public void OnError(string p0)
        {
            isPlay = false;
            OnClick();
        }

        public void OnComplete()
        {
            isPlay = false;
            OnClick();
        }
    }
}