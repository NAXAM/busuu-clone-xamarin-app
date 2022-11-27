// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Social
{
	[Register ("FilterView")]
	partial class FilterView
	{
		[Outlet]
		UIKit.UISwitch SwitchLanguage { get; set; }

		[Outlet]
		UIKit.UISwitch SwitchSpeak { get; set; }

		[Outlet]
		UIKit.UISwitch SwitchWrite { get; set; }

		[Outlet]
		UIKit.UIView ViewShadow { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SwitchLanguage != null) {
				SwitchLanguage.Dispose ();
				SwitchLanguage = null;
			}

			if (SwitchSpeak != null) {
				SwitchSpeak.Dispose ();
				SwitchSpeak = null;
			}

			if (SwitchWrite != null) {
				SwitchWrite.Dispose ();
				SwitchWrite = null;
			}

			if (ViewShadow != null) {
				ViewShadow.Dispose ();
				ViewShadow = null;
			}
		}
	}
}
