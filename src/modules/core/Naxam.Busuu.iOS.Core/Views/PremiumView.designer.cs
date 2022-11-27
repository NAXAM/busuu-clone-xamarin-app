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
	[Register ("PremiumView")]
	partial class PremiumView
	{
		[Outlet]
		UIKit.UIButton btnSeePlan { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UITableView FeatureTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnSeePlan != null) {
				btnSeePlan.Dispose ();
				btnSeePlan = null;
			}

			if (FeatureTableView != null) {
				FeatureTableView.Dispose ();
				FeatureTableView = null;
			}
		}
	}
}
