// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Core.Views
{
	[Register ("BuyPremium")]
	partial class BuyPremium
	{
		[Outlet]
		UIKit.UIButton btnRestore { get; set; }

		[Outlet]
		UIKit.UIView monthView { get; set; }

		[Outlet]
		UIKit.UIView sixMonthView { get; set; }

		[Outlet]
		UIKit.UIView yearView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (monthView != null) {
				monthView.Dispose ();
				monthView = null;
			}

			if (sixMonthView != null) {
				sixMonthView.Dispose ();
				sixMonthView = null;
			}

			if (yearView != null) {
				yearView.Dispose ();
				yearView = null;
			}

			if (btnRestore != null) {
				btnRestore.Dispose ();
				btnRestore = null;
			}
		}
	}
}
