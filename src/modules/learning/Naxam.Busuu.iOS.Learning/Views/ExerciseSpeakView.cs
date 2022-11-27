using Foundation;
using System;
using UIKit;
using Naxam.Busuu.Learning.Models;
using ObjCRuntime;
using AVFoundation;
using CoreGraphics;
using CoreAnimation;
using FFImageLoading;
using FFImageLoading.Work;

namespace Naxam.Busuu.iOS.Learning.Views
{
    public partial class ExerciseSpeakView : MemoriseBaseView
    {
		bool IsAnimationBtn, IsSay;
		AVAudioPlayer SpeakMusicPlayer;
        NSObject abcdef;
        bool btnEnabled = true;
        bool TryAgain;

        public ExerciseSpeakView (IntPtr handle) : base (handle)
        {
        }

		public static ExerciseSpeakView Create(UnitModel item)
		{
			var arr = NSBundle.MainBundle.LoadNib("ExerciseSpeak", null, null);
			var v = Runtime.GetNSObject<ExerciseSpeakView>(arr.ValueAt(0));
			v.Item = item;
            v.InitData();
			return v;
		}

		void InitData()
		{
			ImageService.Instance.LoadUrl(Item.Images[0]).
			   ErrorPlaceholder("anime-wallpaper-art-pc (27).jpg", ImageSource.ApplicationBundle).
			   LoadingPlaceholder("anime-wallpaper-art-pc (27).jpg", ImageSource.ApplicationBundle).
			   Into(imgImage);
		}

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

			btnPlayPause.Layer.CornerRadius = btnPlayPause.Bounds.Height / 2;
			btnPlayPause.ImageEdgeInsets = new UIEdgeInsets(14, 16, 14, 14);

            btnSay.Layer.CornerRadius = btnSay.Bounds.Height / 2;
            viewForBtnSay.Layer.CornerRadius = viewForBtnSay.Bounds.Height / 2;
            btnSay.ImageEdgeInsets = new UIEdgeInsets(20, 24.5f, 20, 24.5f);
		}

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            viewForBtnSay.Layer.CornerRadius = viewForBtnSay.Bounds.Height / 2;
        }

        partial void btnNotSpeak_Click(NSObject sender)
        {
            IsSay = false;
            LearnView.myPoint += 1;
			Answered.NextAnswer(this);
        }

		partial void btnPlayPause_TouchUpInside(NSObject sender)
		{
			if (!IsAnimationBtn && !IsSay)
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

		void StartAnimationBtnSay()
		{
            if (!IsAnimationBtn && !IsSay)
			{
				IsAnimationBtn = true;
                btnSay.BackgroundColor = UIColor.FromRGB(167, 176, 182);

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
            if (IsAnimationBtn && !IsSay)
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
				btnSay.BackgroundColor = UIColor.FromRGB(57, 169, 246);

				var playBtnSay = UIImage.FromFile("play_btn.png");
				btnPlayPause.SetImage(playBtnSay, UIControlState.Normal);
				btnPlayPause.SetImage(playBtnSay, UIControlState.Selected);
				btnPlayPause.SetImage(playBtnSay, UIControlState.Highlighted);

				btnPlayPause.ImageEdgeInsets = new UIEdgeInsets(14, 16, 14, 14);
			}
		}

        partial void btnSay_TouchUpInside(NSObject sender)
        {
            if (!IsSay && btnEnabled)
            {
                abcdef = sender;
                IsSay = true;
                btnSay.BackgroundColor = UIColor.FromRGB(234, 67, 50);
                lblListen.Layer.Hidden = true;

				var scaleAnimation = CABasicAnimation.FromKeyPath("transform");
				scaleAnimation.Duration = 0.65;
				scaleAnimation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut);
				scaleAnimation.RepeatCount = 5;
				scaleAnimation.AutoReverses = true;
				scaleAnimation.From = NSValue.FromCATransform3D(CATransform3D.MakeScale(1, 1, 1));
				scaleAnimation.To = NSValue.FromCATransform3D(CATransform3D.MakeScale(1.4f, 1.4f, 1.4f));
				scaleAnimation.RemovedOnCompletion = true;
				scaleAnimation.AnimationStopped -= HandleEventHandler;
				scaleAnimation.AnimationStopped += HandleEventHandler;

                viewForBtnSay.Layer.AddAnimation(scaleAnimation, "scale_animation");
               
            }
            else if (IsSay && btnEnabled)
            {
                IsSay = false;

				viewForBtnSay.Layer.RemoveAnimation("scale_animation");
                lblListen.Hidden = false;

				UIImage sicon = UIImage.FromFile("conversation_speaking_button_white.png");
				btnSay.TintColor = UIColor.White;
				btnSay.BackgroundColor = UIColor.FromRGB(57, 169, 246);
				btnSay.SetTitle("", UIControlState.Normal);
				btnSay.SetImage(sicon, UIControlState.Normal);
				btnSay.ImageEdgeInsets = new UIEdgeInsets(20, 24.5f, 20, 24.5f);
            }
        }

		void HandleEventHandler(object sender, CAAnimationStateEventArgs e)
		{
			if (IsSay)
			{
				AnimationComplete();
			}
		}

        void AnimationComplete()
        {
            Random afs = new Random();
            int i = afs.Next(0, 3);
            //int i = 1;
            btnSay.Tag = 100;

            if (i == 1)
            {
                if (!TryAgain)
                {
                    lblListen.Text = "Try again";
                    lblListen.Hidden = false;
                }

                btnSay.Tag = 101;
                viewForBtnSay.BackgroundColor = UIColor.FromRGB(242, 245, 248);

				UIImage xicon = UIImage.FromFile("x.png");
                btnSay.TintColor = UIColor.White;
                btnSay.BackgroundColor = UIColor.FromRGB(234, 67, 50);
                btnSay.SetTitle("", UIControlState.Normal);
				btnSay.SetImage(xicon, UIControlState.Normal);
                btnSay.ImageEdgeInsets = new UIEdgeInsets(24, 24, 24, 24);

                UIView.Animate(0.05, 0, UIViewAnimationOptions.CurveEaseIn | UIViewAnimationOptions.Repeat, () =>
                                   {
                                       UIView.SetAnimationRepeatCount(10);
                                     btnSay.Transform = CGAffineTransform.MakeTranslation(5f, 0);
                }, TryagainHandleAction);
            }
            else
            {
                lblListen.Text = "Will done!";
                lblListen.Hidden = false;
                iNotSpeak.Hidden = true;

				UIImage vicon = UIImage.FromFile("v.png");
                btnSay.TintColor = UIColor.White;
                btnSay.BackgroundColor = UIColor.FromRGB(116, 184, 39);
                btnSay.SetTitle("", UIControlState.Normal);
                btnSay.SetImage(vicon, UIControlState.Normal);
                btnSay.ImageEdgeInsets = new UIEdgeInsets(24, 20, 16, 20);
				viewBossBottomConstraint.Constant = -16;
				Answered.DidAnswer(this);
                LearnView.myPoint += 1;
				btnEnabled = false;
				UserInteractionEnabled = false;
			}
        }

        void TryagainHandleAction()
        {
            btnSay.Transform = CGAffineTransform.MakeTranslation(0, 0);

            if (!TryAgain)
            {
                TryAgain = true;
                btnEnabled = true;
                IsSay = false;

                viewForBtnSay.BackgroundColor = UIColor.FromRGB(57, 169, 246);
				lblListen.Text = "Listen and repeat.";
				lblListen.Hidden = false;

				viewForBtnSay.Layer.RemoveAnimation("scale_animation");
				lblListen.Hidden = false;

				UIImage sicon = UIImage.FromFile("conversation_speaking_button_white.png");
				btnSay.TintColor = UIColor.White;
				btnSay.BackgroundColor = UIColor.FromRGB(57, 169, 246);
				btnSay.SetTitle("", UIControlState.Normal);
				btnSay.SetImage(sicon, UIControlState.Normal);
				btnSay.ImageEdgeInsets = new UIEdgeInsets(20, 24.5f, 20, 24.5f);
            }
            else
            {
				Answered.NextAnswer(this);
				//viewBossBottomConstraint.Constant = 0;
				//Answered.DidAnswer(this);
				//btnEnabled = false;
            }
        }
    }
}