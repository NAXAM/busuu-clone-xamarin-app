// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Review.Cells
{
	[Register ("ReviewTableViewCell")]
	partial class ReviewTableViewCell
	{
		[Outlet]
		UIKit.UIButton btnPlay { get; set; }

		[Outlet]
		UIKit.UIButton btnStar { get; set; }

		[Outlet]
		UIKit.UIImageView imgStar { get; set; }

		[Outlet]
		UIKit.UIImageView imgStrength { get; set; }

		[Outlet]
		UIKit.UIImageView imgWord { get; set; }

		[Outlet]
		UIKit.UILabel lblSubtitleSample { get; set; }

		[Outlet]
		UIKit.UILabel lblTitleSample { get; set; }

		[Outlet]
		UIKit.UILabel lbSubtitle { get; set; }

		[Outlet]
		UIKit.UILabel lbTitle { get; set; }

		[Outlet]
		UIKit.UIView viewSample { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint viewSampleHeithConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint viewSampleTopConstraint { get; set; }

		[Action ("btnPlay_TouchUpInside:")]
		partial void btnPlay_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnStar_TouchUpInside:")]
		partial void btnStar_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnTap_TouchUpInside:")]
		partial void btnTap_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnPlay != null) {
				btnPlay.Dispose ();
				btnPlay = null;
			}

			if (btnStar != null) {
				btnStar.Dispose ();
				btnStar = null;
			}

			if (imgStrength != null) {
				imgStrength.Dispose ();
				imgStrength = null;
			}

			if (imgWord != null) {
				imgWord.Dispose ();
				imgWord = null;
			}

			if (imgStar != null) {
				imgStar.Dispose ();
				imgStar = null;
			}

			if (lblSubtitleSample != null) {
				lblSubtitleSample.Dispose ();
				lblSubtitleSample = null;
			}

			if (lblTitleSample != null) {
				lblTitleSample.Dispose ();
				lblTitleSample = null;
			}

			if (lbSubtitle != null) {
				lbSubtitle.Dispose ();
				lbSubtitle = null;
			}

			if (lbTitle != null) {
				lbTitle.Dispose ();
				lbTitle = null;
			}

			if (viewSample != null) {
				viewSample.Dispose ();
				viewSample = null;
			}

			if (viewSampleHeithConstraint != null) {
				viewSampleHeithConstraint.Dispose ();
				viewSampleHeithConstraint = null;
			}

			if (viewSampleTopConstraint != null) {
				viewSampleTopConstraint.Dispose ();
				viewSampleTopConstraint = null;
			}
		}
	}
}
