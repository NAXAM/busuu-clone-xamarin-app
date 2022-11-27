// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Core.Views
{
    [Register ("BuyPremiumCell")]
    partial class BuyPremiumCell
    {
        [Outlet]
        UIKit.UIButton btnGo { get; set; }


        [Outlet]
        UIKit.UIView contentView { get; set; }


        [Outlet]
        UIKit.UILabel lbPR { get; set; }


        [Outlet]
        UIKit.UIView viewRipple { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnGo != null) {
                btnGo.Dispose ();
                btnGo = null;
            }

            if (contentView != null) {
                contentView.Dispose ();
                contentView = null;
            }

            if (lbPR != null) {
                lbPR.Dispose ();
                lbPR = null;
            }

            if (viewRipple != null) {
                viewRipple.Dispose ();
                viewRipple = null;
            }
        }
    }
}