using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.iOS.Profile.Cells;
using UIKit;

namespace Naxam.Busuu.iOS.Profile.Common
{
    public class LanguageSpeakTableViewSource : MvxTableViewSource
    {
		public LanguageSpeakTableViewSource(UITableView tableView) : base(tableView)
        {
		}

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return (LanguageSpeakCell)tableView.DequeueReusableCell((NSString)"LanguageSpeakCell");
        }
    }
}
