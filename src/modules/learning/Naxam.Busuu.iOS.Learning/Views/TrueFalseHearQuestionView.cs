using Foundation;
using System;
using UIKit;
using ObjCRuntime;
using Naxam.Busuu.Learning.Models;
using CoreGraphics;
using AVFoundation;
using FFImageLoading;
using FFImageLoading.Work;

namespace Naxam.Busuu.iOS.Learning.Views
{
    public partial class TrueFalseHearQuestionView : MemoriseBaseView
    {
        bool IsAnimationBtn;
        AVAudioPlayer SpeakMusicPlayer;
        bool btnEnabled = true;

        public TrueFalseHearQuestionView (IntPtr handle) : base (handle)
        {
        }

		public static TrueFalseHearQuestionView Create(UnitModel item)
		{
			var arr = NSBundle.MainBundle.LoadNib("TrueFalseHearQuestion", null, null);
			var v = Runtime.GetNSObject<TrueFalseHearQuestionView>(arr.ValueAt(0));
			v.Item = item;
            v.InitData();
			return v;
		}

        void InitData()
        {
			ImageService.Instance.LoadUrl(Item.Images[0]).
					ErrorPlaceholder("anime-wallpaper-art-pc (27).jpg", ImageSource.ApplicationBundle).
					LoadingPlaceholder("anime-wallpaper-art-pc (27).jpg", ImageSource.ApplicationBundle).
					Into(imgEx);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            btnPlayPause.Layer.CornerRadius = btnPlayPause.Bounds.Height / 2;
            btnPlayPause.ImageEdgeInsets = new UIEdgeInsets(14, 16, 14, 14);

            btnTrue.Layer.CornerRadius = btnTrue.Bounds.Height / 2;
            btnTrue.Layer.ShadowRadius = 2;
            btnTrue.Layer.ShadowOpacity = 0.25f;
            btnTrue.Layer.ShadowOffset = new CGSize(0, 2);

            btnFalse.Layer.CornerRadius = btnFalse.Bounds.Height / 2;
            btnFalse.Layer.ShadowRadius = 2;
            btnFalse.Layer.ShadowOpacity = 0.25f;
            btnFalse.Layer.ShadowOffset = new CGSize(0, 2);
        }

        partial void btnPlayPause_TouchUpInside(NSObject sender)
        {
            if (!IsAnimationBtn)
            {
                StartAnimationBtnSay();

				var fileUrl = NSBundle.MainBundle.PathForResource("wrong", "mp3");
				if (fileUrl != null)
				{
					Uri songURL = new NSUrl(fileUrl);
					SpeakMusicPlayer = AVAudioPlayer.FromUrl(songURL);
					SpeakMusicPlayer.Volume = 1;
					SpeakMusicPlayer.NumberOfLoops = 0;
					SpeakMusicPlayer.FinishedPlaying -= SpeakMusicPlayer_FinishedPlaying;
					SpeakMusicPlayer.FinishedPlaying += SpeakMusicPlayer_FinishedPlaying;
                    SpeakMusicPlayer.Play();
				}
            }
        }

		void SpeakMusicPlayer_FinishedPlaying(object sender, AVStatusEventArgs e)
		{
            StopAnimationBtnSay();
		}

        partial void btnTrue_TouchUpInside(NSObject sender)
        {
            if (btnEnabled)
            {
                btnEnabled = false;

				UIImage vicon = UIImage.FromFile("v.png");
				btnTrue.TintColor = UIColor.White;
				btnTrue.BackgroundColor = UIColor.FromRGB(116, 184, 39);
				btnTrue.SetTitle("", UIControlState.Normal);
				btnTrue.SetImage(vicon, UIControlState.Normal);
				btnTrue.ImageEdgeInsets = new UIEdgeInsets(24, 20, 16, 20);

				Answered.DidAnswer(this);

				btnTrueBottomConstraint.Constant = 88;
                LearnView.myPoint += 1;
                UserInteractionEnabled = false;
            }          
		}

        partial void btnFalse_TouchUpInside(NSObject sender)
        {
            if (btnEnabled)
            {
                btnEnabled = false;

                UIImage xicon = UIImage.FromFile("x.png");
                btnFalse.TintColor = UIColor.White;
                btnFalse.BackgroundColor = UIColor.FromRGB(234, 67, 50);
                btnFalse.SetTitle("", UIControlState.Normal);
                btnFalse.SetImage(xicon, UIControlState.Normal);
                btnFalse.SetImage(xicon, UIControlState.Disabled);
                btnFalse.ImageEdgeInsets = new UIEdgeInsets(24, 24, 24, 24);

                Answered.DidAnswer(this);

                btnTrueBottomConstraint.Constant = 88;

                UIView.Animate(0.05, 0, UIViewAnimationOptions.CurveEaseIn | UIViewAnimationOptions.Repeat, () =>
                        {
                            UIView.SetAnimationRepeatCount(10);
                            btnFalse.Transform = CGAffineTransform.MakeTranslation(5f, 0);
                }, () => {
                    btnFalse.Transform = CGAffineTransform.MakeTranslation(0, 0);
                });
                btnFalse.Transform = CGAffineTransform.MakeIdentity();
				UserInteractionEnabled = false;
            }
        }

		void StartAnimationBtnSay()
		{
			if (!IsAnimationBtn)
			{
				IsAnimationBtn = true;

				UIView.BeginAnimations("btnSayAnimation");
				UIView.SetAnimationDuration(0.25);
				UIView.SetAnimationCurve(UIViewAnimationCurve.EaseOut);
				UIView.SetAnimationDelegate(this);
				UIView.SetAnimationDidStopSelector(new Selector("animationBtnSay:finished:context:"));
				btnPlayPause.Transform = CGAffineTransform.MakeRotation((float)Math.PI);
				btnPlayPause.Alpha = 1;
				UIView.CommitAnimations();
			}
		}

		void StopAnimationBtnSay()
		{
			if (IsAnimationBtn)
			{
				IsAnimationBtn = false;

				UIView.BeginAnimations("btnSayAnimation");
				UIView.SetAnimationDuration(0.25);
				UIView.SetAnimationCurve(UIViewAnimationCurve.EaseOut);
				UIView.SetAnimationDelegate(this);
				UIView.SetAnimationDidStopSelector(new Selector("animationBtnSay:finished:context:"));
				btnPlayPause.Transform = CGAffineTransform.MakeRotation(0);
				UIView.CommitAnimations();
			}
		}

		[Export("animationBtnSay:finished:context:")]
		void btnSayStopped(NSString animationID, NSNumber finished, NSObject context)
		{
			if (IsAnimationBtn)
			{
                btnPlayPause.Transform = CGAffineTransform.MakeRotation((float)Math.PI);

				var playBtnSay = UIImage.FromFile("pause_btn.png");
                btnPlayPause.SetImage(playBtnSay, UIControlState.Normal);
				btnPlayPause.SetImage(playBtnSay, UIControlState.Selected);
				btnPlayPause.SetImage(playBtnSay, UIControlState.Highlighted);

				btnPlayPause.ImageEdgeInsets = new UIEdgeInsets(14, 14, 14, 14);
			}
			else
			{
				btnPlayPause.Transform = CGAffineTransform.MakeRotation(0);

				var playBtnSay = UIImage.FromFile("play_btn.png");
				btnPlayPause.SetImage(playBtnSay, UIControlState.Normal);
				btnPlayPause.SetImage(playBtnSay, UIControlState.Selected);
				btnPlayPause.SetImage(playBtnSay, UIControlState.Highlighted);

				btnPlayPause.ImageEdgeInsets = new UIEdgeInsets(14, 16, 14, 14);
			}
		}
    }
}