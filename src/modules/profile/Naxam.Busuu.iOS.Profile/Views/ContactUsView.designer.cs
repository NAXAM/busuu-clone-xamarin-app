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
    [Register ("ContactUsView")]
    partial class ContactUsView
    {
        [Outlet]
        UIKit.UIButton btnSend { get; set; }

        [Outlet]
        UIKit.UITextField textFieldEmail { get; set; }

        [Outlet]
        UIKit.UITextField textFieldSubject { get; set; }

        [Outlet]
        UIKit.UITextView textViewDescription { get; set; }

        [Action ("btnSend_TouchUpInside:")]
        partial void btnSend_TouchUpInside (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (btnSend != null) {
                btnSend.Dispose ();
                btnSend = null;
            }

            if (textViewDescription != null) {
                textViewDescription.Dispose ();
                textViewDescription = null;
            }

            if (textFieldSubject != null) {
                textFieldSubject.Dispose ();
                textFieldSubject = null;
            }

            if (textFieldEmail != null) {
                textFieldEmail.Dispose ();
                textFieldEmail = null;
            }
        }
    }
}
