using System;
using CoreGraphics;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Review.ViewModels;
using UIKit;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views.Presenters.Attributes;
using Naxam.Busuu.IOS.Core.Floaty;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Platform;
using Naxam.Busuu.iOS.Review.Common;
using System.Linq;
using Naxam.Busuu.Review.Models;
using Naxam.Busuu.Core.Messager;
using FBKVOControllerNS;
using Naxam.Busuu.iOS.Core.Views;
using Naxam.Busuu.iOS.Core.Converter;

namespace Naxam.Busuu.iOS.Review.Views
{
    [MvxFromStoryboard(StoryboardName = "Review")]
	[MvxTabPresentation(WrapInNavigationController = true, TabIconName = "review_tab_icon", TabName = "Review", TabSelectedIconName = "review_tab_icon_selected")]
	public partial class ReviewView : MvxViewController<ReviewViewModel>
	{
        public static string textSearch = "";
		public static IMvxMessenger messengerReset = Mvx.Resolve<IMvxMessenger>();
		MvxSubscriptionToken token;

		FBKVOController _KVOController;

		public static IGrouping<char, ReviewModel>[] grouping;
        public static IGrouping<char, ReviewModel>[] groupFavorite;
        public static IGrouping<char, ReviewModel>[] groupSearch;

        public ReviewView(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            NavigationController.NavigationBarHidden = true;

			token = messengerReset.Subscribe<ResetTableViewMessager>(OnResetMessager);

			var btnContinueLearning = new ActionButtonItem("CONTINUE LEARNING", UIImage.FromBundle("fab_menu_row_learning"), UIColor.White);
			btnContinueLearning.ActionPerform = HandleAction;

            var btnAllac = new ActionButtonItem("TEST ALL", UIImage.FromBundle("fab_menu_row_all"), UIColor.FromRGB(57, 169, 246));
			btnAllac.ActionPerform = HandleAction;

            var btnStrength = new ActionButtonItem("STRENGTHEN VOCABULARY", UIImage.FromBundle("fab_menu_row_weak"), UIColor.FromRGB(234, 67, 50));
			btnStrength.ActionPerform = HandleAction;

            var btnTestFavorite = new ActionButtonItem("TEST FAVORITES", UIImage.FromBundle("fab_menu_row_fav"), UIColor.FromRGB(214, 222, 230));
			btnTestFavorite.ActionPerform = HandleAction;

			var actionButton = new ActionButton(this.View, new[] { btnContinueLearning, btnAllac, btnStrength, btnTestFavorite }, UIColor.FromRGB(57, 169, 246), UIColor.FromRGB(57, 169, 246));
			actionButton.Action = delegate
			{
				actionButton.ToggleMenu();
			};
			actionButton.SetTitle("+", UIControlState.Normal);

			var buyPremiumCell = BuyPremiumCell.Create();
			buyPremiumCell.Frame = new CGRect(uiViewSlide.Frame.GetMinX(), uiViewSlide.Frame.GetMaxY() + 64, View.Bounds.Size.Width, 50);
           
			View.AddSubview(buyPremiumCell);
            View.InsertSubview(buyPremiumCell, 3);

			uiViewButton.Layer.ShadowRadius = 2;
			uiViewButton.Layer.ShadowOffset = new CGSize(0, 2);
			uiViewButton.Layer.ShadowOpacity = 0.25f;

			btnCloseSeach.ContentEdgeInsets = new UIEdgeInsets(4, 4, 4, 4);

			_KVOController = new FBKVOController(this);
			_KVOController.Observe(btnCloseSeach, "Hidden", NSKeyValueObservingOptions.Initial | NSKeyValueObservingOptions.New, UpdateBackgroundForItem);
			_KVOController.Observe(btnSearch, "Hidden", NSKeyValueObservingOptions.Initial | NSKeyValueObservingOptions.New, UpdateBackgroundForItem);

			grouping = (from w in ViewModel.AllReviews
						orderby w.Title[0].ToString().ToUpper() ascending
						group w by w.Title[0] into g
						select g).ToArray();

            groupFavorite = (from w in ViewModel.FavoriteReviews
						orderby w.Title[0].ToString().ToUpper() ascending
						group w by w.Title[0] into g
						select g).ToArray();

            groupSearch = (from w in ViewModel.SearchReviews
						orderby w.Title[0].ToString().ToUpper() ascending
						group w by w.Title[0] into g
						select g).ToArray();

            var allReviewSource = new ReviewTableViewSource(ReviewTableView);
			var favoriteReviewSource = new FavoriteReviewTableViewSource(ReviewTableView);
			var searchReviewSource = new SearchReviewTableViewSource(ReviewTableView);

			var setBinding = this.CreateBindingSet<ReviewView, ReviewViewModel>();
			setBinding.Bind(buyPremiumCell.BtnGo).To(vm => vm.GoPremiumCommand);
            setBinding.Bind(allReviewSource).To(vm => vm.AllReviews);
            setBinding.Bind(allReviewSource).For(nameof(ReviewTableViewSource.TapCommand)).To(vm => vm.TapCommand);
            setBinding.Bind(allReviewSource).For(nameof(ReviewTableViewSource.FavoriteCommand)).To(vm => vm.FavoriteCommand);
            setBinding.Bind(favoriteReviewSource).To(vm => vm.FavoriteReviews);
			setBinding.Bind(favoriteReviewSource).For(nameof(FavoriteReviewTableViewSource.TapCommand)).To(vm => vm.TapCommand);
			setBinding.Bind(favoriteReviewSource).For(nameof(FavoriteReviewTableViewSource.FavoriteCommand)).To(vm => vm.FavoriteCommand);
            setBinding.Bind(searchReviewSource).To(vm => vm.SearchReviews);
			setBinding.Bind(searchReviewSource).For(nameof(SearchReviewTableViewSource.TapCommand)).To(vm => vm.TapCommand);
			setBinding.Bind(searchReviewSource).For(nameof(SearchReviewTableViewSource.FavoriteCommand)).To(vm => vm.FavoriteCommand);
            setBinding.Bind(SearchReviewTableView).For(vm => vm.Hidden).To(vm => vm.VisibleButtonSearch);
            setBinding.Bind(btnAll).For(vm => vm.Enabled).To(vm => vm.EnabledAllButton);
            setBinding.Bind(btnFavorite).For(vm => vm.Enabled).To(vm => vm.EnabledFavoriteButton);
			setBinding.Bind(btnSearch).To(vm => vm.SearchCommand);
            setBinding.Bind(btnCloseSeach).To(vm => vm.CloseCommand);
            setBinding.Bind(btnBack).To(vm => vm.CloseCommand);
            setBinding.Bind(btnBack).For(vm => vm.Hidden).To(vm => vm.VisibleTextSearch).WithConversion(nameof(VisibilityToHideConverter));
            setBinding.Bind(textFakeReview).For(vm => vm.Hidden).To(vm => vm.VisibleButtonSearch).WithConversion(nameof(VisibilityToHideConverter));
			setBinding.Bind(btnSearch).For(vm => vm.Hidden).To(vm => vm.VisibleButtonSearch).WithConversion(nameof(VisibilityToHideConverter));
			setBinding.Bind(btnCloseSeach).For(vm => vm.Hidden).To(vm => vm.VisibleCloseButton).WithConversion(nameof(VisibilityToHideConverter));
			setBinding.Bind(textFieldSearch).For(vm => vm.Hidden).To(vm => vm.VisibleTextSearch).WithConversion(nameof(VisibilityToHideConverter));
			setBinding.Bind(textFieldSearch).For(vm => vm.Text).To(vm => vm.SearchText);
			setBinding.Apply();

			ReviewTableView.RowHeight = UITableView.AutomaticDimension;
			ReviewTableView.EstimatedRowHeight = 140.5f;
            FavoriteReviewTableView.RowHeight = UITableView.AutomaticDimension;
			FavoriteReviewTableView.EstimatedRowHeight = 140.5f;
			SearchReviewTableView.RowHeight = UITableView.AutomaticDimension;
			SearchReviewTableView.EstimatedRowHeight = 140.5f;

            ReviewTableView.Source = allReviewSource;
            FavoriteReviewTableView.Source = favoriteReviewSource;
            SearchReviewTableView.Source = searchReviewSource;
		}

		void OnResetMessager(ResetTableViewMessager obj)
		{
            textSearch = textFieldSearch.Text;

            if(!SearchReviewTableView.Hidden)
            {
                groupSearch = (from w in ViewModel.SearchReviews
							orderby w.Title[0].ToString().ToUpper() ascending
							group w by w.Title[0] into g
							select g).ToArray();

                SearchReviewTableView.ReloadData();
            }
            else if (!FavoriteReviewTableView.Hidden)
            {
				groupFavorite = (from w in ViewModel.FavoriteReviews
								 orderby w.Title[0].ToString().ToUpper() ascending
								 group w by w.Title[0] into g
								 select g).ToArray();

                FavoriteReviewTableView.ReloadData();
            }
            else
            {
				grouping = (from w in ViewModel.AllReviews
							orderby w.Title[0].ToString().ToUpper() ascending
							group w by w.Title[0] into g
							select g).ToArray();

				ReviewTableView.ReloadData();
            }
		}

		void UpdateBackgroundForItem(NSObject arg0, NSObject arg1, NSDictionary<NSString, NSObject> arg2)
		{
			if (arg1 is UIButton item)
			{
				if (btnSearch.Hidden)
				{
					if (item.Hidden)
					{
						textFakeSearch.Hidden = false;
					}
					else
					{
						textFakeSearch.Hidden = true;
					}

					textFieldSearch.BecomeFirstResponder();
				}
				else
				{
					textFakeSearch.Hidden = true;

					textFieldSearch.ResignFirstResponder();
				}
			}
		}

		void HandleAction(ActionButtonItem obj)
		{
			UIAlertView alert = new UIAlertView()
			{
				Title = "alert title",
				Message = "this is a simple alert"
			};
			alert.AddButton("OK");
			alert.Show();
		}

		partial void btnAll_TouchUpInside(NSObject sender)
		{
            viewNotFavorite.Hidden = true;

			SearchReviewTableView.Hidden = true;
			FavoriteReviewTableView.Hidden = true;
			ReviewTableView.Hidden = false;

			ViewModel.AllClickCommand.Execute();

			UIView.Animate(0.2, () =>
			{
				uiViewSlide.Center = new CGPoint(btnAll.Frame.GetMidX() - 1, btnAll.Frame.GetMaxY() - uiViewSlide.Bounds.Height + 1.5);
				uiViewSlide.Transform = CGAffineTransform.MakeTranslation(0.5f, 0.5f);
				btnAll.SetTitleColor(UIColor.FromRGB(57, 169, 246), UIControlState.Normal);
				btnFavorite.SetTitleColor(UIColor.FromRGB(167, 176, 182), UIControlState.Normal);
			});
		}

		partial void btnFavorite_TouchUpInside(NSObject sender)
		{
			SearchReviewTableView.Hidden = true;
			ReviewTableView.Hidden = true;
			FavoriteReviewTableView.Hidden = false;

			ViewModel.FavoriteClickCommand.Execute();

            if (this.ViewModel.FavoriteReviews.Count > 0)
            {
                viewNotFavorite.Hidden = true;
            }
            else
            {
                viewNotFavorite.Hidden = false;
            }

			UIView.Animate(0.2, () =>
			{
				uiViewSlide.Center = new CGPoint(btnFavorite.Frame.GetMidX(), btnFavorite.Frame.GetMaxY() - uiViewSlide.Bounds.Height + 1.5);
				uiViewSlide.Transform = CGAffineTransform.MakeTranslation(0.5f, 0.5f);
				btnFavorite.SetTitleColor(UIColor.FromRGB(57, 169, 246), UIControlState.Normal);
				btnAll.SetTitleColor(UIColor.FromRGB(167, 176, 182), UIControlState.Normal);
			});
		}
	}
}

