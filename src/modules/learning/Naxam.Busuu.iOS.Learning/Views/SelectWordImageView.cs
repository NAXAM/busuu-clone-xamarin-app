using Foundation;
using System;
using UIKit;
using Naxam.Busuu.Learning.Models;
using ObjCRuntime;
using CoreGraphics;
using FFImageLoading;
using FFImageLoading.Work;
using AVFoundation;

namespace Naxam.Busuu.iOS.Learning.Views
{
    public partial class SelectWordImageView : MemoriseBaseView
    {
        bool IsAnimationBtn;
        AVAudioPlayer SpeakMusicPlayer;

		public event EventHandler<AnswerModel> AnswerClick;

		public SelectWordImageView(IntPtr handle) : base(handle)
        {
		}

		public static SelectWordImageView Create(UnitModel item)
		{
			var arr = NSBundle.MainBundle.LoadNib("SelectWordImage", null, null);
			var v = Runtime.GetNSObject<SelectWordImageView>(arr.ValueAt(0));
			v.Item = item;
			v.InitData();
			return v;
		}

        void InitData()
        {
            if (Item != null)
            {
                lblQuestion.Text = Item.Title;

				nfloat y = 0;
                for (int i = 0; i < Item.Answers.Count; i++)
                {
                    var viewButton = new UIView(new CGRect(0, y, ViewAnswers.Layer.Bounds.Width, 80));
                    viewButton.Layer.ShadowRadius = 2;
                    viewButton.Layer.ShadowOffset = new CGSize(0, 2);
                    viewButton.Layer.ShadowOpacity = 0.25f;
                    viewButton.Tag = i + 60;

                    var imgButton = new UIImageView(new CGRect(0, 0, 80, 80));
                    imgButton.Image = UIImage.FromFile("anime-wallpaper-art-pc (27).jpg");
                    imgButton.ContentMode = UIViewContentMode.ScaleAspectFill;
                    imgButton.ClipsToBounds = true;

                    var viewNhi = new UIView(new CGRect(0, 0, 80, 80));
                    viewNhi.Tag = i + 50;
                    viewNhi.BackgroundColor = UIColor.Clear;
                    viewNhi.Alpha = 0.5f;

                    ImageService.Instance.LoadUrl(Item.Answers[i].Image).
                                ErrorPlaceholder("anime-wallpaper-art-pc (27).jpg", ImageSource.CompiledResource).
                      LoadingPlaceholder("anime-wallpaper-art-pc (27).jpg", ImageSource.CompiledResource).
                      Into(imgButton);

                    var button = new UIButton(new CGRect(80, 0, ViewAnswers.Layer.Bounds.Width - 80, 80));
                    button.SetTitle(Item.Answers[i].Text, UIControlState.Normal);
                    button.TitleLabel.Font = UIFont.SystemFontOfSize(14f);
                    button.BackgroundColor = UIColor.White;
                    button.SetTitleColor(UIColor.DarkTextColor, UIControlState.Normal);
                    button.Tag = i + 70;
                    //button.TouchUpInside += (sender, e) => {
                    //  var btn = sender as UIButton;
                    //  AnswerClick?.Invoke(sender, Item.Answers[(int)btn.Tag - 100]);
                    //};
                        
                    var button2 = new UIButton(new CGRect(0, 0, ViewAnswers.Layer.Bounds.Width, 80));
                    button2.BackgroundColor = UIColor.Clear;
                    button2.SetTitle("", UIControlState.Normal);
                    button2.Tag = i + 100;
					button2.TouchUpInside += (sender, e) => {
						var btn = sender as UIButton;
						AnswerClick?.Invoke(sender, Item.Answers[(int)btn.Tag - 100]);
					};
                   					
                    viewButton.AddSubview(imgButton);
                    viewButton.AddSubview(button);
                    viewButton.AddSubview(viewNhi);                
                    viewButton.AddSubview(button2);

                    y += 90;
					ViewAnswers.AddSubview(viewButton);
				}

                AnswerClick += (sender, e) =>
                {
                    nint timtag = 0;
                    var button = sender as UIButton;
                    if (e.Value)
                    {
                        (ViewAnswers.ViewWithTag(button.Tag - 100 + 70) as UIButton).Layer.BackgroundColor = UIColor.FromRGB(103, 176, 0).CGColor;
                        (ViewAnswers.ViewWithTag(button.Tag - 100 + 70) as UIButton).SetTitleColor(UIColor.White, UIControlState.Normal);
                        (ViewAnswers.ViewWithTag(button.Tag - 100 + 50) as UIView).BackgroundColor = UIColor.FromRGB(103, 176, 0);
						LearnView.myPoint += 1;
					}
                    else
                    {
                        (ViewAnswers.ViewWithTag(button.Tag - 100 + 70) as UIButton).Layer.BackgroundColor = UIColor.Red.CGColor;
                        (ViewAnswers.ViewWithTag(button.Tag - 100 + 70) as UIButton).SetTitleColor(UIColor.White, UIControlState.Normal);
                        (ViewAnswers.ViewWithTag(button.Tag - 100 + 50) as UIView).BackgroundColor = UIColor.Red;
                        timtag = button.Tag;

                        for (int i = 0; i < Item.Answers.Count; i++)
                        {
                            if (Item.Answers[i].Value)
                            {
                                ViewAnswers.ViewWithTag(i + 70).Layer.BackgroundColor = UIColor.FromRGB(103, 176, 0).CGColor;
								(ViewAnswers.ViewWithTag(i + 70) as UIButton).SetTitleColor(UIColor.White, UIControlState.Normal);
								(ViewAnswers.ViewWithTag(i + 50) as UIView).BackgroundColor = UIColor.FromRGB(103, 176, 0);
								break;
                            }
                        }

                        UIView.Animate(0.05, 0, UIViewAnimationOptions.CurveEaseIn | UIViewAnimationOptions.Repeat, () =>
                        {
                            UIView.SetAnimationRepeatCount(10);
                            (ViewAnswers.ViewWithTag(timtag - 100 + 60) as UIView).Transform = CGAffineTransform.MakeTranslation(5f, 0);
                        }, () => {
                            (ViewAnswers.ViewWithTag(timtag - 100 + 60) as UIView).Transform = CGAffineTransform.MakeTranslation(0, 0);
                        });
                    }
                    ViewAnswers.UserInteractionEnabled = false;
                    Answered.DidAnswer(this);
                };
            }
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            btnAudio.ImageEdgeInsets = new UIEdgeInsets(18, 22, 18, 18);

            btnAudio.Layer.CornerRadius = btnAudio.Bounds.Height / 2;
        }

        partial void btnAudio_Click(NSObject sender)
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
                btnAudio.Transform = CGAffineTransform.MakeRotation((float)Math.PI);
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
                btnAudio.Transform = CGAffineTransform.MakeRotation(0);
				UIView.CommitAnimations();
			}
		}

		[Export("animationBtnSay:finished:context:")]
		void btnSayStopped(NSString animationID, NSNumber finished, NSObject context)
		{
			if (IsAnimationBtn)
			{
				btnAudio.Transform = CGAffineTransform.MakeRotation((float)Math.PI);

				var playBtnSay = UIImage.FromFile("pause_btn.png");
				btnAudio.SetImage(playBtnSay, UIControlState.Normal);
				btnAudio.SetImage(playBtnSay, UIControlState.Selected);
				btnAudio.SetImage(playBtnSay, UIControlState.Highlighted);

				btnAudio.ImageEdgeInsets = new UIEdgeInsets(18, 18, 18, 18);
			}
			else
			{
				btnAudio.Transform = CGAffineTransform.MakeRotation(0);

				var playBtnSay = UIImage.FromFile("play_btn.png");
				btnAudio.SetImage(playBtnSay, UIControlState.Normal);
				btnAudio.SetImage(playBtnSay, UIControlState.Selected);
				btnAudio.SetImage(playBtnSay, UIControlState.Highlighted);

				btnAudio.ImageEdgeInsets = new UIEdgeInsets(18, 22, 18, 18);
			}
		}

	}
}