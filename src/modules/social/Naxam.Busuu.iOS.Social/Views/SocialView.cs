using System;
using Foundation;
using MvvmCross.iOS.Views;
using UIKit;
using ObjCRuntime;
using CoreGraphics;
using Naxam.Busuu.Social.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views.Presenters.Attributes;

namespace Naxam.Busuu.iOS.Social.Views
{
    [MvxFromStoryboard(StoryboardName = "Social")]
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = "social_tab_icon", TabName = "Social", TabSelectedIconName = "social_tab_icon_selected")]
    public partial class SocialView : MvxViewController<SocialViewModel>
	{
        bool IsAnimationViewBar;
        MvxViewController discoverView;
        MvxViewController friendsView;

		public SocialView (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			discoverView = (MvxViewController)UIStoryboard.FromName("Social", NSBundle.MainBundle).InstantiateViewController("DiscoverView");
			friendsView = (MvxViewController)UIStoryboard.FromName("Social", NSBundle.MainBundle).InstantiateViewController("FriendsView");

            AddChildViewController(discoverView);
            ViewContainer.AddSubview(discoverView.View);
			discoverView.DidMoveToParentViewController(this);

            this.CreateBinding(btnFilter).To<SocialViewModel>(vm => vm.FilterCommand).Apply();
		}
        		
        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

			discoverView.View.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, ViewContainer.Frame.Height);
			friendsView.View.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, ViewContainer.Frame.Height);
        }

		partial void ButtonDiscover_TouchUpInside(NSObject sender)
		{
			if (!IsAnimationViewBar) return;

			ButtonDiscover.SetTitleColor(UIColor.FromRGB(57, 169, 246), UIControlState.Normal);
			ButtonFriends.SetTitleColor(UIColor.FromRGB(167, 176, 182), UIControlState.Normal);
			ButtonDiscover.Enabled = false;
			ButtonFriends.Enabled = true;

            WillMoveToParentViewController(friendsView);			
            ViewContainer.WillRemoveSubview(friendsView.View);
			friendsView.RemoveFromParentViewController();

            AddChildViewController(discoverView);
			ViewContainer.AddSubview(discoverView.View);
			discoverView.DidMoveToParentViewController(this);

            IsAnimationViewBar = false;
			UIView.BeginAnimations("slideAnimation");
			UIView.SetAnimationDuration(0.3);
			UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
			UIView.SetAnimationDelegate(this);
			UIView.SetAnimationDidStopSelector(new Selector("animationDidStop:finished:context:"));
			ViewSelectForButton.Center = new CGPoint(ViewSelectForButton.Bounds.Width / 2, 43);
			UIView.CommitAnimations();
		}

		partial void ButtonFriends_TouchUpInside(NSObject sender)
		{
			if (IsAnimationViewBar) return;

			ButtonFriends.SetTitleColor(UIColor.FromRGB(57, 169, 246), UIControlState.Normal);
			ButtonDiscover.SetTitleColor(UIColor.FromRGB(167, 176, 182), UIControlState.Normal);
			ButtonDiscover.Enabled = true;
			ButtonFriends.Enabled = false;

            WillMoveToParentViewController(discoverView);
            ViewContainer.WillRemoveSubview(discoverView.View);
            discoverView.RemoveFromParentViewController();

            AddChildViewController(friendsView);
			ViewContainer.AddSubview(friendsView.View);
            friendsView.DidMoveToParentViewController(this);

			IsAnimationViewBar = true;
			UIView.BeginAnimations("AnimationViewBar");
			UIView.SetAnimationDuration(0.3);
			UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
			UIView.SetAnimationDelegate(this);
			UIView.SetAnimationDidStopSelector(new Selector("AnimationViewBar:finished:context:"));
			ViewSelectForButton.Center = new CGPoint(ViewSelectForButton.Bounds.Width + ViewSelectForButton.Bounds.Width / 2, 43);
			UIView.CommitAnimations();
		}

		[Export("AnimationViewBar:finished:context:")]
		void SlideStopped(NSString animationID, NSNumber finished, NSObject context)
		{
			if (!IsAnimationViewBar)
			{
                ViewSelectForButton.Center = new CGPoint(ViewSelectForButton.Bounds.Width / 2, 43);
			}
			else
			{
				ViewSelectForButton.Center = new CGPoint(ViewSelectForButton.Bounds.Width + ViewSelectForButton.Bounds.Width / 2, 43);
			}
		}
	}
}
