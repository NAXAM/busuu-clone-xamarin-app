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
	[Register ("ChooseLanguageView")]
	partial class ChooseLanguageView
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UITableView LanguageTableView { get; set; }

		[Outlet]
		UIKit.UIView ViewForTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LanguageTableView != null) {
				LanguageTableView.Dispose ();
				LanguageTableView = null;
			}

			if (ViewForTableView != null) {
				ViewForTableView.Dispose ();
				ViewForTableView = null;
			}

			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}
		}
	}
}
