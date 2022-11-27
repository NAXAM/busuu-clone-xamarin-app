using System;
using Foundation;
using MaterialControls;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Start.ViewModels;
using FBKVOControllerNS;
using UIKit;
using System.Text.RegularExpressions;

namespace Naxam.Busuu.iOS.Start.Views
{
    [MvxFromStoryboard(StoryboardName = "Start")]
	public partial class ForgotPasswordView : MvxViewController<ForgotPasswordViewModel>
	{
		MDTextField fieldEmailPhone;
        FBKVOController _KVOController;

		public ForgotPasswordView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			ViewShadow.Layer.ShadowRadius = 2;
			ViewShadow.Layer.ShadowOpacity = 0.25f;
			ViewShadow.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 2);

            btnSendLink.Layer.CornerRadius = btnSendLink.Frame.Height / 2;

			fieldEmailPhone = new MDTextField();
			fieldEmailPhone.TextColor = UIColor.FromRGB(172, 180, 186);
			fieldEmailPhone.NormalColor = UIColor.FromRGB(172, 180, 186);
			fieldEmailPhone.HighlightColor = UIColor.FromRGB(57, 169, 246);
			fieldEmailPhone.LabelsFont = UIFont.SystemFontOfSize(14);
			fieldEmailPhone.InputTextFont = UIFont.SystemFontOfSize(14);
			fieldEmailPhone.ShouldBeginEditing = FieldEmailPhone_ShouldBeginEditing;
			fieldEmailPhone.ShouldEndEditing = FieldEmailPhone_ShouldEndEditing;
			fieldEmailPhone.Layer.Frame = new CoreGraphics.CGRect(0, 0, viewEmailPhone.Bounds.Width, viewEmailPhone.Bounds.Height);
			
            viewEmailPhone.InsertSubview(fieldEmailPhone, 0);

            _KVOController = new FBKVOController(this);
            _KVOController.Observe(btnSendLink, "enabled", NSKeyValueObservingOptions.Initial | NSKeyValueObservingOptions.New, UpdateBackgroundForItem);

            var setBind = this.CreateBindingSet<ForgotPasswordView, ForgotPasswordViewModel>();
            setBind.Bind(fieldEmailPhone).For(vm => vm.Text).To(vm => vm.EmailPhone);
            setBind.Bind(btnSendLink).For(vm => vm.Enabled).To(vm => vm.IsEnableLoginSend);
			setBind.Apply();
        }

		void UpdateBackgroundForItem(NSObject arg0, NSObject arg1, NSDictionary<NSString, NSObject> arg2)
		{
            if (arg1 is UIButton item )
			{
                if (item.Enabled)
                {
                    btnSendLink.BackgroundColor = UIColor.FromRGB(57, 169, 246);
                    btnSendLink.Layer.ShadowRadius = 1.5f;
                    btnSendLink.Layer.ShadowOpacity = 0.25f;
                    btnSendLink.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 1.5f);
                }
                else
                {
                    btnSendLink.BackgroundColor = UIColor.FromRGB(214, 222, 230);
                    btnSendLink.Layer.ShadowRadius = 0;
                    btnSendLink.Layer.ShadowOpacity = 0;
                    btnSendLink.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 0);
				}
			}
		}

		bool FieldEmailPhone_ShouldBeginEditing(MDTextField textField)
		{
            viewLineEmailPhone.BackgroundColor = UIColor.FromRGB(57, 169, 246);
			fieldEmailPhone.TextColor = UIColor.Black;
			if (fieldEmailPhone.Text == "Email address or phone number")
			{
				fieldEmailPhone.Text = "";
			}

			return true;
		}

		bool FieldEmailPhone_ShouldEndEditing(MDTextField textField)
		{
            viewLineEmailPhone.BackgroundColor = UIColor.Clear;
			if (fieldEmailPhone.Text == "" || fieldEmailPhone.Text == "Email address or phone number")
			{
				fieldEmailPhone.TextColor = UIColor.FromRGB(172, 180, 186);
				fieldEmailPhone.NormalColor = UIColor.FromRGB(172, 180, 186);
				fieldEmailPhone.Text = "Email address or phone number";
			}
			else
			{
				fieldEmailPhone.TextColor = UIColor.Black;

				if (CheckEmailPhone())
				{
					fieldEmailPhone.NormalColor = UIColor.FromRGB(172, 180, 186);
				}
				else
				{
					fieldEmailPhone.NormalColor = UIColor.FromRGB(238, 93, 78);
				}
			}

			return true;
		}

        partial void btnSendLink_TochUpInside(NSObject sender)
        {
            var alert = UIAlertController.Create("Demo Send Successful", fieldEmailPhone.Text, UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
			PresentViewController(alert, true, null);
        }

		bool CheckEmailPhone()
		{
			if (fieldEmailPhone.Text != "Email address or phone number")
			{
				Regex regex = new Regex("^[a-zA-Z0-9-_\\.]+@[a-z0-9]+\\.[a-z]{2,4}$");
				bool checkMail = regex.IsMatch(fieldEmailPhone.Text);

				Regex regexP = new Regex("^+?[0-9]{9,13}$");
				bool checkPhone = regexP.IsMatch(fieldEmailPhone.Text);

				return (checkMail || checkPhone);
			}

			return false;
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (_KVOController != null)
			{
				_KVOController.UnobserveAll();
				_KVOController.Dispose();
				_KVOController = null;
			}
		}
	}
}
