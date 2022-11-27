// This file has been autogenerated from a class added in the UI designer.

using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Naxam.Busuu.iOS.Profile.Common;
using Naxam.Busuu.Profile.ViewModels;

namespace Naxam.Busuu.iOS.Profile.Views
{
    [MvxFromStoryboard(StoryboardName = "Profile")]
    public partial class SettingInterfaceLanguageView : MvxViewController<SettingInterfaceLanguageViewModel>
	{
		public SettingInterfaceLanguageView (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            NavigationController.NavigationBarHidden = false;

            NavigationController.NavigationBar.Layer.ShadowRadius = 2;
            NavigationController.NavigationBar.Layer.ShadowOpacity = 0.25f;
			NavigationController.NavigationBar.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 2);

            var iSource = new InterfaceLanguageTableViewSource(InterfaceLanguageTableView);

            var setBinding = this.CreateBindingSet<SettingInterfaceLanguageView, SettingInterfaceLanguageViewModel>();			
            setBinding.Bind(iSource).To(vm => vm.Languages);
			setBinding.Bind(iSource).For(vm => vm.SelectedItem).To(vm => vm.Value);
			setBinding.Apply();

			InterfaceLanguageTableView.Source = iSource;
		}
	}
}