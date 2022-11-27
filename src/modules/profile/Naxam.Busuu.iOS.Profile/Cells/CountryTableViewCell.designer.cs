// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Profile.Cells
{
    [Register ("CountryTableViewCell")]
    partial class CountryTableViewCell
    {
        [Outlet]
        UIKit.UILabel lblCountry { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (lblCountry != null) {
                lblCountry.Dispose ();
                lblCountry = null;
            }
        }
    }
}
