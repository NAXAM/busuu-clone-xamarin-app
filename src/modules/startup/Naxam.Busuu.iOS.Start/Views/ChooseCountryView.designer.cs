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
	[Register ("ChooseCountryView")]
	partial class ChooseCountryView
	{
		[Outlet]
		UIKit.UITableView ChooseCountryTableView { get; set; }

		[Outlet]
		UIKit.UIView ViewShadow { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ChooseCountryTableView != null) {
				ChooseCountryTableView.Dispose ();
				ChooseCountryTableView = null;
			}

			if (ViewShadow != null) {
				ViewShadow.Dispose ();
				ViewShadow = null;
			}
		}
	}
}
