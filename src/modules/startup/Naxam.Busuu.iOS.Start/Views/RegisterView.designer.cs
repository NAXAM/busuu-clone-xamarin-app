// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Start.Views
{
	[Register ("RegisterView")]
	partial class RegisterView
	{
		[Outlet]
		UIKit.UIButton btnChooseCountry { get; set; }

		[Outlet]
		UIKit.UIButton btnFacebook { get; set; }

		[Outlet]
		UIKit.UIButton btnGoogle { get; set; }

		[Outlet]
		UIKit.UIButton btnSignUp { get; set; }

		[Outlet]
		UIKit.UIButton btnUseEmailPhone { get; set; }

		[Outlet]
		UIKit.UILabel lblChooseCountry { get; set; }

		[Outlet]
		UIKit.UIView viewbtnFacebook { get; set; }

		[Outlet]
		UIKit.UIView viewbtnGoogle { get; set; }

		[Outlet]
		UIKit.UIView viewConnect { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint viewConnectTopConstraint { get; set; }

		[Outlet]
		UIKit.UIView ViewEmail { get; set; }

		[Outlet]
		UIKit.UIView viewLineEmail { get; set; }

		[Outlet]
		UIKit.UIView viewLinePass { get; set; }

		[Outlet]
		UIKit.UIView viewLinePhone { get; set; }

		[Outlet]
		UIKit.UIView viewLineUser { get; set; }

		[Outlet]
		UIKit.UIView ViewPassword { get; set; }

		[Outlet]
		UIKit.UIView ViewShadow { get; set; }

		[Outlet]
		UIKit.UIView ViewUsername { get; set; }

		[Action ("btnUseEmailPhone_TouchUpInside:")]
		partial void btnUseEmailPhone_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnChooseCountry != null) {
				btnChooseCountry.Dispose ();
				btnChooseCountry = null;
			}

			if (btnFacebook != null) {
				btnFacebook.Dispose ();
				btnFacebook = null;
			}

			if (btnGoogle != null) {
				btnGoogle.Dispose ();
				btnGoogle = null;
			}

			if (btnSignUp != null) {
				btnSignUp.Dispose ();
				btnSignUp = null;
			}

			if (btnUseEmailPhone != null) {
				btnUseEmailPhone.Dispose ();
				btnUseEmailPhone = null;
			}

			if (viewbtnFacebook != null) {
				viewbtnFacebook.Dispose ();
				viewbtnFacebook = null;
			}

			if (viewbtnGoogle != null) {
				viewbtnGoogle.Dispose ();
				viewbtnGoogle = null;
			}

			if (lblChooseCountry != null) {
				lblChooseCountry.Dispose ();
				lblChooseCountry = null;
			}

			if (viewConnect != null) {
				viewConnect.Dispose ();
				viewConnect = null;
			}

			if (viewConnectTopConstraint != null) {
				viewConnectTopConstraint.Dispose ();
				viewConnectTopConstraint = null;
			}

			if (ViewEmail != null) {
				ViewEmail.Dispose ();
				ViewEmail = null;
			}

			if (viewLineEmail != null) {
				viewLineEmail.Dispose ();
				viewLineEmail = null;
			}

			if (viewLinePass != null) {
				viewLinePass.Dispose ();
				viewLinePass = null;
			}

			if (viewLinePhone != null) {
				viewLinePhone.Dispose ();
				viewLinePhone = null;
			}

			if (viewLineUser != null) {
				viewLineUser.Dispose ();
				viewLineUser = null;
			}

			if (ViewPassword != null) {
				ViewPassword.Dispose ();
				ViewPassword = null;
			}

			if (ViewShadow != null) {
				ViewShadow.Dispose ();
				ViewShadow = null;
			}

			if (ViewUsername != null) {
				ViewUsername.Dispose ();
				ViewUsername = null;
			}
		}
	}
}
