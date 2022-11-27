using System.ComponentModel;
using System.Runtime.CompilerServices;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.iOS.Notification.Cells;
using UIKit;

namespace Naxam.Busuu.iOS.Notification.Common
{
    public class FriendRequestTableViewSource : MvxTableViewSource, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		IMvxCommand _viewFriendsYesCommand;
		public IMvxCommand ViewFriendsYesCommand
		{
			get
			{
				return _viewFriendsYesCommand;
			}

			set
			{
				SetProperty(ref _viewFriendsYesCommand, value);
			}
		}

		IMvxCommand _viewFriendsNoCommand;
		public IMvxCommand ViewFriendsNoCommand
		{
			get
			{
				return _viewFriendsNoCommand;
			}

			set
			{
				SetProperty(ref _viewFriendsNoCommand, value);
			}
		}

		public FriendRequestTableViewSource(UITableView tableView) : base(tableView)
        {

		}

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
			var cell = (FriendRequestCell)tableView.DequeueReusableCell((NSString)"FriendRequestCell");

			cell.ViewFriendsYesHandler -= HandleViewFriendsYes;
			cell.ViewFriendsYesHandler += HandleViewFriendsYes;

			cell.ViewFriendsNoHandler -= HandleViewFriendsNo;
			cell.ViewFriendsNoHandler += HandleViewFriendsNo;

            return cell;
		}



        void HandleViewFriendsYes(object sender, FriendRequestModel e)
		{
			if (ViewFriendsYesCommand?.CanExecute(e) != true) return;

			ViewFriendsYesCommand.Execute(e);
		}

		void HandleViewFriendsNo(object sender, FriendRequestModel e)
		{
			if (ViewFriendsNoCommand?.CanExecute(e) != true) return;

			ViewFriendsNoCommand.Execute(e);
		}

		void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(backingField, value)) return;

			backingField = value;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
    }
}
