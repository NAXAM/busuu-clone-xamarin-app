// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Social.Views
{
    [Register ("ReplyView")]
    partial class ReplyView
    {
        [Outlet]
        UIKit.UIActivityIndicatorView activityControl { get; set; }

        [Outlet]
        UIKit.UIButton btnBack { get; set; }

        [Outlet]
        UIKit.UIButton btnReply { get; set; }

        [Outlet]
        UIKit.UILabel textSwipe { get; set; }

        [Outlet]
        UIKit.NSLayoutConstraint textSwipeCenterXConstraint { get; set; }

        [Outlet]
        UIKit.UITextView textViewReply { get; set; }

        [Outlet]
        UIKit.UIView viewPhu { get; set; }

        [Outlet]
        UIKit.UIView ViewReplyBar { get; set; }

        [Outlet]
        UIKit.UIView viewReplySpeak { get; set; }

        [Outlet]
        UIKit.NSLayoutConstraint viewReplySpeakHeigthConstraint { get; set; }

        [Outlet]
        UIKit.NSLayoutConstraint viewReplyWidthConstraint { get; set; }

        [Outlet]
        UIKit.UIView viewSendActivity { get; set; }

        [Outlet]
        UIKit.NSLayoutConstraint viewSliderWidthConstraint { get; set; }

        [Action ("btnReply_TouchCancel:")]
        partial void btnReply_TouchCancel (Foundation.NSObject sender);

        [Action ("btnReply_TouchDown:")]
        partial void btnReply_TouchDown (Foundation.NSObject sender);

        [Action ("btnReply_TouchUpInside:")]
        partial void btnReply_TouchUpInside (Foundation.NSObject sender);

        [Action ("btnReply_TouchUpOutside:")]
        partial void btnReply_TouchUpOutside (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (btnBack != null) {
                btnBack.Dispose ();
                btnBack = null;
            }

            if (btnReply != null) {
                btnReply.Dispose ();
                btnReply = null;
            }
               
            if (textSwipe != null) {
                textSwipe.Dispose ();
                textSwipe = null;
            }

            if (textSwipeCenterXConstraint != null) {
                textSwipeCenterXConstraint.Dispose ();
                textSwipeCenterXConstraint = null;
            }

            if (textViewReply != null) {
                textViewReply.Dispose ();
                textViewReply = null;
            }

            if (viewPhu != null) {
                viewPhu.Dispose ();
                viewPhu = null;
            }

            if (ViewReplyBar != null) {
                ViewReplyBar.Dispose ();
                ViewReplyBar = null;
            }

            if (viewReplySpeak != null) {
                viewReplySpeak.Dispose ();
                viewReplySpeak = null;
            }

            if (viewReplySpeakHeigthConstraint != null) {
                viewReplySpeakHeigthConstraint.Dispose ();
                viewReplySpeakHeigthConstraint = null;
            }

            if (viewSliderWidthConstraint != null) {
                viewSliderWidthConstraint.Dispose ();
                viewSliderWidthConstraint = null;
            }

            if (viewReplyWidthConstraint != null) {
                viewReplyWidthConstraint.Dispose ();
                viewReplyWidthConstraint = null;
            }

            if (viewSendActivity != null) {
                viewSendActivity.Dispose ();
                viewSendActivity = null;
            }

            if (activityControl != null) {
                activityControl.Dispose ();
                activityControl = null;
            }
        }
    }
}
