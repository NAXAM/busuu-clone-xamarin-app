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
    [Register ("ExerciseSpeakView")]
    partial class ExerciseSpeakView
    {
        [Outlet]
        UIKit.UIButton btnPlayPause { get; set; }


        [Outlet]
        UIKit.UIButton btnSay { get; set; }


        [Outlet]
        UIKit.UIImageView imgImage { get; set; }


        [Outlet]
        UIKit.UILabel iNotSpeak { get; set; }


        [Outlet]
        UIKit.UILabel lblListen { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint viewBossBottomConstraint { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint viewBtnSayHeightConstraint { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint viewBtnSayWidthConstraint { get; set; }


        [Outlet]
        UIKit.UIView viewForBtnSay { get; set; }


        [Action ("btnNotSpeak_Click:")]
        partial void btnNotSpeak_Click (Foundation.NSObject sender);


        [Action ("btnPlayPause_TouchUpInside:")]
        partial void btnPlayPause_TouchUpInside (Foundation.NSObject sender);


        [Action ("btnSay_TouchUpInside:")]
        partial void btnSay_TouchUpInside (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnPlayPause != null) {
                btnPlayPause.Dispose ();
                btnPlayPause = null;
            }

            if (btnSay != null) {
                btnSay.Dispose ();
                btnSay = null;
            }

            if (imgImage != null) {
                imgImage.Dispose ();
                imgImage = null;
            }

            if (iNotSpeak != null) {
                iNotSpeak.Dispose ();
                iNotSpeak = null;
            }

            if (lblListen != null) {
                lblListen.Dispose ();
                lblListen = null;
            }

            if (viewBossBottomConstraint != null) {
                viewBossBottomConstraint.Dispose ();
                viewBossBottomConstraint = null;
            }

            if (viewBtnSayHeightConstraint != null) {
                viewBtnSayHeightConstraint.Dispose ();
                viewBtnSayHeightConstraint = null;
            }

            if (viewBtnSayWidthConstraint != null) {
                viewBtnSayWidthConstraint.Dispose ();
                viewBtnSayWidthConstraint = null;
            }

            if (viewForBtnSay != null) {
                viewForBtnSay.Dispose ();
                viewForBtnSay = null;
            }
        }
    }
}