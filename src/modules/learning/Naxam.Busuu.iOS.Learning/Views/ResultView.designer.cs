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
    [Register ("ResultView")]
    partial class ResultView
    {
        [Outlet]
        UIKit.UIButton btnContinue { get; set; }


        [Outlet]
        UIKit.UILabel lblPoint { get; set; }


        [Outlet]
        UIKit.UILabel lblPointMax { get; set; }


        [Outlet]
        UIKit.UILabel lblPointMin { get; set; }


        [Outlet]
        UIKit.UIView viewPoint { get; set; }


        [Action ("btnContinue_TouchUpInside:")]
        partial void btnContinue_TouchUpInside (Foundation.NSObject sender);


        [Action ("btnTryAgain_TouchUpInside:")]
        partial void btnTryAgain_TouchUpInside (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnContinue != null) {
                btnContinue.Dispose ();
                btnContinue = null;
            }

            if (lblPoint != null) {
                lblPoint.Dispose ();
                lblPoint = null;
            }

            if (lblPointMax != null) {
                lblPointMax.Dispose ();
                lblPointMax = null;
            }

            if (lblPointMin != null) {
                lblPointMin.Dispose ();
                lblPointMin = null;
            }

            if (viewPoint != null) {
                viewPoint.Dispose ();
                viewPoint = null;
            }
        }
    }
}