// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Profile.Cells
{
	[Register ("LanguageSpeakCell")]
	partial class LanguageSpeakCell
	{
		[Outlet]
		UIKit.UIImageView imgLanguageSpeak { get; set; }

		[Outlet]
		UIKit.UIImageView imgYes { get; set; }

		[Outlet]
		UIKit.UILabel lblLanguageSpeak { get; set; }

		[Outlet]
		UIKit.UILabel lblLever { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint lblLeverHeightConstrait { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint lblLeverTopContraint { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgLanguageSpeak != null) {
				imgLanguageSpeak.Dispose ();
				imgLanguageSpeak = null;
			}

			if (imgYes != null) {
				imgYes.Dispose ();
				imgYes = null;
			}

			if (lblLanguageSpeak != null) {
				lblLanguageSpeak.Dispose ();
				lblLanguageSpeak = null;
			}

			if (lblLever != null) {
				lblLever.Dispose ();
				lblLever = null;
			}

			if (lblLeverHeightConstrait != null) {
				lblLeverHeightConstrait.Dispose ();
				lblLeverHeightConstrait = null;
			}

			if (lblLeverTopContraint != null) {
				lblLeverTopContraint.Dispose ();
				lblLeverTopContraint = null;
			}
		}
	}
}
