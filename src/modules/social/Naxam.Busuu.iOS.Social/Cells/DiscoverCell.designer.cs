// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Social.Cells
{
	[Register ("DiscoverCell")]
	partial class DiscoverCell
	{
		[Outlet]
		UIKit.NSLayoutConstraint audioViewBottomConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint audioViewTopConstraint { get; set; }

		[Outlet]
		UIKit.UIButton ButtonPlay { get; set; }

		[Outlet]
		UIKit.UILabel Country { get; set; }

		[Outlet]
		UIKit.UIImageView ImageLan { get; set; }

		[Outlet]
		UIKit.UIImageView ImageUser { get; set; }

		[Outlet]
		UIKit.UIImageView ImgSpeak { get; set; }

		[Outlet]
		UIKit.UILabel lblTime { get; set; }

		[Outlet]
		UIKit.UILabel NameUser { get; set; }

		[Outlet]
		UIKit.UISlider SliderSpeak { get; set; }

		[Outlet]
		UIKit.UILabel TextLan { get; set; }

		[Outlet]
		UIKit.UIView ViewHome { get; set; }

		[Outlet]
		UIKit.UIView ViewLan { get; set; }

		[Outlet]
		UIKit.UIView ViewSpeak { get; set; }

		[Outlet]
		UIKit.UILabel WriteLabel { get; set; }

		[Action ("btnView_TouchUpInside:")]
		partial void btnView_TouchUpInside (Foundation.NSObject sender);

		[Action ("ButtonPlay_TouchUpInside:")]
		partial void ButtonPlay_TouchUpInside (Foundation.NSObject sender);
		
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

			if (ButtonPlay != null) {
				ButtonPlay.Dispose ();
				ButtonPlay = null;
			}

			if (Country != null) {
				Country.Dispose ();
				Country = null;
			}

			if (ImageLan != null) {
				ImageLan.Dispose ();
				ImageLan = null;
			}

			if (ImageUser != null) {
				ImageUser.Dispose ();
				ImageUser = null;
			}

			if (ImgSpeak != null) {
				ImgSpeak.Dispose ();
				ImgSpeak = null;
			}

			if (lblTime != null) {
				lblTime.Dispose ();
				lblTime = null;
			}

			if (NameUser != null) {
				NameUser.Dispose ();
				NameUser = null;
			}

			if (SliderSpeak != null) {
				SliderSpeak.Dispose ();
				SliderSpeak = null;
			}

			if (TextLan != null) {
				TextLan.Dispose ();
				TextLan = null;
			}

			if (ViewHome != null) {
				ViewHome.Dispose ();
				ViewHome = null;
			}

			if (ViewLan != null) {
				ViewLan.Dispose ();
				ViewLan = null;
			}

			if (ViewSpeak != null) {
				ViewSpeak.Dispose ();
				ViewSpeak = null;
			}

			if (WriteLabel != null) {
				WriteLabel.Dispose ();
				WriteLabel = null;
			}
		}
	}
}
