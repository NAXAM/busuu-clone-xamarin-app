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
using Naxam.Busuu.Droid.Core.Controls;
using Com.Longtailvideo.Jwplayer;
using Com.Longtailvideo.Jwplayer.Configuration;
using Com.Longtailvideo.Jwplayer.Core;
using Com.Longtailvideo.Jwplayer.Events.Listeners;
namespace Naxam.Busuu.Droid.Core.Controls
{
    public class PlayerSocial : RelativeLayout, IVideoPlayerEventsOnTimeListener, IVideoPlayerEventsOnCompleteListener
    {
        public string AudioPath { set; get; }
        NXPlayButton btnPlay;
        SeekBar sbrProgress;
        TextView txtTime;
        JWPlayerView player;
        bool completed;
        bool draw;
        public PlayerSocial(Context context) : base(context)
        {
            Init(context);
        }

        public PlayerSocial(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context);
        }

        public PlayerSocial(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context);
        }

        public PlayerSocial(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context);
        }

        protected PlayerSocial(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Init(Context);
        }

        private void Init(Context context)
        {
            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.player_social_layout, null);
            player = new JWPlayerView(context, new PlayerConfig.Builder().Build());
            btnPlay = view.FindViewById<NXPlayButton>(Resource.Id.btnPlay);
            sbrProgress = view.FindViewById<SeekBar>(Resource.Id.sbrProgress);
            sbrProgress.ProgressChanged += SbrProgress_ProgressChanged;
            txtTime = view.FindViewById<TextView>(Resource.Id.txtTime);
            AddView(view, new ViewGroup.LayoutParams(-1, -1));
            btnPlay.PlayPause += BtnPlay_PlayPause;
            player.AddOnTimeListener(this);
            player.AddOnCompleteListener(this);
        }

        private void SbrProgress_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            if (Math.Abs(sbrProgress.Progress * 1000 - player.Position) > 5000 && !completed)
            {
                long seek = sbrProgress.Progress * 1000;
                player.Seek(seek);
            }
        }

        private void BtnPlay_PlayPause(object sender, bool e)
        {
            if (e)
            {
                completed = false;
                if (player.State == PlayerState.Idle)
                {
                    player.Load(new Com.Longtailvideo.Jwplayer.Media.Playlists.PlaylistItem(AudioPath));
                }
                player.Play();
            }
            else
            {
                player?.Pause();
            }
        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            base.OnLayout(changed, left, top, right, bottom);

            //btnPlay.Measure(MeasureSpec.MakeMeasureSpec(height, MeasureSpecMode.Exactly), MeasureSpec.MakeMeasureSpec(height, MeasureSpecMode.Exactly));
            //btnPlay.Layout(0, 0, btnPlay.MeasuredWidth, btnPlay.MeasuredHeight);
           // if (draw)
               // return;
            draw = true;
            int height = bottom - top;
            var param = new RelativeLayout.LayoutParams(height, height);
            
            param.AddRule(LayoutRules.CenterVertical);
            btnPlay.LayoutParameters = param;
            System.Diagnostics.Debug.WriteLine("--> height=" + btnPlay.MeasuredHeight+"--"+btnPlay.Height+"---"+btnPlay.LayoutParameters.Height);
        }

        public void OnTime(long p0, long p1)
        {
            var time = TimeSpan.FromMilliseconds(p0);
            DateTime date = new DateTime(2000, 1, 1, time.Hours, time.Minutes, time.Seconds);
            sbrProgress.Max = (int)p1 / 1000;
            sbrProgress.Progress = (int)p0 / 1000;
            txtTime.Text = date.ToString("mm:ss");
        }

        public void OnComplete()
        {
            completed = true;
            btnPlay.OnClick(false);
            sbrProgress.Progress = 0;
            sbrProgress.Max = 0;
            txtTime.Text = "00:00";
        }
    }
}