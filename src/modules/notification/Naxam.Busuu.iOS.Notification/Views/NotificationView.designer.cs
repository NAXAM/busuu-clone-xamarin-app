// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Notification.Views
{
	[Register ("NotificationView")]
	partial class NotificationView
	{
		[Outlet]
		UIKit.UIButton btnFriendRequests { get; set; }

		[Outlet]
		UIKit.UILabel lblFriendRequestsCount { get; set; }

		[Outlet]
		UIKit.UITableView NotificationTableView { get; set; }

		[Outlet]
		UIKit.UIView viewFriendRequest { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint viewFriendRequestHeightConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint viewFriendRequestTopConstraint { get; set; }

		[Outlet]
		UIKit.UIView ViewShadow { get; set; }

		[Action ("btnFriendRequests_TouchUpInside:")]
		partial void btnFriendRequests_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnFriendRequests != null) {
				btnFriendRequests.Dispose ();
				btnFriendRequests = null;
			}

			if (lblFriendRequestsCount != null) {
				lblFriendRequestsCount.Dispose ();
				lblFriendRequestsCount = null;
			}

			if (NotificationTableView != null) {
				NotificationTableView.Dispose ();
				NotificationTableView = null;
			}

			if (viewFriendRequest != null) {
				viewFriendRequest.Dispose ();
				viewFriendRequest = null;
			}

			if (viewFriendRequestHeightConstraint != null) {
				viewFriendRequestHeightConstraint.Dispose ();
				viewFriendRequestHeightConstraint = null;
			}

			if (ViewShadow != null) {
				ViewShadow.Dispose ();
				ViewShadow = null;
			}

			if (viewFriendRequestTopConstraint != null) {
				viewFriendRequestTopConstraint.Dispose ();
				viewFriendRequestTopConstraint = null;
			}
		}
	}
}
