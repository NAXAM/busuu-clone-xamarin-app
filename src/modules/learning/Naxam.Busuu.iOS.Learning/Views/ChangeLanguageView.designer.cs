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
    [Register ("ChangeLanguageView")]
    partial class ChangeLanguageView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UICollectionView ChangeLanguageCollectionView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ChangeLanguageCollectionView != null) {
                ChangeLanguageCollectionView.Dispose ();
                ChangeLanguageCollectionView = null;
            }
        }
    }
}