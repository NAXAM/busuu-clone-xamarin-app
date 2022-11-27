using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Social.ViewModels;
using PatridgeDev;
using AVFoundation;
using Foundation;
using UIKit;
using Naxam.Busuu.iOS.Social.Common;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Platform;
using System.Collections.Generic;
using Naxam.Busuu.iOS.Core.Converter;
using Naxam.Busuu.Core.Converter;

namespace Naxam.Busuu.iOS.Social.Views
{
    [MvxFromStoryboard(StoryboardName = "Social")]
    public partial class SocialDetailView : MvxViewController<SocialDetailViewModel>
    {
        public static IMvxMessenger messengerReport = Mvx.Resolve<IMvxMessenger>();
        MvxSubscriptionToken token;

        PDRatingView ratingView;
        readonly MvxImageViewLoader _loaderImageUser;
        readonly MvxImageViewLoader _loaderImgQuestion;

		AVAudioPlayer SpeakMusicPlayer;
		NSTimer update_timer;

		UIImage playBtnBg, pauseBtnBg;
            
		public SocialDetailView(IntPtr handle) : base(handle)
        {
            _loaderImageUser = new MvxImageViewLoader(() => imgUserAvatar);
            _loaderImageUser.DefaultImagePath = "res:user_avatar_placeholder.png";
            _loaderImgQuestion = new MvxImageViewLoader(() => imgQuestion);
            _loaderImgQuestion.DefaultImagePath = "res:ImageMeoNo.jpg";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationController.NavigationBarHidden = false;

			token = messengerReport.Subscribe<ShowReportDialogMessage>(OnReportMessage);

			NavigationController.NavigationBar.Layer.ShadowRadius = 2;
			NavigationController.NavigationBar.Layer.ShadowOffset = new CGSize(0, 2);
			NavigationController.NavigationBar.Layer.ShadowOpacity = 0.25f;

            var dsSource = new SocialDetailTableViewSource(SocialDetailTableView);
			SocialDetailTableView.RowHeight = UITableView.AutomaticDimension;
			SocialDetailTableView.EstimatedRowHeight = 168f;

			var setBinding = this.CreateBindingSet<SocialDetailView, SocialDetailViewModel>();
            setBinding.Bind(_loaderImageUser).To(d => d.SocialDetailData.User.Photo).Apply();
            setBinding.Bind(lblUserName).To(d => d.SocialDetailData.User.Name).Apply();
            setBinding.Bind(lblCountry).To(d => d.SocialDetailData.User.Country.Country).Apply();
            setBinding.Bind(_loaderImgQuestion).To(d => d.SocialDetailData.ImgQuestion).Apply();
            setBinding.Bind(textQuestion).To(d => d.SocialDetailData.TextQuestion).Apply();
            setBinding.Bind(btnAddFriends).For(d => d.Hidden).To(d => d.SocialDetailData.Friends).Apply();
            setBinding.Bind(ViewAudioPlayer).For(d => d.Hidden).To(d => d.SocialDetailData.Type).WithConversion(nameof(SocialTypeToBoolFalseConverter)).Apply();
            setBinding.Bind(audioViewBottomConstraint).For(x => x.Active).To(d => d.SocialDetailData.Type).WithConversion(nameof(SocialTypeToBoolConverter)).Apply();
            setBinding.Bind(audioViewTopConstraint).For(x => x.Active).To(d => d.SocialDetailData.Type).WithConversion(nameof(SocialTypeToBoolConverter)).Apply();
            setBinding.Bind(WriteText).For(d => d.Hidden).To(d => d.SocialDetailData.Type).WithConversion(nameof(SocialTypeToBoolConverter)).Apply();
            setBinding.Bind(WriteText).To(d => d.SocialDetailData.Content).Apply();
            setBinding.Bind(lblTimePublic).To(d => d.SocialDetailData.DatePosted).WithConversion(nameof(PostedToStringConverter)).Apply();
            setBinding.Bind(lblRate).To(d => d.CountRating).WithConversion(nameof(FeedbackCountConverter)).Apply();
            setBinding.Bind(btnFeedBack).To(d => d.CommentViewCommand).Apply();
            setBinding.Bind(dsSource).For(nameof(SocialDetailTableViewSource.ReplyViewCommand)).To(vm => vm.ReplyViewCommand).Apply();

			var imgstar = UIImage.FromBundle("play_icon_small");
            SliderSpeak.SetThumbImage(imgstar, UIControlState.Normal);
            SliderSpeak.SetThumbImage(imgstar, UIControlState.Selected);
            SliderSpeak.SetThumbImage(imgstar, UIControlState.Highlighted);

            var ratingConfig = new RatingConfig(UIImage.FromBundle("Stars" + "/grey_star2"),
                                    UIImage.FromBundle("Stars" + "/yellow_star_d"),
                                    UIImage.FromBundle("Stars" + "/yellow_star_d"));

            ratingConfig.ItemPadding = 1;
            var ratingFrame = new CGRect(CGPoint.Empty, new CGSize(95, 20));

            ratingView = new PDRatingView(ratingFrame, ratingConfig);

            this.CreateBinding(ratingView).For(vm => vm.AverageRating).To<SocialDetailViewModel>(vm => vm.Rating).Apply();

            ViewRate.Add(ratingView);
            ViewRate.SendSubviewToBack(ratingView);

			playBtnBg = UIImage.FromFile("play_btn.png");
			pauseBtnBg = UIImage.FromFile("pause_btn.png");

			var fileUrl = NSBundle.MainBundle.PathForResource("Nokia-tune-Nokia-tune", "mp3");
			Uri songURL = new NSUrl(fileUrl);
			SpeakMusicPlayer = AVAudioPlayer.FromUrl(songURL);
			SpeakMusicPlayer.Volume = 1;
			SpeakMusicPlayer.NumberOfLoops = 0;
			SpeakMusicPlayer.FinishedPlaying -= SpeakMusicPlayer_FinishedPlaying;
			SpeakMusicPlayer.FinishedPlaying += SpeakMusicPlayer_FinishedPlaying;

			UpdateViewForPlayerInfo();
			UpdateViewForPlayerState();

			ButtonAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 12, 10, 10);

            var bbcolor = UIColor.FromRGB(217, 217, 217);

            ViewQuestion.Layer.BorderWidth = 0.75f;
            ViewQuestion.Layer.BorderColor = bbcolor.CGColor;
            ViewQuestion.Layer.CornerRadius = 2;

            btnAddFriends.Layer.BorderWidth = 0.75f;
            btnAddFriends.Layer.BorderColor = bbcolor.CGColor;
			btnAddFriends.ImageEdgeInsets = new UIEdgeInsets(4, 12, 4, 12);
            btnAddFriends.Layer.CornerRadius = btnAddFriends.Frame.Height / 2;

            btnReport.ImageEdgeInsets = new UIEdgeInsets(6, 0, 6, 0);
         
            var img = UIImage.FromBundle("play_icon_small");
            SliderSpeak.SetThumbImage(img, UIControlState.Normal);
            SliderSpeak.SetThumbImage(img, UIControlState.Selected);
            SliderSpeak.SetThumbImage(img, UIControlState.Highlighted);

            var itemsFeedback = this.ViewModel.SocialDetailData.Feedbacks;
            var feedbackItems = new List<CustomFeedbackModel>();

            for (int i = 0; i < itemsFeedback.Count; i++)
            {
                if (itemsFeedback[i].Replies.Count > 0)
                {
                    feedbackItems.Add(new CustomFeedbackModel()
                    {
                        Boss = true,
                        Feedback = itemsFeedback[i].Feedback,
                        PostedDate = itemsFeedback[i].PostedDate,
                        User = itemsFeedback[i].User
					});

                    for (int j = 0; j < itemsFeedback[i].Replies.Count; j++)
                    {
                        feedbackItems.Add(new CustomFeedbackModel()
						{
                            Boss = false,
                            Feedback = itemsFeedback[i].Replies[j].Feedback,
							PostedDate = itemsFeedback[i].Replies[j].PostedDate,
							User = itemsFeedback[i].Replies[j].User
						});
                    }
                }
                else
                {
                    feedbackItems.Add(new CustomFeedbackModel()
					{
						Boss = true,
						Feedback = itemsFeedback[i].Feedback,
						PostedDate = itemsFeedback[i].PostedDate,
						User = itemsFeedback[i].User
					});
                }
            }

            dsSource.ItemsSource = feedbackItems;

			SocialDetailTableView.Source = dsSource;
		}

		void OnReportMessage(ShowReportDialogMessage obj)
		{
			EventReport();
		}

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            var size = ViewDetail.SystemLayoutSizeFittingSize(UIView.UILayoutFittingCompressedSize);
            if (size.Height != ViewDetail.Frame.Height) {
				SocialDetailTableView.TableHeaderView = null;
                var frame = new CGRect(0, 0, ViewDetail.Frame.Width, size.Height);
                ViewDetail.Frame = frame;
                SocialDetailTableView.TableHeaderView = ViewDetail;
			}
        }

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

            this.NavigationController.NavigationBarHidden = false;
		}

        partial void ButtonAudioPlay_TouchUpInside(NSObject sender)
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

        partial void btnReport_TouchUpInside(NSObject sender)
        {
            EventReport();
        }

        partial void btnAddFriends_TouchUpInside(NSObject sender)
        {
            var img = UIImage.FromBundle("friendship_request_sent.png");
            btnAddFriends.SetImage(img, UIControlState.Normal);
            btnAddFriends.SetImage(img, UIControlState.Highlighted);
        }

		void SpeakMusicPlayer_FinishedPlaying(object sender, AVStatusEventArgs e)
		{
			UpdateViewForPlayerInfo();
			UpdateViewForPlayerState();
		}

        void UpdateCurrentTime(NSTimer obj)
        {
			if (SpeakMusicPlayer.Playing)
			{
				ButtonAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 10, 10, 10);
				ButtonAudioPlay.SetImage(pauseBtnBg, UIControlState.Normal);
				var min = (int)((SpeakMusicPlayer.Duration - SpeakMusicPlayer.CurrentTime) / 60);
				var sec = (int)((SpeakMusicPlayer.Duration - SpeakMusicPlayer.CurrentTime) % 60);
				lblTime.Text = String.Format("{0:D2}:{1:D2}", min, sec);
				SliderSpeak.Value = (float)SpeakMusicPlayer.CurrentTime;
			}
			else
			{
				ButtonAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 12, 10, 10);
				ButtonAudioPlay.SetImage(playBtnBg, UIControlState.Normal);
			}
        }

		void UpdateViewForPlayerState()
		{
            if (SpeakMusicPlayer.Playing)
            {
                ButtonAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 10, 10, 10);
                ButtonAudioPlay.SetImage(pauseBtnBg, UIControlState.Normal);
                if (update_timer == null)
                {
					InvokeOnMainThread(() =>
					{
						update_timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(0.01), UpdateCurrentTime);
					});
                }    
            }
			else
			{
				ButtonAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 12, 10, 10);
				ButtonAudioPlay.SetImage(playBtnBg, UIControlState.Normal);
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

        void EventReport()
        {
			var alert = UIAlertController.Create("Report abuse", "Are you sure?", UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Default, null));
			alert.AddAction(UIAlertAction.Create("Yes", UIAlertActionStyle.Default, ReportHandleAction));
			PresentViewController(alert, true, null);
        }

        void ReportHandleAction(UIAlertAction obj)
        {
			UIAlertController actionSheetAlert = UIAlertController.Create("What's your reason for reporting this post?", "Reports are reviwed by our Customer Service agents who will take action if the activity violates our Terms.", UIAlertControllerStyle.ActionSheet);

			// Add Actions
			actionSheetAlert.AddAction(UIAlertAction.Create("It's sapm", UIAlertActionStyle.Default, ReportSuccessHandleAction));

			actionSheetAlert.AddAction(UIAlertAction.Create("It's not helpful", UIAlertActionStyle.Default, ReportSuccessHandleAction));

			actionSheetAlert.AddAction(UIAlertAction.Create("It's abusive or harmful", UIAlertActionStyle.Default, ReportSuccessHandleAction));

			actionSheetAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));

			// Required for iPad - You must specify a source for the Action Sheet since it is
			// displayed as a popover
			UIPopoverPresentationController presentationPopover = actionSheetAlert.PopoverPresentationController;
			if (presentationPopover != null)
			{
				presentationPopover.SourceView = this.View;
				presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
			}

			// Display the alert
			this.PresentViewController(actionSheetAlert, true, null);
        }

        void ReportSuccessHandleAction(UIAlertAction obj)
        {
			var alert = UIAlertController.Create("Thank you", "Your report has been sent to our Customer Service team.", UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("Ok thanks", UIAlertActionStyle.Default, null));
			PresentViewController(alert, true, null);
        }
    }
}
