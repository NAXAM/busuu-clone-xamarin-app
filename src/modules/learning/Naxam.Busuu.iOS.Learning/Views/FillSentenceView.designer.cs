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
    [Register ("FillSentenceView")]
    partial class FillSentenceView
    {
        [Outlet]
        UIKit.UIImageView imgFillSentence { get; set; }


        [Outlet]
        UIKit.UILabel lblQuestion { get; set; }


        [Outlet]
        UIKit.UILabel lblTitle { get; set; }


        [Outlet]
        UIKit.UIView ViewAnswers { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint viewAnswersWidthContraint { get; set; }

        void ReleaseDesignerOutlets ()
        {
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

            if (ViewAnswers != null) {
                ViewAnswers.Dispose ();
                ViewAnswers = null;
            }

            if (viewAnswersWidthContraint != null) {
                viewAnswersWidthContraint.Dispose ();
                viewAnswersWidthContraint = null;
            }
        }
    }
}