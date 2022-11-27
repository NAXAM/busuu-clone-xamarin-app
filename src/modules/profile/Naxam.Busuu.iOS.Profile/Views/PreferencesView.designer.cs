// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Profile.Views
{
	[Register ("PreferencesView")]
	partial class PreferencesView
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnContactUs { get; set; }

		[Outlet]
		UIKit.UIButton btnCountry { get; set; }

		[Outlet]
		UIKit.UIButton btnDone { get; set; }

		[Outlet]
		UIKit.UIButton btnInterfaceLanguage { get; set; }

		[Outlet]
		UIKit.UIButton btnISpeak { get; set; }

		[Outlet]
		UIKit.UIButton btnItWorks { get; set; }

		[Outlet]
		UIKit.UIButton btnLogout { get; set; }

		[Outlet]
		UIKit.UIButton btnNotifications { get; set; }

		[Outlet]
		UIKit.UIButton btnPhoto { get; set; }

		[Outlet]
		UIKit.UIImageView imgUser { get; set; }

		[Outlet]
		UIKit.UILabel lblAboutMe { get; set; }

		[Outlet]
		UIKit.UILabel lblGender { get; set; }

		[Outlet]
		UIKit.UILabel lblInterfaceLanguage { get; set; }

		[Outlet]
		UIKit.UILabel lblISpeak { get; set; }

		[Outlet]
		UIKit.UILabel lblUserCountry { get; set; }

		[Outlet]
		UIKit.UILabel lblUserName { get; set; }

		[Outlet]
		UIKit.UITextView textViewInput { get; set; }

		[Outlet]
		UIKit.UIView viewAppBarS { get; set; }

		[Outlet]
		UIKit.UIView viewBar { get; set; }

		[Outlet]
		UIKit.UIView viewInputText { get; set; }

		[Outlet]
		UIKit.UIView viewRadioButton1 { get; set; }

		[Outlet]
		UIKit.UIView viewRadioButton2 { get; set; }

		[Outlet]
		UIKit.UIView viewRadioButton3 { get; set; }

		[Outlet]
		UIKit.UIView viewSelectGender { get; set; }

		[Outlet]
		UIKit.UIView viewSetting { get; set; }

		[Action ("btnAboutMe_TouchUpInside:")]
		partial void btnAboutMe_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnCancel_TouchUpInside:")]
		partial void btnCancel_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnCancelCode_TouchupInside:")]
		partial void btnCancelCode_TouchupInside (Foundation.NSObject sender);

		[Action ("btnClearData_TouchUpInside:")]
		partial void btnClearData_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnContactUs_TouchUpInside:")]
		partial void btnContactUs_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnDone_TouchUpInside:")]
		partial void btnDone_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnEditName_TouchUpInside:")]
		partial void btnEditName_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnGender_TouchUpInside:")]
		partial void btnGender_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnLogout_TouchUpInside:")]
		partial void btnLogout_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnPhoto_TouchUpInside:")]
		partial void btnPhoto_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnSubmitCode_TouchUpInside:")]
		partial void btnSubmitCode_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnVoucherCode_TouchUpInside:")]
		partial void btnVoucherCode_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (btnContactUs != null) {
				btnContactUs.Dispose ();
				btnContactUs = null;
			}

			if (btnCountry != null) {
				btnCountry.Dispose ();
				btnCountry = null;
			}

			if (btnDone != null) {
				btnDone.Dispose ();
				btnDone = null;
			}

			if (btnInterfaceLanguage != null) {
				btnInterfaceLanguage.Dispose ();
				btnInterfaceLanguage = null;
			}

			if (btnISpeak != null) {
				btnISpeak.Dispose ();
				btnISpeak = null;
			}

			if (btnItWorks != null) {
				btnItWorks.Dispose ();
				btnItWorks = null;
			}

			if (btnLogout != null) {
				btnLogout.Dispose ();
				btnLogout = null;
			}

			if (btnNotifications != null) {
				btnNotifications.Dispose ();
				btnNotifications = null;
			}

			if (btnPhoto != null) {
				btnPhoto.Dispose ();
				btnPhoto = null;
			}

			if (imgUser != null) {
				imgUser.Dispose ();
				imgUser = null;
			}

			if (lblAboutMe != null) {
				lblAboutMe.Dispose ();
				lblAboutMe = null;
			}

			if (lblGender != null) {
				lblGender.Dispose ();
				lblGender = null;
			}

			if (lblInterfaceLanguage != null) {
				lblInterfaceLanguage.Dispose ();
				lblInterfaceLanguage = null;
			}

			if (lblISpeak != null) {
				lblISpeak.Dispose ();
				lblISpeak = null;
			}

			if (lblUserCountry != null) {
				lblUserCountry.Dispose ();
				lblUserCountry = null;
			}

			if (lblUserName != null) {
				lblUserName.Dispose ();
				lblUserName = null;
			}

			if (textViewInput != null) {
				textViewInput.Dispose ();
				textViewInput = null;
			}

			if (viewAppBarS != null) {
				viewAppBarS.Dispose ();
				viewAppBarS = null;
			}

			if (viewBar != null) {
				viewBar.Dispose ();
				viewBar = null;
			}

			if (viewInputText != null) {
				viewInputText.Dispose ();
				viewInputText = null;
			}

			if (viewRadioButton1 != null) {
				viewRadioButton1.Dispose ();
				viewRadioButton1 = null;
			}

			if (viewRadioButton2 != null) {
				viewRadioButton2.Dispose ();
				viewRadioButton2 = null;
			}

			if (viewRadioButton3 != null) {
				viewRadioButton3.Dispose ();
				viewRadioButton3 = null;
			}

			if (viewSelectGender != null) {
				viewSelectGender.Dispose ();
				viewSelectGender = null;
			}

			if (viewSetting != null) {
				viewSetting.Dispose ();
				viewSetting = null;
			}
		}
	}
}
