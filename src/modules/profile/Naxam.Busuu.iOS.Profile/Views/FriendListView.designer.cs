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
	[Register ("FriendListView")]
	partial class FriendListView
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnCloseSeach { get; set; }

		[Outlet]
		UIKit.UIButton btnSeach { get; set; }

		[Outlet]
		UIKit.UITableView FriendListTableView { get; set; }

		[Outlet]
		UIKit.UITableView FriendSearchTableView { get; set; }

		[Outlet]
		UIKit.UILabel textFakeFriends { get; set; }

		[Outlet]
		UIKit.UILabel textFakeSeach { get; set; }

		[Outlet]
		UIKit.UITextField textFieldSeach { get; set; }

		[Outlet]
		UIKit.UIView viewStatusBar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (btnCloseSeach != null) {
				btnCloseSeach.Dispose ();
				btnCloseSeach = null;
			}

			if (btnSeach != null) {
				btnSeach.Dispose ();
				btnSeach = null;
			}

			if (FriendListTableView != null) {
				FriendListTableView.Dispose ();
				FriendListTableView = null;
			}

			if (textFakeFriends != null) {
				textFakeFriends.Dispose ();
				textFakeFriends = null;
			}

			if (textFakeSeach != null) {
				textFakeSeach.Dispose ();
				textFakeSeach = null;
			}

			if (textFieldSeach != null) {
				textFieldSeach.Dispose ();
				textFieldSeach = null;
			}

			if (viewStatusBar != null) {
				viewStatusBar.Dispose ();
				viewStatusBar = null;
			}

			if (FriendSearchTableView != null) {
				FriendSearchTableView.Dispose ();
				FriendSearchTableView = null;
			}
		}
	}
}
