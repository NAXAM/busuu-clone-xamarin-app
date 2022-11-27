using System.ComponentModel;
using System.Runtime.CompilerServices;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.iOS.Social.Cells;
using Naxam.Busuu.Core.Models;
using UIKit;

namespace Naxam.Busuu.iOS.Social.Common
{
    public class FriendsTableViewSource : MvxTableViewSource, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		IMvxCommand _viewFriendsCommand;
		public IMvxCommand ViewFriendsCommand
		{
            get => _viewFriendsCommand;
			set => SetProperty(ref _viewFriendsCommand, value);		
		}

		public FriendsTableViewSource(UITableView tableView) : base(tableView)
		{

		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var cell = (FriendsCell)tableView.DequeueReusableCell((NSString)"FriendsCell");
			cell.ViewFriendsHandler -= HandleViewFriends;
			cell.ViewFriendsHandler += HandleViewFriends;
			return cell;
		}

		void HandleViewFriends(object sender, SocialModel e)
		{
			if (ViewFriendsCommand?.CanExecute(e) != true) return;

			ViewFriendsCommand.Execute(e);
		}

		void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(backingField, value)) return;

			backingField = value;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}	
    }
}
