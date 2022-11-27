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
    public class FriendListTableViewSource : MvxTableViewSource
    {
		public FriendListTableViewSource(UITableView tableView) : base(tableView)
        {

		}

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return (FriendListCell)tableView.DequeueReusableCell((NSString)"FriendListCell");
		}
    }
}
