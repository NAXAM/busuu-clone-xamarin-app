// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Start.Cells
{
    [Register ("LanguageTableViewCell")]
    partial class LanguageTableViewCell
    {
        [Outlet]
        UIKit.UIImageView imgLanguage { get; set; }

        [Outlet]
        UIKit.UILabel nameLanguage { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (imgLanguage != null) {
                imgLanguage.Dispose ();
                imgLanguage = null;
            }

            if (nameLanguage != null) {
                nameLanguage.Dispose ();
                nameLanguage = null;
            }
        }
    }
}
