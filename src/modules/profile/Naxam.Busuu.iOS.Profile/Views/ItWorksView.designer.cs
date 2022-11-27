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
	[Register ("ItWorksView")]
	partial class ItWorksView
	{
		[Outlet]
		UIKit.UIButton btnRead { get; set; }

		[Outlet]
		UIKit.UIImageView imgComplete { get; set; }

		[Outlet]
		UIKit.UIImageView imgPractice { get; set; }

		[Outlet]
		UIKit.UILabel lblComplete { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint lblCompleteTopConstraint { get; set; }

		[Outlet]
		UIKit.UILabel lblHtml { get; set; }

		[Outlet]
		UIKit.UILabel lblPractice { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint lblPracticeTopConstraint { get; set; }

		[Outlet]
		UIKit.UIView viewComplete { get; set; }

		[Outlet]
		UIKit.UIView viewPractice { get; set; }

		[Action ("btnRead_TouchUpInside:")]
		partial void btnRead_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (viewComplete != null) {
				viewComplete.Dispose ();
				viewComplete = null;
			}

			if (lblComplete != null) {
				lblComplete.Dispose ();
				lblComplete = null;
			}

			if (viewPractice != null) {
				viewPractice.Dispose ();
				viewPractice = null;
			}

			if (lblPractice != null) {
				lblPractice.Dispose ();
				lblPractice = null;
			}

			if (lblPracticeTopConstraint != null) {
				lblPracticeTopConstraint.Dispose ();
				lblPracticeTopConstraint = null;
			}

			if (lblCompleteTopConstraint != null) {
				lblCompleteTopConstraint.Dispose ();
				lblCompleteTopConstraint = null;
			}

			if (btnRead != null) {
				btnRead.Dispose ();
				btnRead = null;
			}

			if (imgComplete != null) {
				imgComplete.Dispose ();
				imgComplete = null;
			}

			if (imgPractice != null) {
				imgPractice.Dispose ();
				imgPractice = null;
			}

			if (lblHtml != null) {
				lblHtml.Dispose ();
				lblHtml = null;
			}
		}
	}
}
