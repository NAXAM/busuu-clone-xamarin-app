using Foundation;
using System;
using UIKit;
using ObjCRuntime;
using Naxam.Busuu.Learning.Models;
using FFImageLoading;
using FFImageLoading.Work;
using CoreGraphics;
using AVFoundation;

namespace Naxam.Busuu.iOS.Learning.Views
{
    public partial class SelectWordView : MemoriseBaseView
    {
		bool IsAnimationBtn;
		AVAudioPlayer SpeakMusicPlayer;
		public event EventHandler<AnswerModel> AnswerClick;

		public SelectWordView(IntPtr handle) : base(handle)
        {
		}

		public static SelectWordView Create(UnitModel item)
		{
			var arr = NSBundle.MainBundle.LoadNib("SelectWord", null, null);
			var v = Runtime.GetNSObject<SelectWordView>(arr.ValueAt(0));
			v.Item = item;
			v.InitData();
			return v;
		}

		void InitData()
		{
			if (Item != null)
			{
				LbQuestion.Text = Item.Title;
			}

			if ((Item.Images != null) && (Item.Image != null))
			{
				ImgImageHeightConstraint.Constant = ImgImage.Layer.Bounds.Width / 2.25f;
				ImageService.Instance.LoadUrl(Item.Images[0]).
					  ErrorPlaceholder("anime-wallpaper-art-pc (27).jpg", ImageSource.ApplicationBundle).
					  LoadingPlaceholder("anime-wallpaper-art-pc (27).jpg", ImageSource.ApplicationBundle).
					  Into(ImgImage);
			}
			else
			{
				btnPlayPause.Hidden = true;
				ImgImage.Hidden = true;
				ImgImageHeightConstraint.Constant = 0;
				LbQuestionTopConstraint.Constant = 12;
			}

			nfloat y = 0;
			for (int i = 0; i < Item.Answers.Count; i++)
			{
				var button = new UIButton(new CGRect(0, y, ViewAnswers.Layer.Bounds.Width, 40));
				y += 50;
				button.SetTitle(Item.Answers[i].Text, UIControlState.Normal);
				button.TitleLabel.Font = UIFont.SystemFontOfSize(14f);
				button.Layer.ShadowRadius = 2;
				button.Layer.ShadowOffset = new CGSize(0, 2);
				button.Layer.ShadowOpacity = 0.25f;
				button.BackgroundColor = UIColor.White;
				button.SetTitleColor(UIColor.DarkTextColor, UIControlState.Normal);
				button.Tag = i + 100;
				button.TouchUpInside += (sender, e) =>
				{
					var btn = sender as UIButton;
					AnswerClick?.Invoke(sender, Item.Answers[(int)btn.Tag - 100]);
				};
				ViewAnswers.AddSubview(button);
			}

			AnswerClick += (sender, e) =>
			{
				var button = sender as UIButton;
				if (e.Value)
				{
					button.Layer.BackgroundColor = UIColor.FromRGB(103, 176, 0).CGColor;
					button.SetTitleColor(UIColor.White, UIControlState.Normal);
					LearnView.myPoint += 1;
				}
				else
				{
					button.Layer.BackgroundColor = UIColor.Red.CGColor;
					button.SetTitleColor(UIColor.White, UIControlState.Normal);
					for (int i = 0; i < Item.Answers.Count; i++)
					{
						if (Item.Answers[i].Value)
						{
							ContentView.ViewWithTag(i + 100).Layer.BackgroundColor = UIColor.FromRGB(103, 176, 0).CGColor;
							(ContentView.ViewWithTag(i + 100) as UIButton).SetTitleColor(UIColor.White, UIControlState.Normal);
							break;
						}
					}

					UIView.Animate(0.05, 0, UIViewAnimationOptions.CurveEaseIn | UIViewAnimationOptions.Repeat, () =>
					{
						UIView.SetAnimationRepeatCount(10);
						button.Transform = CGAffineTransform.MakeTranslation(5f, 0);
					}, () =>
					{
						button.Transform = CGAffineTransform.MakeTranslation(0, 0);
					});
					//button.Transform = CGAffineTransform.MakeIdentity();
				}
				ContentView.UserInteractionEnabled = false;
				Answered.DidAnswer(this);
			};
		}


		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

			btnPlayPause.Layer.CornerRadius = btnPlayPause.Bounds.Height / 2;
			btnPlayPause.ImageEdgeInsets = new UIEdgeInsets(14, 16, 14, 14);
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