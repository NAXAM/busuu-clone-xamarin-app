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
    [Register ("SettingCountryView")]
    partial class SettingCountryView
    {
        [Outlet]
        UIKit.UIButton btnCancel { get; set; }

        [Outlet]
        UIKit.UIButton btnDone { get; set; }

        [Outlet]
        UIKit.UITableView CountryTableView { get; set; }

        [Outlet]
        UIKit.UIView viewAppBar { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (CountryTableView != null) {
                CountryTableView.Dispose ();
                CountryTableView = null;
            }

            if (viewAppBar != null) {
                viewAppBar.Dispose ();
                viewAppBar = null;
            }

            if (btnCancel != null) {
                btnCancel.Dispose ();
                btnCancel = null;
            }

            if (btnDone != null) {
                btnDone.Dispose ();
                btnDone = null;
            }
        }
    }
}
