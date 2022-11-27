using Foundation;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.iOS.Start.Cells;
using UIKit;

namespace Naxam.Busuu.iOS.Start.Common
{
    public class LanguageTableViewSource : MvxTableViewSource
    {
		public LanguageTableViewSource(UITableView tableView) : base(tableView)
        {
		}

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return (LanguageTableViewCell)tableView.DequeueReusableCell((NSString)"LanguageTableViewCell");
        }
    }
}
