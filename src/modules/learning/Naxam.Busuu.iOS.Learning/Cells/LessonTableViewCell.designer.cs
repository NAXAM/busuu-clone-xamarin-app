// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Learning.Cells
{
    [Register ("LessonTableViewCell")]
    partial class LessonTableViewCell
    {
        [Outlet]
        UIKit.UIButton btnDownload { get; set; }


        [Outlet]
        UIKit.UIImageView imgLesson { get; set; }


        [Outlet]
        UIKit.UILabel lbNumber { get; set; }


        [Outlet]
        UIKit.UILabel lbTitle { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint numberLblHeightConstraint { get; set; }


        [Action ("btnDownload_TouchUpInside:")]
        partial void btnDownload_TouchUpInside (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnDownload != null) {
                btnDownload.Dispose ();
                btnDownload = null;
            }

            if (imgLesson != null) {
                imgLesson.Dispose ();
                imgLesson = null;
            }

            if (lbNumber != null) {
                lbNumber.Dispose ();
                lbNumber = null;
            }

            if (lbTitle != null) {
                lbTitle.Dispose ();
                lbTitle = null;
            }

            if (numberLblHeightConstraint != null) {
                numberLblHeightConstraint.Dispose ();
                numberLblHeightConstraint = null;
            }
        }
    }
}