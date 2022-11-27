using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Naxam.Busuu.iOS.Start.Common;
using Naxam.Busuu.Start.ViewModels;

namespace Naxam.Busuu.iOS.Start.Views
{
    [MvxFromStoryboard(StoryboardName = "Start")]
    public partial class ChooseCountryView : MvxViewController<ChooseCountryViewModel>
	{
		public ChooseCountryView (IntPtr handle) : base (handle)
		{
		}
              
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			ViewShadow.Layer.ShadowRadius = 2;
			ViewShadow.Layer.ShadowOpacity = 0.25f;
			ViewShadow.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 2);

            ChooseCountryTableViewSource.allCountry = this.ViewModel.Countries;

            var cSource = new ChooseCountryTableViewSource(ChooseCountryTableView);

            var setBinding = this.CreateBindingSet<ChooseCountryView, ChooseCountryViewModel>();
            setBinding.Bind(cSource).To(vm => vm.Countries);
            setBinding.Bind(cSource).For(vm => vm.SelectedItem).To(vm => vm.CountrySelected);
			setBinding.Apply();
           	
			ChooseCountryTableView.Source = cSource;
        }
	}
}
