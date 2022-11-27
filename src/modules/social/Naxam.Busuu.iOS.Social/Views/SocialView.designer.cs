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
	[Register ("SocialView")]
	partial class SocialView
	{
		[Outlet]
		UIKit.UIBarButtonItem btnFilter { get; set; }

		[Outlet]
		UIKit.UIButton ButtonDiscover { get; set; }

		[Outlet]
		UIKit.UIButton ButtonFriends { get; set; }

		[Outlet]
		UIKit.UIView ViewBarItem { get; set; }

		[Outlet]
		UIKit.UIView ViewContainer { get; set; }

		[Outlet]
		UIKit.UIView ViewSelectForButton { get; set; }

		[Action ("ButtonDiscover_TouchUpInside:")]
		partial void ButtonDiscover_TouchUpInside (Foundation.NSObject sender);

		[Action ("ButtonFriends_TouchUpInside:")]
		partial void ButtonFriends_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ButtonDiscover != null) {
				ButtonDiscover.Dispose ();
				ButtonDiscover = null;
			}

			if (ButtonFriends != null) {
				ButtonFriends.Dispose ();
				ButtonFriends = null;
			}

			if (ViewBarItem != null) {
				ViewBarItem.Dispose ();
				ViewBarItem = null;
			}

			if (ViewContainer != null) {
				ViewContainer.Dispose ();
				ViewContainer = null;
			}

			if (ViewSelectForButton != null) {
				ViewSelectForButton.Dispose ();
				ViewSelectForButton = null;
			}

			if (btnFilter != null) {
				btnFilter.Dispose ();
				btnFilter = null;
			}
		}
	}
}
