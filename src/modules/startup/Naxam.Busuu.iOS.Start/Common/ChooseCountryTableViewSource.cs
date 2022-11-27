using System;
using System.Linq;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.iOS.Start.Cells;
using UIKit;

namespace Naxam.Busuu.iOS.Start.Common
{
    public class ChooseCountryTableViewSource : MvxTableViewSource
    {
        public static MvxObservableCollection<CountryModel> allCountry;
        IGrouping<char, CountryModel>[] grouping;

		public ChooseCountryTableViewSource(UITableView tableView) : base(tableView)
        {
            grouping = (from w in allCountry
                        orderby w.Country[0].ToString().ToUpper() ascending
                        group w by w.Country[0] into g
                        select g).ToArray();
		}

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return (CountryCell)tableView.DequeueReusableCell((NSString)"CountryCell");
        }

		[Export("tableView:numberOfRowsInSection:")]
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return grouping[section].Count();
		}

		[Export("tableView:titleForHeaderInSection:")]
        public override string TitleForHeader(UITableView tableView, nint section)
		{
			return grouping[section].Key.ToString().ToUpper();
		}

		[Export("numberOfSectionsInTableView:")]
        public override nint NumberOfSections(UITableView tableView)
		{
            return grouping.Count();
		}

        protected override object GetItemAt(NSIndexPath indexPath)
        {
            if (grouping == null) return null;
            var section = grouping[indexPath.Section];
            return section.ElementAt(indexPath.Row);
        }
    }
}
