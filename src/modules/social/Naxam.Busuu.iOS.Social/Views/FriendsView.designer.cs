// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Social.Views
{
	[Register ("FriendsView")]
	partial class FriendsView
	{
		[Outlet]
		UIKit.UITableView FriendsTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (FriendsTableView != null) {
				FriendsTableView.Dispose ();
				FriendsTableView = null;
			}
		}
	}
}
