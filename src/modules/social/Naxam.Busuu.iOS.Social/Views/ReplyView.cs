using System;
using System.Drawing;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using Naxam.Busuu.Social.ViewModels;
using UIKit;

namespace Naxam.Busuu.iOS.Social.Views
{
    [MvxFromStoryboard(StoryboardName = "Social")]
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class ReplyView : MvxViewController<ReplyViewModel>
	{
        const string TextViewPlaceHolder = "Reply to Naxam";
        NSTimer update_timer;
        double timeSendLoad;

        public ReplyView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationController.NavigationBarHidden = true;

            viewPhu.Layer.CornerRadius = 8;

            ViewReplyBar.Layer.ShadowRadius = 2;
            ViewReplyBar.Layer.ShadowOpacity = 0.25f;
            ViewReplyBar.Layer.ShadowOffset = new CGSize(0, 2);

            btnReply.Layer.ShadowRadius = 2;
			btnReply.Layer.ShadowOpacity = 0.25f;
			btnReply.Layer.ShadowOffset = new CGSize(0, 2);

            var setBinding = this.CreateBindingSet<ReplyView, ReplyViewModel>();
            setBinding.Bind(btnBack).To(vm => vm.GoBackCommand);
            setBinding.Apply();

			textViewReply.ShouldBeginEditing += TextViewShouldBeginEditing;
			textViewReply.ShouldEndEditing += TextViewShouldEndEditing;
            textViewReply.ShouldChangeText += TextViewReply_ShouldChangeText;

            btnReply.ImageEdgeInsets = new UIEdgeInsets(12, 16, 12, 16);

            textViewReply.BecomeFirstResponder();

           
			UIPanGestureRecognizer gesture = new UIPanGestureRecognizer();

			gesture.AddTarget(() => HandleDrag(gesture));

            btnReply.AddGestureRecognizer(gesture);

            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
        }

	    void HandleDrag(UIPanGestureRecognizer recognizer)
		{
            if (recognizer.State == UIGestureRecognizerState.Changed)
			{
                PointF offset = (PointF)recognizer.TranslationInView(btnReply);

                if (offset.X <= 0)
                {
					textSwipeCenterXConstraint.Constant = offset.X;
				}
               
                if (textSwipe.Alpha > 0)
                {
					textSwipe.Alpha = 1 - (nfloat)((offset.X - offset.X * 2) * 0.01);		
                }

				if (textSwipe.Alpha <= 0)
				{
					viewReplySpeak.Hidden = true;
                    textSwipe.Alpha = 1;
					viewSliderWidthConstraint.Constant = 0;
                    textSwipeCenterXConstraint.Constant = 0;

					if (update_timer != null)
					{
						update_timer.Invalidate();
						update_timer = null;
					}
				}
			}

            if (recognizer.State == UIGestureRecognizerState.Ended)
			{
                if (textSwipe.Alpha > 0)
                {
					CheckTouch();
                }
                else
                {
					textSwipe.Alpha = 1;
                }     
			}
		}

		bool TextViewShouldEndEditing(UITextView textView)
		{
			if (textViewReply.Text == "")
			{
				textViewReply.Text = TextViewPlaceHolder;
				textViewReply.TextColor = UIColor.FromRGB(173, 182, 187);
			}
			return true;
		}

        void OnKeyboardNotification(NSNotification notification)
        {
            NSValue keyboardFrame = notification.UserInfo.ObjectForKey(UIKeyboard.FrameEndUserInfoKey) as NSValue;
            var keyboardRectangle = keyboardFrame.CGRectValue;

            if (notification.Name == "UIKeyboardWillShowNotification")
            {
                viewReplySpeakHeigthConstraint.Constant = keyboardRectangle.Height;
			}
            else
            {
                viewReplySpeakHeigthConstraint.Constant = 0;
			}
        }

		bool TextViewShouldBeginEditing(UITextView textView)
		{
			if (textViewReply.Text == TextViewPlaceHolder)
			{
				textViewReply.Text = "";
				textViewReply.TextColor = UIColor.Black;
			}
			return true;
		}

        bool TextViewReply_ShouldChangeText(UITextView textView, NSRange range, string text)
		{
            UIImage img;
            if (text != "")
            {
				img = UIImage.FromBundle("ic_send_light");
				btnReply.ImageEdgeInsets = new UIEdgeInsets(10, 14, 10, 10);
			}
            else
            {
				img = UIImage.FromBundle("conversation_speaking_button_white");
				btnReply.ImageEdgeInsets = new UIEdgeInsets(12, 16, 12, 16);
            }

			btnReply.SetImage(img, UIControlState.Normal);
			btnReply.SetImage(img, UIControlState.Selected);
			btnReply.SetImage(img, UIControlState.Highlighted);

            return true;
		}

		void UpdateTimeAction(NSTimer obj)
		{
            if ((textViewReply.Text == "Reply to Naxam") || (textViewReply.Text == ""))
            {
                double abcd = (viewReplySpeak.Bounds.Width / 30) * 0.01;

                viewSliderWidthConstraint.Constant += (nfloat)abcd;
            }

			if (!viewSendActivity.Hidden)
				timeSendLoad += 0.01;

			if (timeSendLoad > 1.5)
			{
				activityControl.StopAnimating();

				if (update_timer != null)
				{
					update_timer.Invalidate();
					update_timer = null;
				}

				timeSendLoad = 0;

				this.ViewModel.GoBackCommand.Execute();
			}
		}

        void CheckTouch()
        {
			if (update_timer == null)
			{
                InvokeOnMainThread(() =>
                {
                    update_timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(0.01), UpdateTimeAction);
                });
			}
            
            if (((textViewReply.Text != "") && (textViewReply.Text != "Reply to Naxam")) || ((textSwipe.Alpha > 0) && (viewSliderWidthConstraint.Constant > 1)))
            {
                textViewReply.ResignFirstResponder();

                viewSendActivity.Hidden = false;
                activityControl.StartAnimating();
            }
            else
            {
				viewReplySpeak.Hidden = true;
				textSwipe.Alpha = 1;
				viewSliderWidthConstraint.Constant = 0;
				textSwipeCenterXConstraint.Constant = 0;

				if (update_timer != null)
				{
					update_timer.Invalidate();
					update_timer = null;
				}
            }
        }

		partial void btnReply_TouchDown(Foundation.NSObject sender)
		{
            if ((textViewReply.Text == "Reply to Naxam") || (textViewReply.Text == ""))
            {
				viewReplySpeak.Hidden = false;

				if (update_timer == null)
				{
                    InvokeOnMainThread(() =>
                    {
                        update_timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(0.01), UpdateTimeAction);
                    });
				}			
            }
		}

        partial void btnReply_TouchUpInside(Foundation.NSObject sender)
        {
            CheckTouch();
        }

		partial void btnReply_TouchUpOutside(Foundation.NSObject sender)
		{
			CheckTouch();
		}
    }
}
