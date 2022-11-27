using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Naxam.Busuu.iOS.Start.Common;
using Naxam.Busuu.Start.ViewModels;

namespace Naxam.Busuu.iOS.Start.Views
{
    [MvxFromStoryboard(StoryboardName = "Start")]
    public partial class ChooseLanguageView : MvxViewController<ChooseLanguageViewModel>
	{
		public ChooseLanguageView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationController.NavigationBarHidden = true;

			var lSource = new LanguageTableViewSource(LanguageTableView);

            var setBinding = this.CreateBindingSet<ChooseLanguageView, ChooseLanguageViewModel>();
            setBinding.Bind(btnBack).To(vm => vm.btnBackCommand);
			setBinding.Bind(lSource).To(vm => vm.Languages);
			setBinding.Bind(lSource).For(vm => vm.SelectionChangedCommand).To(vm => vm.RegisterCommand);
			setBinding.Apply();

            LanguageTableView.Source = lSource;

            LanguageTableView.Layer.CornerRadius = 5;
            ViewForTableView.Layer.CornerRadius = 5;
			ViewForTableView.Layer.ShadowOpacity = 0.35f;
			ViewForTableView.Layer.ShadowOffset = new CGSize(0, 2);
            ViewForTableView.Layer.MasksToBounds = false;
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			this.NavigationController.NavigationBarHidden = true;
		}		
	}
}
