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
    [Register ("TrueFalseHearQuestionView")]
    partial class TrueFalseHearQuestionView
    {
        [Outlet]
        UIKit.UIButton btnFalse { get; set; }


        [Outlet]
        UIKit.UIButton btnPlayPause { get; set; }


        [Outlet]
        UIKit.UIButton btnTrue { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint btnTrueBottomConstraint { get; set; }


        [Outlet]
        UIKit.UIImageView imgEx { get; set; }


        [Action ("btnFalse_TouchUpInside:")]
        partial void btnFalse_TouchUpInside (Foundation.NSObject sender);


        [Action ("btnPlayPause_TouchUpInside:")]
        partial void btnPlayPause_TouchUpInside (Foundation.NSObject sender);


        [Action ("btnTrue_TouchUpInside:")]
        partial void btnTrue_TouchUpInside (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnFalse != null) {
                btnFalse.Dispose ();
                btnFalse = null;
            }

            if (btnPlayPause != null) {
                btnPlayPause.Dispose ();
                btnPlayPause = null;
            }

            if (btnTrue != null) {
                btnTrue.Dispose ();
                btnTrue = null;
            }

            if (btnTrueBottomConstraint != null) {
                btnTrueBottomConstraint.Dispose ();
                btnTrueBottomConstraint = null;
            }

            if (imgEx != null) {
                imgEx.Dispose ();
                imgEx = null;
            }
        }
    }
}