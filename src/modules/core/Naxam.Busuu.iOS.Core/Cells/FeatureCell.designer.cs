// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Core.Cells
{
	[Register ("FeatureCell")]
	partial class FeatureCell
	{
		[Outlet]
		UIKit.UIImageView imgFeature { get; set; }

		[Outlet]
		UIKit.UILabel lbFeature { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgFeature != null) {
				imgFeature.Dispose ();
				imgFeature = null;
			}

			if (lbFeature != null) {
				lbFeature.Dispose ();
				lbFeature = null;
			}
		}
	}
}
