// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Review.Views
{
	[Register ("ReviewView")]
	partial class ReviewView
	{
		[Outlet]
		UIKit.UIButton btnAll { get; set; }

		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnCloseSeach { get; set; }

		[Outlet]
		UIKit.UIButton btnFavorite { get; set; }

		[Outlet]
		UIKit.UIButton btnSearch { get; set; }

		[Outlet]
		UIKit.UITableView FavoriteReviewTableView { get; set; }

		[Outlet]
		UIKit.UITableView ReviewTableView { get; set; }

		[Outlet]
		UIKit.UITableView SearchReviewTableView { get; set; }

		[Outlet]
		UIKit.UILabel textFakeReview { get; set; }

		[Outlet]
		UIKit.UILabel textFakeSearch { get; set; }

		[Outlet]
		UIKit.UITextField textFieldSearch { get; set; }

		[Outlet]
		UIKit.UIView uiViewButton { get; set; }

		[Outlet]
		UIKit.UIView uiViewSlide { get; set; }

		[Outlet]
		UIKit.UIView viewNotFavorite { get; set; }

		[Action ("btnAll_TouchUpInside:")]
		partial void btnAll_TouchUpInside (Foundation.NSObject sender);
	
		[Action ("btnFavorite_TouchUpInside:")]
		partial void btnFavorite_TouchUpInside (Foundation.NSObject sender);

		void ReleaseDesignerOutlets ()
		{
			if (btnAll != null) {
				btnAll.Dispose ();
				btnAll = null;
			}

			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (btnCloseSeach != null) {
				btnCloseSeach.Dispose ();
				btnCloseSeach = null;
			}

			if (btnFavorite != null) {
				btnFavorite.Dispose ();
				btnFavorite = null;
			}

			if (btnSearch != null) {
				btnSearch.Dispose ();
				btnSearch = null;
			}

			if (FavoriteReviewTableView != null) {
				FavoriteReviewTableView.Dispose ();
				FavoriteReviewTableView = null;
			}

			if (ReviewTableView != null) {
				ReviewTableView.Dispose ();
				ReviewTableView = null;
			}

			if (SearchReviewTableView != null) {
				SearchReviewTableView.Dispose ();
				SearchReviewTableView = null;
			}

			if (textFakeReview != null) {
				textFakeReview.Dispose ();
				textFakeReview = null;
			}

			if (textFakeSearch != null) {
				textFakeSearch.Dispose ();
				textFakeSearch = null;
			}

			if (textFieldSearch != null) {
				textFieldSearch.Dispose ();
				textFieldSearch = null;
			}

			if (uiViewButton != null) {
				uiViewButton.Dispose ();
				uiViewButton = null;
			}

			if (uiViewSlide != null) {
				uiViewSlide.Dispose ();
				uiViewSlide = null;
			}

			if (viewNotFavorite != null) {
				viewNotFavorite.Dispose ();
				viewNotFavorite = null;
			}
		}
	}
}
