using System;
using System.Text.RegularExpressions;
using FBKVOControllerNS;
using Foundation;
using MaterialControls;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Start.ViewModels;
using ObjCRuntime;
using UIKit;

namespace Naxam.Busuu.iOS.Start.Views
{
	[MvxFromStoryboard(StoryboardName = "Start")]
	public partial class RegisterView : MvxViewController<RegisterViewModel>
	{
		MDTextField fieldEmailPhone;
		MDTextField fieldPass;
        MDTextField fieldUserName;
		FBKVOController _KVOController;
		bool isEAnimating;
		bool isUAnimating;
		bool isPAnimating;
        bool isUse; // false = Email, true = Phone

		public RegisterView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			this.NavigationController.NavigationBarHidden = false;

			ViewShadow.Layer.ShadowRadius = 2;
			ViewShadow.Layer.ShadowOpacity = 0.25f;
			ViewShadow.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 2);

			fieldEmailPhone = new MDTextField();
			fieldEmailPhone.TextColor = UIColor.FromRGB(172, 180, 186);
			fieldEmailPhone.NormalColor = UIColor.FromRGB(172, 180, 186);
			fieldEmailPhone.HighlightColor = UIColor.FromRGB(57, 169, 246);
			fieldEmailPhone.LabelsFont = UIFont.SystemFontOfSize(14);
			fieldEmailPhone.InputTextFont = UIFont.SystemFontOfSize(14);
			fieldEmailPhone.ShouldBeginEditing = FieldEmailPhone_ShouldBeginEditing;
			fieldEmailPhone.ShouldEndEditing = FieldEmailPhone_ShouldEndEditing;
			fieldEmailPhone.Layer.Frame = new CoreGraphics.CGRect(0, 0, ViewEmail.Bounds.Width, ViewEmail.Bounds.Height);

			fieldUserName = new MDTextField();
			fieldUserName.TextColor = UIColor.FromRGB(172, 180, 186);
			fieldUserName.NormalColor = UIColor.FromRGB(172, 180, 186);
			fieldUserName.HighlightColor = UIColor.FromRGB(57, 169, 246);
			fieldUserName.LabelsFont = UIFont.SystemFontOfSize(14);
			fieldUserName.InputTextFont = UIFont.SystemFontOfSize(14);
            fieldUserName.ShouldBeginEditing = FieldUserName_ShouldBeginEditing;
            fieldUserName.ShouldEndEditing = FieldUserName_ShouldEndEditing;
            fieldUserName.Layer.Frame = new CoreGraphics.CGRect(0, 0, ViewUsername.Bounds.Width, ViewUsername.Bounds.Height);

			fieldPass = new MDTextField();
			fieldPass.TextColor = UIColor.FromRGB(172, 180, 186);
			fieldPass.NormalColor = UIColor.FromRGB(172, 180, 186);
			fieldPass.HighlightColor = UIColor.FromRGB(57, 169, 246);
			fieldPass.LabelsFont = UIFont.SystemFontOfSize(14);
			fieldPass.InputTextFont = UIFont.SystemFontOfSize(14);
			fieldPass.ShouldBeginEditing = FieldPass_ShouldBeginEditing;
			fieldPass.ShouldEndEditing = FieldPass_ShouldEndEditing;
			fieldPass.Layer.Frame = new CoreGraphics.CGRect(0, 0, ViewPassword.Bounds.Width, ViewPassword.Bounds.Height);

            ViewEmail.InsertSubview(fieldEmailPhone, 2);
            ViewUsername.InsertSubview(fieldUserName, 0);
            ViewPassword.InsertSubview(fieldPass, 0);

			btnFacebook.ContentEdgeInsets = new UIEdgeInsets(0, 16, 0, 0);
			btnGoogle.ContentEdgeInsets = new UIEdgeInsets(0, 16, 0, 0);

			btnFacebook.Layer.CornerRadius = btnFacebook.Frame.Height / 2;
			btnGoogle.Layer.CornerRadius = btnGoogle.Frame.Height / 2;

			viewbtnFacebook.Layer.CornerRadius = viewbtnFacebook.Frame.Height / 2;
			viewbtnGoogle.Layer.CornerRadius = viewbtnGoogle.Frame.Height / 2;

            viewbtnFacebook.Layer.ShadowRadius = 1.5f;
			viewbtnFacebook.Layer.ShadowOpacity = 0.25f;
			viewbtnFacebook.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 1.5f);

            viewbtnGoogle.Layer.ShadowRadius = 1.5f;
			viewbtnGoogle.Layer.ShadowOpacity = 0.25f;
			viewbtnGoogle.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 1.5f);

			btnSignUp.Layer.CornerRadius = btnSignUp.Frame.Height / 2;

			_KVOController = new FBKVOController(this);
			_KVOController.Observe(btnSignUp, "enabled", NSKeyValueObservingOptions.Initial | NSKeyValueObservingOptions.New, UpdateBackgroundForItem);

            var setBind = this.CreateBindingSet<RegisterView, RegisterViewModel>();
			setBind.Bind(btnFacebook).To(vm => vm.LoginCommend);
			setBind.Bind(btnGoogle).To(vm => vm.LoginCommend);
			setBind.Bind(btnSignUp).To(vm => vm.LoginCommend);
            setBind.Bind(lblChooseCountry).To(vm => vm.PhoneCode);
            setBind.Bind(btnChooseCountry).To(vm => vm.PhoneCodeCommand);
            setBind.Bind(fieldEmailPhone).For(vm => vm.Text).To(vm => vm.EmailPhone);
            setBind.Bind(fieldUserName).For(vm => vm.Text).To(vm => vm.UserName);
            setBind.Bind(fieldPass).For(vm => vm.Text).To(vm => vm.Password);
            setBind.Bind(btnSignUp).For(vm => vm.Enabled).To(vm => vm.IsEnableSignBtn);		
			setBind.Apply();
        }

		void UpdateBackgroundForItem(NSObject arg0, NSObject arg1, NSDictionary<NSString, NSObject> arg2)
		{
			if (arg1 is UIButton item)
			{
				if (item.Enabled)
				{
					btnSignUp.BackgroundColor = UIColor.FromRGB(57, 169, 246);
					btnSignUp.Layer.ShadowRadius = 1.5f;
					btnSignUp.Layer.ShadowOpacity = 0.25f;
					btnSignUp.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 1.5f);
				}
				else
				{
					btnSignUp.BackgroundColor = UIColor.FromRGB(214, 222, 230);
					btnSignUp.Layer.ShadowRadius = 0;
					btnSignUp.Layer.ShadowOpacity = 0;
					btnSignUp.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 0);
				}
			}
		}

        partial void btnUseEmailPhone_TouchUpInside(NSObject sender)
        {
            if (!isUse)
            {
                isUse = true;
                fieldEmailPhone.Text = "Phone number";
                fieldEmailPhone.KeyboardType = UIKeyboardType.PhonePad;
				fieldEmailPhone.Frame = new CoreGraphics.CGRect(45, 0, ViewEmail.Bounds.Width - 48, ViewEmail.Bounds.Height);
				btnUseEmailPhone.SetTitle("USE EMAIL", UIControlState.Normal);
				btnUseEmailPhone.SetTitle("USE EMAIL", UIControlState.Highlighted);
            }
            else
            {
                isUse = false;
				fieldEmailPhone.Text = "Email";
                fieldEmailPhone.KeyboardType = UIKeyboardType.EmailAddress;
				fieldEmailPhone.Frame = new CoreGraphics.CGRect(0, 0, ViewEmail.Bounds.Width, ViewEmail.Bounds.Height);
				btnUseEmailPhone.SetTitle("USE PHONE NUMBER", UIControlState.Normal);
				btnUseEmailPhone.SetTitle("USE PHONE NUMBER", UIControlState.Highlighted);
            }
        }

		bool FieldEmailPhone_ShouldBeginEditing(MDTextField textField)
		{
			isEAnimating = true;
            if (!isUAnimating && !isPAnimating)
			{
				StartAnimation();
			}
			else
			{
                isUAnimating = isPAnimating = false;
			}

			viewLineEmail.BackgroundColor = UIColor.FromRGB(57, 169, 246);
			fieldEmailPhone.TextColor = UIColor.Black;
			if (fieldEmailPhone.Text == "Email" || fieldEmailPhone.Text == "Phone number")
			{
				fieldEmailPhone.Text = "";
			}

			return true;
		}

		bool FieldEmailPhone_ShouldEndEditing(MDTextField textField)
		{
            if (isEAnimating && !isUAnimating && !isPAnimating)
			{
				isEAnimating = false;
				StopAnimationC();
			}

            viewLineEmail.BackgroundColor = UIColor.Clear;
			if (fieldEmailPhone.Text == "" || fieldEmailPhone.Text == "Email" || fieldEmailPhone.Text == "Phone number")
			{
                viewLinePhone.BackgroundColor = UIColor.FromRGB(172, 180, 186);
				fieldEmailPhone.TextColor = UIColor.FromRGB(172, 180, 186);
				fieldEmailPhone.NormalColor = UIColor.FromRGB(172, 180, 186);
                if (!isUse)
                {
                    fieldEmailPhone.Text = "Email";
                }
                else
                {
                    fieldEmailPhone.Text = "Phone number";
                }
			}
			else
			{
				fieldEmailPhone.TextColor = UIColor.Black;
            
				if (CheckEmailPhone())
				{
					fieldEmailPhone.NormalColor = UIColor.FromRGB(172, 180, 186);
					viewLinePhone.BackgroundColor = UIColor.FromRGB(172, 180, 186);
				}
				else
				{
					fieldEmailPhone.NormalColor = UIColor.FromRGB(238, 93, 78);
					viewLinePhone.BackgroundColor = UIColor.FromRGB(238, 93, 78);
				}
			}

			return true;
		}

		bool FieldUserName_ShouldBeginEditing(MDTextField textField)
		{
			isUAnimating = true;
            if (!isEAnimating && !isPAnimating)
			{
				StartAnimation();
			}
			else
			{
                isEAnimating = isPAnimating = false;
			}

            viewLineUser.BackgroundColor = UIColor.FromRGB(57, 169, 246);
			fieldUserName.TextColor = UIColor.Black;
            if (fieldUserName.Text == "Username")
			{
                fieldUserName.Text = "";
			}

			return true;
		}

		bool FieldUserName_ShouldEndEditing(MDTextField textField)
		{
            if (isUAnimating && !isEAnimating && !isPAnimating)
			{
				isUAnimating = false;
				StopAnimationC();
			}

            viewLineUser.BackgroundColor = UIColor.Clear;
			if (fieldUserName.Text == "" || fieldUserName.Text == "Username")
			{
                fieldUserName.TextColor = UIColor.FromRGB(172, 180, 186);
                fieldUserName.Text = "Username";
			}
			else
			{
                fieldUserName.TextColor = UIColor.Black;
			}

			return true;
		}

		bool FieldPass_ShouldBeginEditing(MDTextField textField)
		{
			isPAnimating = true;
            if (!isEAnimating && !isUAnimating)
			{
				StartAnimation();
			}
			else
			{
                isEAnimating = isUAnimating = false;
			}

            viewLinePass.BackgroundColor = UIColor.FromRGB(57, 169, 246);
			fieldPass.TextColor = UIColor.Black;
			fieldPass.SecureTextEntry = true;
			if (fieldPass.Text == "Password (minimum 6 characters)")
			{
				fieldPass.Text = "";
			}

			return true;
		}

		bool FieldPass_ShouldEndEditing(MDTextField textField)
		{
            if (isPAnimating && !isEAnimating && !isUAnimating)
			{
				isPAnimating = false;
				StopAnimationC();
			}

            viewLinePass.BackgroundColor = UIColor.Clear;
			if (fieldPass.Text == "" || fieldPass.Text == "Password (minimum 6 characters)")
			{
				fieldPass.TextColor = UIColor.FromRGB(172, 180, 186);
				fieldPass.NormalColor = UIColor.FromRGB(172, 180, 186);
				fieldPass.SecureTextEntry = false;
				fieldPass.Text = "Password (minimum 6 characters)";
			}
			else
			{
				fieldPass.TextColor = UIColor.Black;
				fieldPass.SecureTextEntry = true;

				fieldPass.TextColor = UIColor.Black;

				if (fieldPass.Text.Length >= 6)
				{
					fieldPass.NormalColor = UIColor.FromRGB(172, 180, 186);
				}
				else
				{
					fieldPass.NormalColor = UIColor.FromRGB(238, 93, 78);
				}
			}

			return true;
		}

		void StartAnimation()
		{
			UIView.BeginAnimations("ConnectAnimation");
			UIView.SetAnimationDuration(0.35);
			UIView.SetAnimationCurve(UIViewAnimationCurve.EaseOut);
			UIView.SetAnimationDelegate(this);
			UIView.SetAnimationDidStopSelector(new Selector("ConnectAnimation:finished:context:"));
			viewConnect.Alpha = 0;
			UIView.CommitAnimations();
		}

		void StopAnimation()
		{
			UIView.BeginAnimations("ConnectAnimation");
			UIView.SetAnimationDuration(0.35);
			UIView.SetAnimationCurve(UIViewAnimationCurve.EaseOut);
			UIView.SetAnimationDelegate(this);
			UIView.SetAnimationDidStopSelector(new Selector("ConnectAnimation:finished:context:"));
			viewConnect.Alpha = 1;
			UIView.CommitAnimations();
		}

		void StartAnimationC()
		{
			viewConnectTopConstraint.Constant = -viewConnect.Frame.Height;
			View.UpdateConstraintsIfNeeded();
			UIView.AnimateNotify(0.35, () =>
			{
				View.LayoutIfNeeded();
			}, (finished) =>
			{
				//IsAnimating = false;
			});
		}

		void StopAnimationC()
		{
			viewConnectTopConstraint.Constant = 24;
			View.UpdateConstraintsIfNeeded();
			UIView.AnimateNotify(0.35, () =>
			{
				View.LayoutIfNeeded();
			}, (finished) =>
			{
				//IsAnimating = false;
                StopAnimation();
			});
		}

		[Export("ConnectAnimation:finished:context:")]
		void ConnectAnimation(NSString animationID, NSNumber finished, NSObject context)
		{
            if (isEAnimating || isUAnimating || isPAnimating)
			{
				viewConnect.Alpha = 0;
				StartAnimationC();
			}
			else
			{
				viewConnect.Alpha = 1;
			}
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
