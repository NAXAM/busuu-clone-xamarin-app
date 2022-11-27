using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using Naxam.Busuu.Start.ViewModels;

namespace Naxam.Busuu.iOS.Start.Views
{
    [MvxFromStoryboard(StoryboardName = "Start")]
	[MvxRootPresentation(WrapInNavigationController = true)]
	public partial class StartPageView : MvxViewController<StartPageViewModel>
	{
		public StartPageView (IntPtr handle) : base (handle)
		{
            
		}

        public override void ViewDidLoad()
        {
			this.Request = new MvxViewModelRequest<StartPageViewModel>(null, null);

			base.ViewDidLoad();

            this.NavigationController.NavigationBarHidden = true;
         
            btnGetStarted.Layer.CornerRadius = btnGetStarted.Frame.Height / 2;

            var setBinding = this.CreateBindingSet<StartPageView, StartPageViewModel>();
            setBinding.Bind(btnGetStarted).To(vm => vm.GetStartedCommand);
            setBinding.Bind(btnLogin).To(vm => vm.LoginCommand);
			setBinding.Apply();
        }

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

            this.NavigationController.NavigationBarHidden = true;
		}

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

			this.NavigationController.NavigationBarHidden = true;
		}
	}
}
