// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Profile.Views
{
    [Register ("SettingInterfaceLanguageView")]
    partial class SettingInterfaceLanguageView
    {
        [Outlet]
        UIKit.UITableView InterfaceLanguageTableView { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (InterfaceLanguageTableView != null) {
                InterfaceLanguageTableView.Dispose ();
                InterfaceLanguageTableView = null;
            }
        }
    }
}
