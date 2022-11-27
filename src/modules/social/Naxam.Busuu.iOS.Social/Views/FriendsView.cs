using System;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Social.ViewModels;
using UIKit;
using Naxam.Busuu.iOS.Social.Common;

namespace Naxam.Busuu.iOS.Social.Views
{
	[MvxFromStoryboard(StoryboardName = "Social")]
    public partial class FriendsView : MvxViewController<FriendsViewModel>
	{
		public FriendsView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            Request = new MvxViewModelRequest<FriendsViewModel>(null, null);

			base.ViewDidLoad();
            		
			var fSource = new FriendsTableViewSource(FriendsTableView);

			var setBinding = this.CreateBindingSet<FriendsView, FriendsViewModel>();
			setBinding.Bind(fSource).To(vm => vm.FriendsData);
            setBinding.Bind(fSource).For(nameof(FriendsTableViewSource.ViewFriendsCommand)).To(vm => vm.ViewFriendsCommand);
			setBinding.Apply();

			FriendsTableView.RowHeight = UITableView.AutomaticDimension;
			FriendsTableView.EstimatedRowHeight = 210f;
            FriendsTableView.Source = fSource;			
        }
	}
}
