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
	[Register ("NotificationCell")]
	partial class NotificationCell
	{
		[Outlet]
		UIKit.UIImageView imgUser { get; set; }

		[Outlet]
		UIKit.UILabel lblDetail { get; set; }

		[Outlet]
		UIKit.UILabel lblTime { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgUser != null) {
				imgUser.Dispose ();
				imgUser = null;
			}

			if (lblDetail != null) {
				lblDetail.Dispose ();
				lblDetail = null;
			}

			if (lblTime != null) {
				lblTime.Dispose ();
				lblTime = null;
			}
		}
	}
}
