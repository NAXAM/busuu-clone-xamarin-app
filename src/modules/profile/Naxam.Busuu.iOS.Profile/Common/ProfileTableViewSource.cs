using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.iOS.Profile.Cells;
using UIKit;

namespace Naxam.Busuu.iOS.Profile.Common
{
    public class ProfileTableViewSource : MvxTableViewSource, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		IMvxCommand _ViewProfileCommand;
		public IMvxCommand ViewProfileCommand
		{
			get
			{
				return _ViewProfileCommand;
			}

			set
			{
				SetProperty(ref _ViewProfileCommand, value);
			}
		}

		public ProfileTableViewSource(UITableView tableView) : base(tableView)
        {

		}

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
			var cell = (ProfileStaticCell)tableView.DequeueReusableCell((NSString)"ProfileStaticCell");

            cell.ViewProfileHandler -= HandleViewProfile;
			cell.ViewProfileHandler += HandleViewProfile;
			return cell;
        }

		void HandleViewProfile(object sender, SocialModel e)
		{
			if (ViewProfileCommand?.CanExecute(e) != true) return;

			ViewProfileCommand.Execute(e);
		}

		void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(backingField, value)) return;

			backingField = value;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}		
	}
}
