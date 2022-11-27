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
    [Register ("SubLessonTableViewCell")]
    partial class SubLessonTableViewCell
    {
        [Outlet]
        Naxam.Busuu.iOS.Learning.Controls.ExcercisesView exerciseView { get; set; }

        [Outlet]
        UIKit.UILabel lbTime { get; set; }

        [Outlet]
        UIKit.UILabel lbTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (exerciseView != null) {
                exerciseView.Dispose ();
                exerciseView = null;
            }

            if (lbTime != null) {
                lbTime.Dispose ();
                lbTime = null;
            }

            if (lbTitle != null) {
                lbTitle.Dispose ();
                lbTitle = null;
            }
        }
    }
}