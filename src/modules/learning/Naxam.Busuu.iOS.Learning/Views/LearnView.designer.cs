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
    [Register ("LearnView")]
    partial class LearnView
    {
        [Outlet]
        UIKit.UITableView LessonTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LessonTableView != null) {
                LessonTableView.Dispose ();
                LessonTableView = null;
            }
        }
    }
}