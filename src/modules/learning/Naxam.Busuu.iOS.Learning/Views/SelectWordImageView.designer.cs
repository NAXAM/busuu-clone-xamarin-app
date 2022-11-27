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
    [Register ("SelectWordImageView")]
    partial class SelectWordImageView
    {
        [Outlet]
        UIKit.UIButton btnAudio { get; set; }


        [Outlet]
        UIKit.UILabel lblQuestion { get; set; }


        [Outlet]
        UIKit.UIView ViewAnswers { get; set; }


        [Action ("btnAudio_Click:")]
        partial void btnAudio_Click (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnAudio != null) {
                btnAudio.Dispose ();
                btnAudio = null;
            }

            if (lblQuestion != null) {
                lblQuestion.Dispose ();
                lblQuestion = null;
            }

            if (ViewAnswers != null) {
                ViewAnswers.Dispose ();
                ViewAnswers = null;
            }
        }
    }
}