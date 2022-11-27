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
	[Register ("StartPageView")]
	partial class StartPageView
	{
		[Outlet]
		UIKit.UIButton btnGetStarted { get; set; }

		[Outlet]
		UIKit.UIButton btnLogin { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnLogin != null) {
				btnLogin.Dispose ();
				btnLogin = null;
			}

			if (btnGetStarted != null) {
				btnGetStarted.Dispose ();
				btnGetStarted = null;
			}
		}
	}
}
