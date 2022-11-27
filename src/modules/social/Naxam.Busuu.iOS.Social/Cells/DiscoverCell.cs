using Foundation;
using System;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.iOS.Core.Converter;
using AVFoundation;

namespace Naxam.Busuu.iOS.Social.Cells
{
    public partial class DiscoverCell : MvxCollectionViewCell
    {
        public event EventHandler<SocialModel> ViewDiscoverHandler;

        readonly MvxImageViewLoader _loaderImageUser;
        readonly MvxImageViewLoader _loaderImgSpeak;
        readonly MvxImageViewLoader _loaderImgLearn;

        AVAudioPlayer SpeakMusicPlayer;
        NSTimer update_timer;
        string textNameAndCountry;

        UIImage playBtnBg, pauseBtnBg;

        public DiscoverCell(IntPtr handle) : base(handle)
        {
            _loaderImageUser = new MvxImageViewLoader(() => ImageUser);
			_loaderImageUser.DefaultImagePath = "res:user_avatar_placeholder.png";

            _loaderImgSpeak = new MvxImageViewLoader(() => ImgSpeak);
            _loaderImgLearn = new MvxImageViewLoader(() => ImageLan);

			playBtnBg = UIImage.FromFile("play_btn.png");
			pauseBtnBg = UIImage.FromFile("pause_btn.png");

            this.DelayBind(() =>
            {
                var setBinding = this.CreateBindingSet<DiscoverCell, SocialModel>();
                setBinding.Bind(_loaderImageUser).To(d => d.User.Photo);
                setBinding.Bind(NameUser).To(d => d.User.Name);
                setBinding.Bind(Country).To(d => d.User.Country.Country);
                setBinding.Bind(_loaderImgSpeak).To(d => d.ImageSpeakLanguage).WithConversion(nameof(ImageUriConverter));
                setBinding.Bind(ViewSpeak).For(d => d.Hidden).To(d => d.Type).WithConversion(nameof(SocialTypeToBoolFalseConverter));
                setBinding.Bind(audioViewBottomConstraint).For(x => x.Active).To(d => d.Type).WithConversion(nameof(SocialTypeToBoolConverter));
                setBinding.Bind(audioViewTopConstraint).For(x => x.Active).To(d => d.Type).WithConversion(nameof(SocialTypeToBoolConverter));
                setBinding.Bind(WriteLabel).For(d => d.Hidden).To(d => d.Type).WithConversion(nameof(SocialTypeToBoolConverter));
                setBinding.Bind(WriteLabel).To(d => d.Content);
                setBinding.Bind(_loaderImgLearn).To(d => d.ImageLearn).WithConversion(nameof(ImageUriConverter));
                setBinding.Bind(TextLan).To(d => d.TextLearn);
                setBinding.Apply();
            });
		}

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            var bbcolor = UIColor.FromRGB(224, 230, 235);
            ViewLan.Layer.BorderColor = bbcolor.CGColor;
            ViewHome.Layer.BorderColor = bbcolor.CGColor;

            ButtonPlay.ImageEdgeInsets = new UIEdgeInsets(9, 11, 9, 9);

            var img = UIImage.FromBundle("play_icon_small");
            SliderSpeak.SetThumbImage(img, UIControlState.Normal);
            SliderSpeak.SetThumbImage(img, UIControlState.Selected);
            SliderSpeak.SetThumbImage(img, UIControlState.Highlighted);

			var fileUrl = NSBundle.MainBundle.PathForResource("Nokia-tune-Nokia-tune", "mp3");
			Uri songURL = new NSUrl(fileUrl);
			SpeakMusicPlayer = AVAudioPlayer.FromUrl(songURL);
			SpeakMusicPlayer.Volume = 1;
			SpeakMusicPlayer.NumberOfLoops = 0;
			SpeakMusicPlayer.FinishedPlaying -= SpeakMusicPlayer_FinishedPlaying;
			SpeakMusicPlayer.FinishedPlaying += SpeakMusicPlayer_FinishedPlaying;

            textNameAndCountry = NameUser.Text + Country.Text;
            UpdateViewForPlayerInfo();
            UpdateViewForPlayerState();
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if ((NameUser.Text + Country.Text) != textNameAndCountry)
            {
                textNameAndCountry = NameUser.Text + Country.Text;
                
				if (SpeakMusicPlayer != null)
                    SpeakMusicPlayer.Stop();

				UpdateViewForPlayerInfo();
                UpdateViewForPlayerState();
            }
		}

        void SpeakMusicPlayer_FinishedPlaying(object sender, AVStatusEventArgs e)
        {
            UpdateViewForPlayerInfo();
            UpdateViewForPlayerState();
        }

        partial void btnView_TouchUpInside(NSObject sender)
        {
            ViewDiscoverHandler?.Invoke(this, (SocialModel)DataContext);
        }

        partial void ButtonPlay_TouchUpInside(NSObject sender)
        {
            if (SpeakMusicPlayer.Playing)
            {
                PausePlayback();
            }
            else
            {
                StartPlayback();
            }
        }

        void UpdateCurrentTime()
        {
            if (SpeakMusicPlayer.Playing)
            {
                ButtonPlay.ImageEdgeInsets = new UIEdgeInsets(9, 9, 9, 9);
                ButtonPlay.SetImage(pauseBtnBg, UIControlState.Normal);
                var min = (int)((SpeakMusicPlayer.Duration - SpeakMusicPlayer.CurrentTime) / 60);
                var sec = (int)((SpeakMusicPlayer.Duration - SpeakMusicPlayer.CurrentTime) % 60);
                lblTime.Text = String.Format("{0:D2}:{1:D2}", min , sec);
                SliderSpeak.Value = (float)SpeakMusicPlayer.CurrentTime;
            }
            else
            {
                ButtonPlay.ImageEdgeInsets = new UIEdgeInsets(9, 11, 9, 9);
                ButtonPlay.SetImage(playBtnBg, UIControlState.Normal);
            }
        }

        void UpdateViewForPlayerState()
        {
            if (SpeakMusicPlayer.Playing)
            {
				ButtonPlay.ImageEdgeInsets = new UIEdgeInsets(9, 9, 9, 9);
				ButtonPlay.SetImage(pauseBtnBg, UIControlState.Normal);
				
                if (update_timer == null)
				{
					InvokeOnMainThread(() =>
					{
						update_timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(0.01), delegate
						{
							UpdateCurrentTime();
						});
					});
				}
            }
            else
            {
                ButtonPlay.ImageEdgeInsets = new UIEdgeInsets(9, 11, 9, 9);
                ButtonPlay.SetImage(playBtnBg, UIControlState.Normal);

                if (update_timer != null)
                {
                    update_timer.Invalidate();
                    update_timer = null;
                }
            }
        }

        void UpdateViewForPlayerInfo()
        {
            SliderSpeak.Value = 0;
            SliderSpeak.MaxValue = (float)SpeakMusicPlayer.Duration;
            lblTime.Text = String.Format("{0:00}:{1:00}", (int)SpeakMusicPlayer.Duration / 60, (int)SpeakMusicPlayer.Duration % 60);
        }

        void PausePlayback()
        {
            SpeakMusicPlayer.Pause();
            UpdateViewForPlayerState();
        }

        void StartPlayback()
        {
            SpeakMusicPlayer.Play();
            UpdateViewForPlayerState();
        }
    }
}