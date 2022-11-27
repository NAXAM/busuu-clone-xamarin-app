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
	[Register ("NotificationSettingView")]
	partial class NotificationSettingView
	{
		[Outlet]
		UIKit.UISwitch switchCorrectionAdded { get; set; }

		[Outlet]
		UIKit.UISwitch switchCorrectionReceived { get; set; }

		[Outlet]
		UIKit.UISwitch switchCorrectionRequest { get; set; }

		[Outlet]
		UIKit.UISwitch switchFriendRequests { get; set; }

		[Outlet]
		UIKit.UISwitch switchNotifications { get; set; }

		[Outlet]
		UIKit.UISwitch switchPrivateMode { get; set; }

		[Outlet]
		UIKit.UISwitch switchReplies { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (switchCorrectionAdded != null) {
				switchCorrectionAdded.Dispose ();
				switchCorrectionAdded = null;
			}

			if (switchCorrectionReceived != null) {
				switchCorrectionReceived.Dispose ();
				switchCorrectionReceived = null;
			}

			if (switchCorrectionRequest != null) {
				switchCorrectionRequest.Dispose ();
				switchCorrectionRequest = null;
			}

			if (switchFriendRequests != null) {
				switchFriendRequests.Dispose ();
				switchFriendRequests = null;
			}

			if (switchNotifications != null) {
				switchNotifications.Dispose ();
				switchNotifications = null;
			}

			if (switchReplies != null) {
				switchReplies.Dispose ();
				switchReplies = null;
			}

			if (switchPrivateMode != null) {
				switchPrivateMode.Dispose ();
				switchPrivateMode = null;
			}
		}
	}
}
