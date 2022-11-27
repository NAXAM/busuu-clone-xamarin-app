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
	[Register ("SettingLanguageSpeakView")]
	partial class SettingLanguageSpeakView
	{
		[Outlet]
		UIKit.UIButton btnAccept { get; set; }

		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnCancel { get; set; }

		[Outlet]
		UIKit.UIButton btnDone { get; set; }

		[Outlet]
		UIKit.UITableView LanguageSpeakTableView { get; set; }

		[Outlet]
		UIKit.UISlider sliderLever { get; set; }

		[Outlet]
		UIKit.UILabel textChooseLever { get; set; }

		[Outlet]
		UIKit.UIView ViewBackgroundDialog { get; set; }

		[Outlet]
		UIKit.UIView viewChooseLever { get; set; }

		[Outlet]
		UIKit.UIView ViewDialog { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint viewdot1LeadingContraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint viewdot2LeadingContraint { get; set; }

		[Outlet]
		UIKit.UIView viewForDialog { get; set; }

		[Outlet]
		UIKit.UIView ViewStartBar { get; set; }

		[Action ("btnAccept_TouchUpInside:")]
		partial void btnAccept_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnCancel_TouchUpInside:")]
		partial void btnCancel_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAccept != null) {
				btnAccept.Dispose ();
				btnAccept = null;
			}

			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (btnCancel != null) {
				btnCancel.Dispose ();
				btnCancel = null;
			}

			if (btnDone != null) {
				btnDone.Dispose ();
				btnDone = null;
			}

			if (LanguageSpeakTableView != null) {
				LanguageSpeakTableView.Dispose ();
				LanguageSpeakTableView = null;
			}

			if (sliderLever != null) {
				sliderLever.Dispose ();
				sliderLever = null;
			}

			if (textChooseLever != null) {
				textChooseLever.Dispose ();
				textChooseLever = null;
			}

			if (ViewBackgroundDialog != null) {
				ViewBackgroundDialog.Dispose ();
				ViewBackgroundDialog = null;
			}

			if (viewChooseLever != null) {
				viewChooseLever.Dispose ();
				viewChooseLever = null;
			}

			if (ViewDialog != null) {
				ViewDialog.Dispose ();
				ViewDialog = null;
			}

			if (viewForDialog != null) {
				viewForDialog.Dispose ();
				viewForDialog = null;
			}

			if (ViewStartBar != null) {
				ViewStartBar.Dispose ();
				ViewStartBar = null;
			}

			if (viewdot2LeadingContraint != null) {
				viewdot2LeadingContraint.Dispose ();
				viewdot2LeadingContraint = null;
			}

			if (viewdot1LeadingContraint != null) {
				viewdot1LeadingContraint.Dispose ();
				viewdot1LeadingContraint = null;
			}
		}
	}
}
