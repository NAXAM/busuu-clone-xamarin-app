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
	[Register ("ProfileView")]
	partial class ProfileView
	{
		[Outlet]
		UIKit.UIButton btnChangePhoto { get; set; }

		[Outlet]
		UIKit.UIButton btnFriendList { get; set; }

		[Outlet]
		UIKit.UIButton btnImgUser { get; set; }

		[Outlet]
		UIKit.UIButton btnPreference { get; set; }

		[Outlet]
		UIKit.UIButton btnShowPhoto { get; set; }

		[Outlet]
		UIKit.UILabel handleLabel { get; set; }

		[Outlet]
		UIKit.UILabel headerLabel { get; set; }

		[Outlet]
		UIKit.UIView headerView { get; set; }

		[Outlet]
		UIKit.UIImageView imgAvatar { get; set; }

		[Outlet]
		UIKit.UIImageView imgLan { get; set; }

		[Outlet]
		UIKit.UIImageView imgLanLearn { get; set; }

		[Outlet]
		UIKit.UIImageView imgUser { get; set; }

		[Outlet]
		UIKit.UILabel lblCountry { get; set; }

		[Outlet]
		UIKit.UILabel lblLearnLan { get; set; }

		[Outlet]
		UIKit.UIView popupMenuPhoto { get; set; }

		[Outlet]
		UIKit.UITableView ProfileTableView { get; set; }

		[Outlet]
		UIKit.UIView profileView { get; set; }

		[Outlet]
		UIKit.UIView viewBarItem { get; set; }

		[Outlet]
		UIKit.UIView ViewBody { get; set; }

		[Outlet]
		UIKit.UIView viewSelectForButton { get; set; }

		[Action ("btnChangePhoto_TouchUpInside:")]
		partial void btnChangePhoto_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnImgUser_TouchUpInside:")]
		partial void btnImgUser_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnMyCorrections_TouchUpInside:")]
		partial void btnMyCorrections_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnMyExercises_TouchUpInside:")]
		partial void btnMyExercises_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnShowPhoto_TouchUpInside:")]
		partial void btnShowPhoto_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnChangePhoto != null) {
				btnChangePhoto.Dispose ();
				btnChangePhoto = null;
			}

			if (btnFriendList != null) {
				btnFriendList.Dispose ();
				btnFriendList = null;
			}

			if (btnImgUser != null) {
				btnImgUser.Dispose ();
				btnImgUser = null;
			}

			if (btnPreference != null) {
				btnPreference.Dispose ();
				btnPreference = null;
			}

			if (btnShowPhoto != null) {
				btnShowPhoto.Dispose ();
				btnShowPhoto = null;
			}

			if (handleLabel != null) {
				handleLabel.Dispose ();
				handleLabel = null;
			}

			if (headerLabel != null) {
				headerLabel.Dispose ();
				headerLabel = null;
			}

			if (headerView != null) {
				headerView.Dispose ();
				headerView = null;
			}

			if (imgAvatar != null) {
				imgAvatar.Dispose ();
				imgAvatar = null;
			}

			if (imgLan != null) {
				imgLan.Dispose ();
				imgLan = null;
			}

			if (imgLanLearn != null) {
				imgLanLearn.Dispose ();
				imgLanLearn = null;
			}

			if (imgUser != null) {
				imgUser.Dispose ();
				imgUser = null;
			}

			if (lblCountry != null) {
				lblCountry.Dispose ();
				lblCountry = null;
			}

			if (lblLearnLan != null) {
				lblLearnLan.Dispose ();
				lblLearnLan = null;
			}

			if (ProfileTableView != null) {
				ProfileTableView.Dispose ();
				ProfileTableView = null;
			}

			if (popupMenuPhoto != null) {
				popupMenuPhoto.Dispose ();
				popupMenuPhoto = null;
			}

			if (profileView != null) {
				profileView.Dispose ();
				profileView = null;
			}

			if (viewBarItem != null) {
				viewBarItem.Dispose ();
				viewBarItem = null;
			}

			if (ViewBody != null) {
				ViewBody.Dispose ();
				ViewBody = null;
			}

			if (viewSelectForButton != null) {
				viewSelectForButton.Dispose ();
				viewSelectForButton = null;
			}
		}
	}
}
