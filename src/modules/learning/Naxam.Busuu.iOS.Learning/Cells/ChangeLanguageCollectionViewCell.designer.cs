// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Naxam.Busuu.iOS.Learning.Cells
{
    [Register ("ChangeLanguageCollectionViewCell")]
    partial class ChangeLanguageCollectionViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgLan { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbPercent { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel nameLan { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView viewOfImg { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgLan != null) {
                imgLan.Dispose ();
                imgLan = null;
            }

            if (lbPercent != null) {
                lbPercent.Dispose ();
                lbPercent = null;
            }

            if (nameLan != null) {
                nameLan.Dispose ();
                nameLan = null;
            }

            if (viewOfImg != null) {
                viewOfImg.Dispose ();
                viewOfImg = null;
            }
        }
    }
}