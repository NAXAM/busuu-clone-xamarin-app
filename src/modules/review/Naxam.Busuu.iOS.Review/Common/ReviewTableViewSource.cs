using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.iOS.Review.Cells;
using Naxam.Busuu.iOS.Review.Views;
using Naxam.Busuu.Review.Models;
using UIKit;

namespace Naxam.Busuu.iOS.Review.Common
{
    public class ReviewTableViewSource : MvxTableViewSource, INotifyPropertyChanged
    {		
		public event PropertyChangedEventHandler PropertyChanged;

		IMvxCommand _FavoriteCommand;
		public IMvxCommand FavoriteCommand
		{
			get => _FavoriteCommand;
			set => SetProperty(ref _FavoriteCommand, value);
		}

		IMvxCommand _TapCommand;
		public IMvxCommand TapCommand
		{
			get => _TapCommand;
			set => SetProperty(ref _TapCommand, value);
		}

		public ReviewTableViewSource(UITableView tableView) : base(tableView)
        {
			
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = (ReviewTableViewCell)tableView.DequeueReusableCell("reviewCell", indexPath);
			
            cell.StarTapHandler -= HandleStarTap;
            cell.StarTapHandler += HandleStarTap;

            cell.TapHandler -= HandleTap;
			cell.TapHandler += HandleTap;

            return cell;
        }

		[Export("tableView:numberOfRowsInSection:")]
		public override nint RowsInSection(UITableView tableview, nint section)
		{
            return ReviewView.grouping[section].Count();
		}

		[Export("numberOfSectionsInTableView:")]
        public override nint NumberOfSections(UITableView tableView)
		{
            return ReviewView.grouping.Count();
		}

		[Export("tableView:titleForHeaderInSection:")]
        public override string TitleForHeader(UITableView tableView, nint section)
		{
			return ReviewView.grouping[section].Key.ToString().ToUpper();
		}

		protected override object GetItemAt(NSIndexPath indexPath)
		{
			if (ReviewView.grouping == null) return null;
			var section = ReviewView.grouping[indexPath.Section];
			return section.ElementAt(indexPath.Row);
		}

        void HandleStarTap(object sender, ReviewModel e)
		{
			if (FavoriteCommand?.CanExecute(e) != true) return;

			FavoriteCommand.Execute(e);
		}

		void HandleTap(object sender, ReviewModel e)
		{
			if (TapCommand?.CanExecute(e) != true) return;

			TapCommand.Execute(e);
		}

		void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(backingField, value)) return;

			backingField = value;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
