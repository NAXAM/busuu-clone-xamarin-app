using System.ComponentModel;
using System.Runtime.CompilerServices;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.iOS.Social.Cells;
using UIKit;

namespace Naxam.Busuu.iOS.Social.Common
{
    public class SocialDetailTableViewSource : MvxTableViewSource, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		IMvxCommand _ReplyViewCommand;
		public IMvxCommand ReplyViewCommand
		{
			get => _ReplyViewCommand;
			set => SetProperty(ref _ReplyViewCommand, value);
		}

        public SocialDetailTableViewSource(UITableView tableview) : base(tableview)
        {
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var itemFeedback = (CustomFeedbackModel)item;

            CommentSocialDetailCell cell;
           
            if (itemFeedback.Boss)
            {
				cell = (CommentSocialDetailCell)tableView.DequeueReusableCell((NSString)"CommentSocialDetailCell");
            }
            else
            {
                cell = (CommentSocialDetailCell)tableView.DequeueReusableCell((NSString)"ReplyCommentSocialDetailCell");
            }

			cell.ViewSocialDetailHandler -= HandleViewSocialDetail;
			cell.ViewSocialDetailHandler += HandleViewSocialDetail;

            return cell;
        }

		void HandleViewSocialDetail(object sender, CustomFeedbackModel e)
		{
			if (ReplyViewCommand?.CanExecute(e) != true) return;

			ReplyViewCommand.Execute(e);
		}

		void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(backingField, value)) return;

			backingField = value;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
