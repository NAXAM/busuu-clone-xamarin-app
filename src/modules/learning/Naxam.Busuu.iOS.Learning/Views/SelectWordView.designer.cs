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

namespace Naxam.Busuu.iOS.Learning.Views
{
    [Register ("SelectWordView")]
    partial class SelectWordView
    {
        [Outlet]
        UIKit.UIButton btnPlayPause { get; set; }


        [Outlet]
        UIKit.UIView ContentView { get; set; }


        [Outlet]
        UIKit.UIImageView ImgImage { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint ImgImageHeightConstraint { get; set; }


        [Outlet]
        UIKit.UILabel LbQuestion { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint LbQuestionTopConstraint { get; set; }


        [Outlet]
        UIKit.UIView ViewAnswers { get; set; }


        [Action ("btnPlayPause_TouchUpInside:")]
        partial void btnPlayPause_TouchUpInside (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnPlayPause != null) {
                btnPlayPause.Dispose ();
                btnPlayPause = null;
            }

            if (ContentView != null) {
                ContentView.Dispose ();
                ContentView = null;
            }

            if (ImgImage != null) {
                ImgImage.Dispose ();
                ImgImage = null;
            }

            if (ImgImageHeightConstraint != null) {
                ImgImageHeightConstraint.Dispose ();
                ImgImageHeightConstraint = null;
            }

            if (LbQuestion != null) {
                LbQuestion.Dispose ();
                LbQuestion = null;
            }

            if (LbQuestionTopConstraint != null) {
                LbQuestionTopConstraint.Dispose ();
                LbQuestionTopConstraint = null;
            }

            if (ViewAnswers != null) {
                ViewAnswers.Dispose ();
                ViewAnswers = null;
            }
        }
    }
}