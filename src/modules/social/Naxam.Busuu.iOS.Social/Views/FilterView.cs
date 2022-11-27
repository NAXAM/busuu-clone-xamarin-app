using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using Naxam.Busuu.Social.ViewModels;
using UIKit;

namespace Naxam.Busuu.iOS.Social
{
    [MvxFromStoryboard(StoryboardName = "Social")]
	[MvxModalPresentation(WrapInNavigationController = true)]
	public partial class FilterView : MvxViewController<FilterViewModel>
	{
        UIBarButtonItem btndone;

		public FilterView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			var btnBack = new UIBarButtonItem()
			{
				Image = UIImage.FromBundle("iconBack")
			};

			NavigationItem.SetLeftBarButtonItem(btnBack, false);

            btndone = new UIBarButtonItem()
            {
                Title = "DONE"
            };

            NavigationItem.SetLeftBarButtonItem(btnBack, false);
            NavigationItem.SetRightBarButtonItem(btndone, false);

            ViewShadow.Layer.ShadowRadius = 2;
			ViewShadow.Layer.ShadowOpacity = 0.25f;
			ViewShadow.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 2);

            var setBinding = this.CreateBindingSet<FilterView, FilterViewModel>();
            setBinding.Bind(btnBack).For("Clicked").To(vm => vm.GoBackCommand);
			setBinding.Bind(btndone).For("Clicked").To(vm => vm.DoneCommand);
            setBinding.Bind(btndone).For(vm => vm.Enabled).To(vm => vm.VisibilyDoneButton);
            setBinding.Bind(SwitchLanguage).For(vm => vm.On).To(vm => vm.FilterLanguage);
            setBinding.Bind(SwitchWrite).For(vm => vm.On).To(vm => vm.FilterWriting);
			setBinding.Bind(SwitchSpeak).For(vm => vm.On).To(vm => vm.FilterSpeaking);
			setBinding.Apply();
		}
	}
}
