// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Social.Cells
{
    [Register ("CommentSocialDetailCell")]
    partial class CommentSocialDetailCell
    {
        [Outlet]
        UIKit.UIButton btnAddFriends { get; set; }

        [Outlet]
        UIKit.UIButton btnReply { get; set; }

        [Outlet]
        UIKit.UIButton btnReport { get; set; }

        [Outlet]
        UIKit.UIImageView imgBtnDislike { get; set; }

        [Outlet]
        UIKit.UIImageView imgBtnLike { get; set; }

        [Outlet]
        UIKit.UIImageView imgUserAvatar { get; set; }

        [Outlet]
        UIKit.UILabel lblComment { get; set; }

        [Outlet]
        UIKit.UILabel lblCountry { get; set; }

        [Outlet]
        UIKit.UILabel lblDisLike { get; set; }

        [Outlet]
        UIKit.UILabel lblLike { get; set; }

        [Outlet]
        UIKit.UILabel lblReply { get; set; }

        [Outlet]
        UIKit.UILabel lblTimePublic { get; set; }

        [Outlet]
        UIKit.UILabel lblUserName { get; set; }

        [Outlet]
        UIKit.UIView viewBackground { get; set; }

        [Outlet]
        UIKit.UIView viewBtnDislike { get; set; }

        [Outlet]
        UIKit.UIView viewBtnLike { get; set; }

        [Action ("btnAddFriends_TouchUpInside:")]
        partial void btnAddFriends_TouchUpInside (Foundation.NSObject sender);

        [Action ("btnDislike_TouchUpInside:")]
        partial void btnDislike_TouchUpInside (Foundation.NSObject sender);

        [Action ("btnLike_TouchUpSide:")]
        partial void btnLike_TouchUpSide (Foundation.NSObject sender);

        [Action ("btnReply_TouchUpSide:")]
        partial void btnReply_TouchUpSide (Foundation.NSObject sender);

        [Action ("btnReport_TouchUpInside:")]
        partial void btnReport_TouchUpInside (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (btnAddFriends != null) {
                btnAddFriends.Dispose ();
                btnAddFriends = null;
            }

            if (btnReply != null) {
                btnReply.Dispose ();
                btnReply = null;
            }

            if (btnReport != null) {
                btnReport.Dispose ();
                btnReport = null;
            }

            if (imgBtnDislike != null) {
                imgBtnDislike.Dispose ();
                imgBtnDislike = null;
            }

            if (imgBtnLike != null) {
                imgBtnLike.Dispose ();
                imgBtnLike = null;
            }

            if (imgUserAvatar != null) {
                imgUserAvatar.Dispose ();
                imgUserAvatar = null;
            }

            if (lblComment != null) {
                lblComment.Dispose ();
                lblComment = null;
            }

            if (lblCountry != null) {
                lblCountry.Dispose ();
                lblCountry = null;
            }

            if (lblDisLike != null) {
                lblDisLike.Dispose ();
                lblDisLike = null;
            }

            if (lblLike != null) {
                lblLike.Dispose ();
                lblLike = null;
            }

            if (lblReply != null) {
                lblReply.Dispose ();
                lblReply = null;
            }

            if (lblTimePublic != null) {
                lblTimePublic.Dispose ();
                lblTimePublic = null;
            }

            if (lblUserName != null) {
                lblUserName.Dispose ();
                lblUserName = null;
            }

            if (viewBackground != null) {
                viewBackground.Dispose ();
                viewBackground = null;
            }

            if (viewBtnDislike != null) {
                viewBtnDislike.Dispose ();
                viewBtnDislike = null;
            }

            if (viewBtnLike != null) {
                viewBtnLike.Dispose ();
                viewBtnLike = null;
            }
        }
    }
}
