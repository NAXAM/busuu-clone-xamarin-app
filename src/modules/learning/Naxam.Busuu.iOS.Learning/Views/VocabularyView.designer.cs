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
    [Register ("VocabularyView")]
    partial class VocabularyView
    {
        [Outlet]
        UIKit.UIButton btnNext { get; set; }


        [Outlet]
        UIKit.UIButton btnStart { get; set; }


        [Outlet]
        UIKit.UILabel lbProcess { get; set; }


        [Outlet]
        UIKit.UIView viewEnd { get; set; }


        [Outlet]
        UIKit.UIView ViewExercise { get; set; }


        [Outlet]
        UIKit.UIView viewOfTitle { get; set; }


        [Outlet]
        UIKit.UIView viewProcess { get; set; }


        [Outlet]
        UIKit.UIView viewProcessValue { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint viewProcessValueWidthConstraint { get; set; }


        [Outlet]
        UIKit.UIView viewStart { get; set; }


        [Action ("btnNext_Click:")]
        partial void btnNext_Click (Foundation.NSObject sender);


        [Action ("btnStart_Click:")]
        partial void btnStart_Click (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnNext != null) {
                btnNext.Dispose ();
                btnNext = null;
            }

            if (btnStart != null) {
                btnStart.Dispose ();
                btnStart = null;
            }

            if (lbProcess != null) {
                lbProcess.Dispose ();
                lbProcess = null;
            }

            if (viewEnd != null) {
                viewEnd.Dispose ();
                viewEnd = null;
            }

            if (ViewExercise != null) {
                ViewExercise.Dispose ();
                ViewExercise = null;
            }

            if (viewOfTitle != null) {
                viewOfTitle.Dispose ();
                viewOfTitle = null;
            }

            if (viewProcess != null) {
                viewProcess.Dispose ();
                viewProcess = null;
            }

            if (viewProcessValue != null) {
                viewProcessValue.Dispose ();
                viewProcessValue = null;
            }

            if (viewProcessValueWidthConstraint != null) {
                viewProcessValueWidthConstraint.Dispose ();
                viewProcessValueWidthConstraint = null;
            }

            if (viewStart != null) {
                viewStart.Dispose ();
                viewStart = null;
            }
        }
    }
}