using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MaterialControls;
using Naxam.Busuu.Start.ViewModels;
using ObjCRuntime;
using UIKit;
using FBKVOControllerNS;
using System.Text.RegularExpressions;

namespace Naxam.Busuu.iOS.Start
{
    [MvxFromStoryboard(StoryboardName = "Start")]
    public partial class LoginView : MvxViewController<LoginViewModel>
	{
        MDTextField fieldEmailPhone;
        MDTextField fieldPass;
        FBKVOController _KVOController;
        bool isAnimating1;
        bool isAnimating2;

		public LoginView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationController.NavigationBarHidden = false;

			fieldEmailPhone = new MDTextField();
			fieldEmailPhone.TextColor = UIColor.FromRGB(172, 180, 186);
            fieldEmailPhone.NormalColor = UIColor.FromRGB(172, 180, 186);
			fieldEmailPhone.HighlightColor = UIColor.FromRGB(57, 169, 246);
			fieldEmailPhone.LabelsFont = UIFont.SystemFontOfSize(14);
			fieldEmailPhone.InputTextFont = UIFont.SystemFontOfSize(14);
            fieldEmailPhone.ShouldBeginEditing = FieldEmailPhone_ShouldBeginEditing;
            fieldEmailPhone.ShouldEndEditing = FieldEmailPhone_ShouldEndEditing;
            fieldEmailPhone.Layer.Frame = new CoreGraphics.CGRect(0, 0, ViewEmail.Bounds.Width, ViewEmail.Bounds.Height);

			fieldPass = new MDTextField();
			fieldPass.TextColor = UIColor.FromRGB(172, 180, 186);
            fieldPass.NormalColor = UIColor.FromRGB(172, 180, 186);
			fieldPass.HighlightColor = UIColor.FromRGB(57, 169, 246);
			fieldPass.LabelsFont = UIFont.SystemFontOfSize(14);
			fieldPass.InputTextFont = UIFont.SystemFontOfSize(14);
            fieldPass.ShouldBeginEditing = FieldPass_ShouldBeginEditing;
            fieldPass.ShouldEndEditing = FieldPass_ShouldEndEditing;
            fieldPass.Layer.Frame = new CoreGraphics.CGRect(0, 0, ViewPassword.Bounds.Width, ViewPassword.Bounds.Height);

            ViewEmail.InsertSubview(fieldEmailPhone, 0);
            ViewPassword.InsertSubview(fieldPass, 0);

			ViewShadow.Layer.ShadowRadius = 2;
			ViewShadow.Layer.ShadowOpacity = 0.25f;
			ViewShadow.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 2);

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

            btnLogin.Layer.CornerRadius = btnLogin.Frame.Height / 2;

			_KVOController = new FBKVOController(this);
            _KVOController.Observe(btnLogin, "enabled", NSKeyValueObservingOptions.Initial | NSKeyValueObservingOptions.New, UpdateBackgroundForItem);

			var setBind = this.CreateBindingSet<LoginView, LoginViewModel>();
            setBind.Bind(btnFacebook).To(vm => vm.LoginCommand);
            setBind.Bind(btnGoogle).To(vm => vm.LoginCommand);
            setBind.Bind(btnLogin).To(vm => vm.LoginCommand);
            setBind.Bind(btnForgotPassword).To(vm => vm.ForgotPasswordCommand);
            setBind.Bind(fieldEmailPhone).For(vm => vm.Text).To(vm => vm.TextEmail);
            setBind.Bind(fieldPass).For(vm => vm.Text).To(vm => vm.TextPass);
            setBind.Bind(btnLogin).For(vm => vm.Enabled).To(vm => vm.IsEnableLoginBtn);
			setBind.Apply();
		}

		void UpdateBackgroundForItem(NSObject arg0, NSObject arg1, NSDictionary<NSString, NSObject> arg2)
		{
			if (arg1 is UIButton item)
			{
				if (item.Enabled)
				{
					btnLogin.BackgroundColor = UIColor.FromRGB(57, 169, 246);
					btnLogin.Layer.ShadowRadius = 1.5f;
					btnLogin.Layer.ShadowOpacity = 0.25f;
					btnLogin.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 1.5f);
				}
				else
				{
					btnLogin.BackgroundColor = UIColor.FromRGB(214, 222, 230);
					btnLogin.Layer.ShadowRadius = 0;
					btnLogin.Layer.ShadowOpacity = 0;
					btnLogin.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 0);
				}
			}
		}

        bool FieldEmailPhone_ShouldBeginEditing(MDTextField textField)
        {
			isAnimating1 = true;
            if (!isAnimating2)
            {
                StartAnimation();
            }
            else
            {
                isAnimating2 = false;
            }

            viewLineEmail.BackgroundColor = UIColor.FromRGB(57, 169, 246);
			fieldEmailPhone.TextColor = UIColor.Black;
            if (fieldEmailPhone.Text == "Email address or phone number")
            {
				fieldEmailPhone.Text = "";          
            }

            return true;
        }

        bool FieldEmailPhone_ShouldEndEditing(MDTextField textField)
        {
            if (isAnimating1 && !isAnimating2)
            {
                isAnimating1 = false;
                StopAnimationC();
            }

            viewLineEmail.BackgroundColor = UIColor.Clear;
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

		bool FieldPass_ShouldBeginEditing(MDTextField textField)
		{
			isAnimating2 = true;
            if (!isAnimating1)
            {
                StartAnimation();
            }		
            else
            {
				isAnimating1 = false;
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
            if (isAnimating2 && !isAnimating1)
            {
                isAnimating2 = false;
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
            viewConnectTopConstraint.Constant =- viewConnect.Frame.Height;
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
            if (isAnimating1 || isAnimating2)
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
