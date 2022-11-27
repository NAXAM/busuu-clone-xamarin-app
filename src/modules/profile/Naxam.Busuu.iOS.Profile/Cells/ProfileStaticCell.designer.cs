// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Profile.Cells
{
	[Register ("ProfileStaticCell")]
	partial class ProfileStaticCell
	{
		[Outlet]
		UIKit.NSLayoutConstraint audioViewBottomConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint audioViewTopConstraint { get; set; }

		[Outlet]
		UIKit.UIButton ButtonAudioPlay { get; set; }

		[Outlet]
		UIKit.UIImageView imgLan { get; set; }

		[Outlet]
		UIKit.UIImageView imgUserAvatar { get; set; }

		[Outlet]
		UIKit.UILabel lblCountry { get; set; }

		[Outlet]
		UIKit.UILabel lblRate { get; set; }

		[Outlet]
		UIKit.UILabel lblTime { get; set; }

		[Outlet]
		UIKit.UILabel lblTimePublic { get; set; }

		[Outlet]
		UIKit.UILabel lblUserName { get; set; }

		[Outlet]
		UIKit.UISlider SliderSpeak { get; set; }

		[Outlet]
		UIKit.UIView ViewAudioPlayer { get; set; }

		[Outlet]
		UIKit.UIView ViewBackGroud { get; set; }

		[Outlet]
		UIKit.UIView ViewRate { get; set; }

		[Outlet]
		UIKit.UILabel WriteText { get; set; }

		[Action("ButtonAudioPlay_TouchUpInside:")]
		partial void ButtonAudioPlay_TouchUpInside(Foundation.NSObject sender);

		[Action("ButtonRate_TouchUpInside:")]
		partial void ButtonRate_TouchUpInside(Foundation.NSObject sender);

		void ReleaseDesignerOutlets()
		{
			if (audioViewBottomConstraint != null)
			{
				audioViewBottomConstraint.Dispose();
				audioViewBottomConstraint = null;
			}

			if (audioViewTopConstraint != null)
			{
				audioViewTopConstraint.Dispose();
				audioViewTopConstraint = null;
			}

			if (ButtonAudioPlay != null)
			{
				ButtonAudioPlay.Dispose();
				ButtonAudioPlay = null;
			}

			if (imgLan != null)
			{
				imgLan.Dispose();
				imgLan = null;
			}

			if (imgUserAvatar != null)
			{
				imgUserAvatar.Dispose();
				imgUserAvatar = null;
			}

			if (lblCountry != null)
			{
				lblCountry.Dispose();
				lblCountry = null;
			}

			if (lblRate != null)
			{
				lblRate.Dispose();
				lblRate = null;
			}

			if (lblTime != null)
			{
				lblTime.Dispose();
				lblTime = null;
			}

			if (lblTimePublic != null)
			{
				lblTimePublic.Dispose();
				lblTimePublic = null;
			}

			if (lblUserName != null)
			{
				lblUserName.Dispose();
				lblUserName = null;
			}

			if (SliderSpeak != null)
			{
				SliderSpeak.Dispose();
				SliderSpeak = null;
			}

			if (ViewAudioPlayer != null)
			{
				ViewAudioPlayer.Dispose();
				ViewAudioPlayer = null;
			}

			if (ViewBackGroud != null)
			{
				ViewBackGroud.Dispose();
				ViewBackGroud = null;
			}

			if (ViewRate != null)
			{
				ViewRate.Dispose();
				ViewRate = null;
			}

			if (WriteText != null)
			{
				WriteText.Dispose();
				WriteText = null;
			}
		}
	}
}
