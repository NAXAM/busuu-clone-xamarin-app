using Foundation;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.iOS.Notification.Cells;
using UIKit;

namespace Naxam.Busuu.iOS.Notification.Common
{
    public class NotificationTableViewSource : MvxTableViewSource
    {
		public NotificationTableViewSource(UITableView tableView) : base(tableView)
        {

		}

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
			return (NotificationCell)tableView.DequeueReusableCell((NSString)"NotificationCell");
        }
    }
}
