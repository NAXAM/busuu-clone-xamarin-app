// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.iOS.Core.Views;
using Naxam.Busuu.iOS.Learning.Cells;
using Naxam.Busuu.iOS.Learning.Common;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Learning.ViewModels;
using UIKit;

namespace Naxam.Busuu.iOS.Learning.Views
{
	[MvxFromStoryboard(StoryboardName = "Learning")]
	[MvxTabPresentation(WrapInNavigationController = true, TabIconName = "learn_tab_icon", TabName = "Learn", TabSelectedIconName = "learn_tab_icon_selected")]
    public partial class LearnView : MvxViewController<LearnViewModel>
	{
		public static int myPoint = 0;
        public static int myItemExercise = 0;
		public static IMvxMessenger messengerDownload = Mvx.Resolve<IMvxMessenger>();
        MvxSubscriptionToken token;

		public LearnView(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.NavigationController.NavigationBarHidden = false;

            UIView viewTitle = new UIView();
            viewTitle.Frame = new CGRect(0, 0, View.Bounds.Size.Width, 44);
            viewTitle.BackgroundColor = UIColor.FromRGB(57, 169, 246);

            UIImageView imgLanguageTitle = new UIImageView();
            imgLanguageTitle.Frame = new CGRect(5, 8, 28, 28);
            imgLanguageTitle.Image = UIImage.FromFile("flag_small_english.png");
            imgLanguageTitle.Layer.CornerRadius = 14;
            imgLanguageTitle.Layer.BorderWidth = 1.75f;
            imgLanguageTitle.Layer.BorderColor = UIColor.White.CGColor;

            UILabel lblTitle = new UILabel(new CGRect(48, 0, 100, 44));
            lblTitle.Text = "Learn";
            lblTitle.Font = UIFont.SystemFontOfSize(16.5f, UIFontWeight.Semibold);
            lblTitle.TextColor = UIColor.White;

            viewTitle.AddSubview(imgLanguageTitle);
            viewTitle.AddSubview(lblTitle);

			UITapGestureRecognizer tapTitle = new UITapGestureRecognizer(() => {
                this.ViewModel.ChangeLanguageCommand.Execute();
			});

            viewTitle.AddGestureRecognizer(tapTitle);

			NavigationController.NavigationBar.Layer.ShadowRadius = 2;
			NavigationController.NavigationBar.Layer.ShadowOffset = new CGSize(0, 2);
			NavigationController.NavigationBar.Layer.ShadowOpacity = 0.25f;

            NavigationItem.TitleView = viewTitle;

            token = messengerDownload.Subscribe<ShowDownloadDialogMessager>(OnDownLoadMessager);

			//var nib = UINib.FromName("LessonHeader", null);
			//LessonTableView.RegisterNibForHeaderFooterViewReuse(nib, "LessonHeader");

			var buyPremiumCell = BuyPremiumCell.Create();
			buyPremiumCell.Frame = new CGRect(0, 0, View.Bounds.Size.Width, 50);
			View.AddSubview(buyPremiumCell);

			var source = new LessionTableViewSource(LessonTableView);
			
			var bindingSet = this.CreateBindingSet<LearnView, LearnViewModel>();
			bindingSet.Bind(buyPremiumCell.BtnGo).To(vm => vm.GoPremiumCommand);
			bindingSet.Bind(source).To(vm => vm.LessonAndSubLessions);
			bindingSet.Bind(source).For(nameof(LessionTableViewSource.ExerciseCommand)).To(vm => vm.ExerciseClickCommand);
			bindingSet.Apply();

            LessonTableView.Source = source;
		}

		void OnDownLoadMessager(ShowDownloadDialogMessager obj)
		{
			var alert = UIAlertController.Create("Download success", "ahihi", UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
			PresentViewController(alert, true, null);	
		}
	}

	public class LessionTableViewSource : MvxTableViewSource, INotifyPropertyChanged, ILessonTableViewCellDelegate
	{
		public event PropertyChangedEventHandler PropertyChanged;
		List<int> lessonIndexs;
		List<int> openingLessons;
		Dictionary<int, List<NSIndexPath>> AllTopicWithLessonKey;

		IMvxCommand _downloadCommand;
		public IMvxCommand DownloadCommand
		{
			get
			{
				return _downloadCommand;
			}

			set
			{
				SetProperty(ref _downloadCommand, value);
			}
		}

		IMvxCommand _exerciseCommand;
		public IMvxCommand ExerciseCommand
		{
			get
			{
				return _exerciseCommand;
			}

			set
			{
				SetProperty(ref _exerciseCommand, value);
			}
		}

		public LessionTableViewSource(UITableView tableview) : base(tableview)
		{
			lessonIndexs = new List<int>();
			openingLessons = new List<int>();
			AllTopicWithLessonKey = new Dictionary<int, List<NSIndexPath>>();
		}

		//public override UIView GetViewForHeader(UITableView tableView, nint section)
		//{
		//	var cell = LessonHeader.Create();
		//	cell.Title.Text = "Beginner A1";
		//	return cell;
		//}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			var item = GetItemAt(indexPath);
			if (item is LessonModel)
			{
				return 80f;
			}
			if (item is TopicModel
				&& openingLessons.Any((arg) => AllTopicWithLessonKey[arg].Contains(indexPath)))
			{
				return 140f;
			}
			//foreach (var item in lessonIndexs)
			//{
			//    if(indexPath.Row == item)
			//    {
			//        return 80f;
			//    }
			//}
			//foreach (var lesson in openingLessons)
			//{
			//    foreach (var topic in AllTopicWithLessonKey[lesson])
			//    {
			//        if(indexPath.Row == topic.Row)
			//        {
			//            return 140f;
			//        }
			//    }
			//}
			return 0;
		}

		//public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		//{
		//	return 90;
		//}

		void HandleEventHandler(object sender, LessonModel e)
		{
		}

		void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(backingField, value)) return;

			backingField = value;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			NSString cellIdentifier;
			if (item is LessonModel)
			{
				cellIdentifier = (NSString)"lessonCell";
			}
			else if (item is TopicModel)
			{
				cellIdentifier = (NSString)"subCell";
			}
			else
			{
				throw new ArgumentException("Unknown item of type " + item.GetType().Name);
			}

			var cell = TableView.DequeueReusableCell(cellIdentifier, indexPath);

			if (cell is LessonTableViewCell lCell)
			{
				lessonIndexs.Add(indexPath.Row);
				var topics = new List<NSIndexPath>();

				for (int j = 1; j <= (item as LessonModel).Count; j++)
				{
					topics.Add(NSIndexPath.FromRowSection(indexPath.Row + j, 0));
				}

				if (!AllTopicWithLessonKey.ContainsKey(indexPath.Row)) AllTopicWithLessonKey.Add(indexPath.Row, topics);
				lCell.Delegate = this;
				lCell.Update(openingLessons.Contains(indexPath.Row), item as LessonModel);
			}

			if (cell is SubLessonTableViewCell sCell)
			{
				sCell.LessonId = AllTopicWithLessonKey.FirstOrDefault(x => x.Value.Contains(indexPath)).Key;
				sCell.TopicId = indexPath.Row;
				sCell.ExerciseClick -= SCell_ExerciseClick;
				sCell.ExerciseClick += SCell_ExerciseClick;
			}
			//System.Diagnostics.Debug.WriteLine($"Row {indexPath.Row} is got");
			return cell;
		}

		void SCell_ExerciseClick(object sender, ExerciseClickEventArg e)
		{
			if (ExerciseCommand?.CanExecute(e) != true) return;

			ExerciseCommand.Execute(e);
		}

		public void DidTapOnLessonCell(LessonTableViewCell cell)
		{
			var indexPath = TableView.IndexPathForCell(cell);

			if (cell.IsOpen)
			{
				openingLessons.Add(indexPath.Row);
				TableView.ReloadRows(AllTopicWithLessonKey[indexPath.Row].ToArray(), UITableViewRowAnimation.Automatic);
				TableView.ScrollToRow(indexPath, UITableViewScrollPosition.Top, true);
			}
			else
			{
				openingLessons.Remove(indexPath.Row);
				TableView.ReloadRows(AllTopicWithLessonKey[indexPath.Row].ToArray(), UITableViewRowAnimation.Automatic);
			}
		}
	}
}