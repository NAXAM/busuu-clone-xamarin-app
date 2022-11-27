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
    [Register ("CompleteSentenceView")]
    partial class CompleteSentenceView
    {
        [Outlet]
        UIKit.UIButton btnContinue { get; set; }


        [Outlet]
        UIKit.UIButton btnPlayPause { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint compeleteViewBottomConstraint { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgFillSentence { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblQuestion { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTitle { get; set; }


        [Action ("btnContinue_Click:")]
        partial void btnContinue_Click (Foundation.NSObject sender);


        [Action ("btnPlayPause_TouchUpInside:")]
        partial void btnPlayPause_TouchUpInside (Foundation.NSObject sender);


        [Action ("btnQuestion_Click:")]
        partial void btnQuestion_Click (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnContinue != null) {
                btnContinue.Dispose ();
                btnContinue = null;
            }

            if (btnPlayPause != null) {
                btnPlayPause.Dispose ();
                btnPlayPause = null;
            }

            if (compeleteViewBottomConstraint != null) {
                compeleteViewBottomConstraint.Dispose ();
                compeleteViewBottomConstraint = null;
            }

            if (imgFillSentence != null) {
                imgFillSentence.Dispose ();
                imgFillSentence = null;
            }

            if (lblQuestion != null) {
                lblQuestion.Dispose ();
                lblQuestion = null;
            }

            if (lblTitle != null) {
                lblTitle.Dispose ();
                lblTitle = null;
            }
        }
    }
}