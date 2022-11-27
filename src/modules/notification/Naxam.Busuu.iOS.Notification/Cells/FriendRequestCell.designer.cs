// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Notification.Cells
{
	[Register ("FriendRequestCell")]
	partial class FriendRequestCell
	{
		[Outlet]
		UIKit.UIButton btnNo { get; set; }

		[Outlet]
		UIKit.UIButton btnYes { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint btnYesTrailingConstraint { get; set; }

		[Outlet]
		UIKit.UIImageView imgNo { get; set; }

		[Outlet]
		UIKit.UIImageView imgUser { get; set; }

		[Outlet]
		UIKit.UIImageView imgYes { get; set; }

		[Outlet]
		UIKit.UILabel lblUserName { get; set; }

		[Action ("btnNo_TouchUpInside:")]
		partial void btnNo_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnYes_TouchUpInside:")]
		partial void btnYes_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnNo != null) {
				btnNo.Dispose ();
				btnNo = null;
			}

			if (btnYes != null) {
				btnYes.Dispose ();
				btnYes = null;
			}

			if (btnYesTrailingConstraint != null) {
				btnYesTrailingConstraint.Dispose ();
				btnYesTrailingConstraint = null;
			}

			if (imgUser != null) {
				imgUser.Dispose ();
				imgUser = null;
			}

			if (lblUserName != null) {
				lblUserName.Dispose ();
				lblUserName = null;
			}

			if (imgYes != null) {
				imgYes.Dispose ();
				imgYes = null;
			}

			if (imgNo != null) {
				imgNo.Dispose ();
				imgNo = null;
			}
		}
	}
}
