// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Learning.Views
{
    [Register ("TipView")]
    partial class TipView
    {
        [Outlet]
        UIKit.UIImageView imgTip { get; set; }


        [Outlet]
        UIKit.UIView ViewTip { get; set; }


        [Action ("btnOk_TouchUpInside:")]
        partial void btnOk_TouchUpInside (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (imgTip != null) {
                imgTip.Dispose ();
                imgTip = null;
            }

            if (ViewTip != null) {
                ViewTip.Dispose ();
                ViewTip = null;
            }
        }
    }
}