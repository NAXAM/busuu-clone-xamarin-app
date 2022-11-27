using Foundation;
using System;
using UIKit;
using Naxam.Busuu.Learning.Models;
using ObjCRuntime;
using FFImageLoading;
using CoreGraphics;
using FFImageLoading.Work;
using AVFoundation;

namespace Naxam.Busuu.iOS.Learning.Views
{
    public partial class CompleteSentenceView : MemoriseBaseView
    {
        UITextField textField;
        string textQ;
		bool IsAnimationBtn;
		AVAudioPlayer SpeakMusicPlayer;

		public CompleteSentenceView(IntPtr handle) : base(handle)
        {
		}

		public static CompleteSentenceView Create(UnitModel item)
		{
			var arr = NSBundle.MainBundle.LoadNib("CompleteSentence", null, null);
			var v = Runtime.GetNSObject<CompleteSentenceView>(arr.ValueAt(0));
			v.Item = item;
			v.InitData();
			return v;
		}

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            btnContinue.Layer.CornerRadius = btnContinue.Bounds.Height / 2;
            btnContinue.Layer.ShadowRadius = 2;
            btnContinue.Layer.ShadowOpacity = 0.25f;
            btnContinue.Layer.ShadowOffset = new CGSize(0, 2);

			btnPlayPause.Layer.CornerRadius = btnPlayPause.Bounds.Height / 2;
			btnPlayPause.ImageEdgeInsets = new UIEdgeInsets(14, 16, 14, 14);
        }

        void InitData()
        {
            if (Item != null)
            {
                lblTitle.Text = Item.Title;
                lblQuestion.Text = Item.Inputs[0].Replace("%%", "_____");

                ImageService.Instance.LoadUrl(Item.Images[0]).
                        ErrorPlaceholder("anime-wallpaper-art-pc (27).jpg", ImageSource.ApplicationBundle).
                        LoadingPlaceholder("anime-wallpaper-art-pc (27).jpg", ImageSource.ApplicationBundle).
                            Into(imgFillSentence);
            }

            textField = new UITextField(new CGRect(0, 16, 0, 0));
            textField.Text = "";
            textField.Alpha = 0;
            textField.EditingChanged += TextField_EditingChanged;

            lblQuestion.AddSubview(textField);
        }

		void TextField_EditingChanged(object sender, EventArgs e)
		{
            var textf = sender as UITextField;
            textQ = textf.Text;
            if (textf.Text != "")
            {
				lblQuestion.Text = Item.Inputs[0].Replace("%%", textf.Text);

				var greenAttributes = new UIStringAttributes
				{
					Font = UIFont.BoldSystemFontOfSize(15f),
					ForegroundColor = UIColor.FromRGB(57, 169, 246)
				};

				int uLan1 = lblQuestion.Text.IndexOf(textf.Text);

				var prettyString = new NSMutableAttributedString(lblQuestion.Text);

				prettyString.SetAttributes(greenAttributes.Dictionary, new NSRange(uLan1, textf.Text.Length));

				lblQuestion.AttributedText = prettyString;

                btnContinue.Hidden = false;
            }
            else
            {
                lblQuestion.Text = Item.Inputs[0].Replace("%%", "_____");
                btnContinue.Hidden = true;
            }
		}

        partial void btnQuestion_Click(NSObject sender)
        {
            textField.BecomeFirstResponder();
        }

        partial void btnContinue_Click(NSObject sender)
        {
			string aw = Item.Inputs[0].Replace("%%", "_____");
			int unitAw = aw.IndexOf("_____");

			var greenAttributes = new UIStringAttributes
			{
				Font = UIFont.BoldSystemFontOfSize(15f),
				ForegroundColor = UIColor.FromRGB(116, 184, 39)
			};

            if (textQ == Item.Answers[0].Text)
            {
				var prettyString = new NSMutableAttributedString(lblQuestion.Text);

				prettyString.SetAttributes(greenAttributes.Dictionary, new NSRange(unitAw, textQ.Length));

				lblQuestion.AttributedText = prettyString;

				LearnView.myPoint += 1;
			}
            else
            {
				var redAttributes = new UIStringAttributes
				{
					Font = UIFont.BoldSystemFontOfSize(15f),
					ForegroundColor = UIColor.FromRGB(234, 66, 48),
					StrikethroughStyle = NSUnderlineStyle.Single,
				};

				var green2Attributes = new UIStringAttributes
				{
					Font = UIFont.SystemFontOfSize(15f),
					ForegroundColor = UIColor.FromRGB(116, 184, 39)
				};

                var TextBoss = lblQuestion.Text + "\n\n" + Item.Inputs[0].Replace("%%", Item.Answers[0].Text);

                int ulan1 = TextBoss.IndexOf(Item.Inputs[0].Replace("%%", Item.Answers[0].Text));
                int unlan2 = TextBoss.IndexOf(Item.Answers[0].Text, ulan1);

                var prettyString = new NSMutableAttributedString(TextBoss);

                prettyString.SetAttributes(redAttributes.Dictionary, new NSRange(unitAw, textQ.Length));
                prettyString.SetAttributes(green2Attributes.Dictionary, new NSRange(ulan1, Item.Inputs[0].Replace("%%", Item.Answers[0].Text).Length));
                prettyString.SetAttributes(greenAttributes.Dictionary, new NSRange(unlan2, Item.Answers[0].Text.Length));

                lblQuestion.AttributedText = prettyString;
			}

            UserInteractionEnabled = false;

            btnContinue.Hidden = true;
			Answered.DidAnswer(this);
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