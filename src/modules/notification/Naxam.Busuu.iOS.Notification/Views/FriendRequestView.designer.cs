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
	[Register ("FriendRequestView")]
	partial class FriendRequestView
	{
		[Outlet]
		UIKit.UITableView FriendRequestTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (FriendRequestTableView != null) {
				FriendRequestTableView.Dispose ();
				FriendRequestTableView = null;
			}
		}
	}
}
