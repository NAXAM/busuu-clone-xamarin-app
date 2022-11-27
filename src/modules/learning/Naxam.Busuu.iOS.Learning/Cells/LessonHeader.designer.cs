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
    [Register ("LessonHeader")]
    partial class LessonHeader
    {
        [Outlet]
        UIKit.UILabel lbHeader { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lbHeader != null) {
                lbHeader.Dispose ();
                lbHeader = null;
            }
        }
    }
}