// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Social.Views
{
	[Register ("CommentView")]
	partial class CommentView
	{
		[Outlet]
		UIKit.NSLayoutConstraint audioViewBottomConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint audioViewTopConstraint { get; set; }

		[Outlet]
		UIKit.UIButton btnAudioPlay { get; set; }

		[Outlet]
		UIKit.UIButton btnRemove { get; set; }

		[Outlet]
		UIKit.UIButton btnSay { get; set; }

		[Outlet]
		UIKit.UIImageView imgCircle { get; set; }

		[Outlet]
		UIKit.UIImageView imgQuestion { get; set; }

		[Outlet]
		UIKit.UILabel lblTime { get; set; }

		[Outlet]
		UIKit.UILabel lblTimeSay { get; set; }

		[Outlet]
		UIKit.UISlider SliderSpeak { get; set; }

		[Outlet]
		UIKit.UILabel textHowDid { get; set; }

		[Outlet]
		UIKit.UILabel textQuestion { get; set; }

		[Outlet]
		UIKit.UITextView textViewComment { get; set; }

		[Outlet]
		UIKit.UITextView textViewCorrect { get; set; }

		[Outlet]
		UIKit.UIView ViewAudioPlayer { get; set; }

		[Outlet]
		UIKit.UIView ViewBossQuestion { get; set; }

		[Outlet]
		UIKit.UIView ViewForSpeak { get; set; }

		[Outlet]
		UIKit.UIView ViewForWrite { get; set; }

		[Outlet]
		UIKit.UIView ViewQuestion { get; set; }

		[Outlet]
		UIKit.UIView ViewSay { get; set; }

		[Outlet]
		UIKit.UIView ViewSay2 { get; set; }

		[Outlet]
		UIKit.UIView ViewSay3 { get; set; }

		[Outlet]
		UIKit.UIView ViewShadow { get; set; }

		[Outlet]
		UIKit.UIView ViewStar { get; set; }

		[Action ("btnAudioPlay_TouchUpInside:")]
		partial void btnAudioPlay_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnRemove_TouchUpInside:")]
		partial void btnRemove_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnSay_TouchDown:")]
		partial void btnSay_TouchDown (Foundation.NSObject sender);

		[Action ("btnSay_TouchUpInside:")]
		partial void btnSay_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnSay_TouchUpOutside:")]
		partial void btnSay_TouchUpOutside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (audioViewBottomConstraint != null) {
				audioViewBottomConstraint.Dispose ();
				audioViewBottomConstraint = null;
			}

			if (audioViewTopConstraint != null) {
				audioViewTopConstraint.Dispose ();
				audioViewTopConstraint = null;
			}

			if (btnAudioPlay != null) {
				btnAudioPlay.Dispose ();
				btnAudioPlay = null;
			}

			if (btnRemove != null) {
				btnRemove.Dispose ();
				btnRemove = null;
			}

			if (btnSay != null) {
				btnSay.Dispose ();
				btnSay = null;
			}

			if (imgCircle != null) {
				imgCircle.Dispose ();
				imgCircle = null;
			}

			if (imgQuestion != null) {
				imgQuestion.Dispose ();
				imgQuestion = null;
			}

			if (lblTime != null) {
				lblTime.Dispose ();
				lblTime = null;
			}

			if (lblTimeSay != null) {
				lblTimeSay.Dispose ();
				lblTimeSay = null;
			}

			if (SliderSpeak != null) {
				SliderSpeak.Dispose ();
				SliderSpeak = null;
			}

			if (textHowDid != null) {
				textHowDid.Dispose ();
				textHowDid = null;
			}

			if (textQuestion != null) {
				textQuestion.Dispose ();
				textQuestion = null;
			}

			if (textViewComment != null) {
				textViewComment.Dispose ();
				textViewComment = null;
			}

			if (textViewCorrect != null) {
				textViewCorrect.Dispose ();
				textViewCorrect = null;
			}

			if (ViewAudioPlayer != null) {
				ViewAudioPlayer.Dispose ();
				ViewAudioPlayer = null;
			}

			if (ViewBossQuestion != null) {
				ViewBossQuestion.Dispose ();
				ViewBossQuestion = null;
			}

			if (ViewForSpeak != null) {
				ViewForSpeak.Dispose ();
				ViewForSpeak = null;
			}

			if (ViewForWrite != null) {
				ViewForWrite.Dispose ();
				ViewForWrite = null;
			}

			if (ViewQuestion != null) {
				ViewQuestion.Dispose ();
				ViewQuestion = null;
			}

			if (ViewSay != null) {
				ViewSay.Dispose ();
				ViewSay = null;
			}

			if (ViewSay2 != null) {
				ViewSay2.Dispose ();
				ViewSay2 = null;
			}

			if (ViewSay3 != null) {
				ViewSay3.Dispose ();
				ViewSay3 = null;
			}

			if (ViewShadow != null) {
				ViewShadow.Dispose ();
				ViewShadow = null;
			}

			if (ViewStar != null) {
				ViewStar.Dispose ();
				ViewStar = null;
			}
		}
	}
}
