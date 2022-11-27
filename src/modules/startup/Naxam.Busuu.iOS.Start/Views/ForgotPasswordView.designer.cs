// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Start.Views
{
	[Register ("ForgotPasswordView")]
	partial class ForgotPasswordView
	{
		[Outlet]
		UIKit.UIButton btnSendLink { get; set; }

		[Outlet]
		UIKit.UIView viewEmailPhone { get; set; }

		[Outlet]
		UIKit.UIView viewLineEmailPhone { get; set; }

		[Outlet]
		UIKit.UIView ViewShadow { get; set; }

		[Action ("btnSendLink_TochUpInside:")]
		partial void btnSendLink_TochUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnSendLink != null) {
				btnSendLink.Dispose ();
				btnSendLink = null;
			}

			if (viewEmailPhone != null) {
				viewEmailPhone.Dispose ();
				viewEmailPhone = null;
			}

			if (ViewShadow != null) {
				ViewShadow.Dispose ();
				ViewShadow = null;
			}

			if (viewLineEmailPhone != null) {
				viewLineEmailPhone.Dispose ();
				viewLineEmailPhone = null;
			}
		}
	}
}
