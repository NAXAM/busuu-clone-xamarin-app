using Foundation;
using System;
using UIKit;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Social.ViewModels;
using CoreGraphics;
using CoreAnimation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Naxam.Busuu.iOS.Social.Cells;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.iOS.Social.Views
{
    [MvxFromStoryboard(StoryboardName = "Social")]
    public partial class DiscoverView : MvxViewController<DiscoverViewModel>
    {
        public DiscoverView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            Request = new MvxViewModelRequest<DiscoverViewModel>(null, null);

            base.ViewDidLoad();

            CollectionViewLineLayout myFlow = new CollectionViewLineLayout();
            DiscoverCollectionView.SetCollectionViewLayout(myFlow, true);

            DiscoverDataSource dSource = new DiscoverDataSource(DiscoverCollectionView, (NSString)"DiscoverCell");

            var setBinding = this.CreateBindingSet<DiscoverView, DiscoverViewModel>();
            setBinding.Bind(dSource).To(vm => vm.DiscoverData);
            setBinding.Bind(dSource).For(nameof(DiscoverDataSource.ViewDiscoverCommand)).To(vm => vm.ViewDisoverCommand);
            setBinding.Apply();

            DiscoverCollectionView.Source = dSource;
        }
    }

	public class DiscoverDataSource : MvxCollectionViewSource, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

	    IMvxCommand _viewDiscoverCommand;
		public IMvxCommand ViewDiscoverCommand
		{
			get => _viewDiscoverCommand;
			set => SetProperty(ref _viewDiscoverCommand, value);
		}

        public DiscoverDataSource(UICollectionView collectionView, string identifier) : base(collectionView, new NSString(identifier))
        {

        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (DiscoverCell)base.GetCell(collectionView, indexPath);

            cell.ViewDiscoverHandler -= HandleViewDiscover;
            cell.ViewDiscoverHandler += HandleViewDiscover;

            return cell;
        }

        void HandleViewDiscover(object sender, SocialModel e)
        {
            if (ViewDiscoverCommand?.CanExecute(e) != true) return;

            ViewDiscoverCommand.Execute(e);
        }

        void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(backingField, value)) return;

			backingField = value;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
    }

    public class CollectionViewLineLayout : UICollectionViewFlowLayout
    {
        public const float ZOOM_FACTOR = 0.12f;
        public float ACTIVE_DISTANCE;

        public CollectionViewLineLayout()
        {
            nfloat viewLR = UIScreen.MainScreen.Bounds.Size.Width * 0.8f;
            nfloat insetsTB = (UIScreen.MainScreen.Bounds.Size.Height - 84) / 6;
            nfloat insetsLR = UIScreen.MainScreen.Bounds.Size.Width * 0.145f;

            float ITEM_SIZE = (float)(viewLR - viewLR * ZOOM_FACTOR);
            ACTIVE_DISTANCE = ITEM_SIZE;
            ItemSize = new CGSize(ITEM_SIZE, insetsTB * 4);
            SectionInset = new UIEdgeInsets(insetsTB, insetsLR, insetsTB, insetsLR);
            ScrollDirection = UICollectionViewScrollDirection.Horizontal;
            MinimumLineSpacing = UIScreen.MainScreen.Bounds.Size.Width * 0.1f;
        }

        public override bool ShouldInvalidateLayoutForBoundsChange(CGRect newBounds)
        {
            return true;
        }

        public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect(CGRect rect)
        {
            var array = base.LayoutAttributesForElementsInRect(rect);
            var visibleRect = new CGRect(CollectionView.ContentOffset, CollectionView.Bounds.Size);

            foreach (var attributes in array)
            {
                if (attributes.Frame.IntersectsWith(rect))
                {
                    float distance = (float)(visibleRect.GetMidX() - attributes.Center.X);
                    float normalizedDistance = distance / ACTIVE_DISTANCE;
                    if (Math.Abs(distance) < ACTIVE_DISTANCE)
                    {
                        float zoom = 1 + ZOOM_FACTOR * (1 - Math.Abs(normalizedDistance));
                        attributes.Transform3D = CATransform3D.MakeScale(zoom, zoom, 1.0f);
                        attributes.ZIndex = 1;
                    }
                }
            }
            return array;
        }

        public override CGPoint TargetContentOffset(CGPoint proposedContentOffset, CGPoint scrollingVelocity)
        {
            float offSetAdjustment = float.MaxValue;
            float horizontalCenter = (float)(proposedContentOffset.X + (this.CollectionView.Bounds.Size.Width / 2.0));
            CGRect targetRect = new CGRect(proposedContentOffset.X, 0.0f, this.CollectionView.Bounds.Size.Width, this.CollectionView.Bounds.Size.Height);
            var array = base.LayoutAttributesForElementsInRect(targetRect);
            foreach (var layoutAttributes in array)
            {
                float itemHorizontalCenter = (float)layoutAttributes.Center.X;
                if (Math.Abs(itemHorizontalCenter - horizontalCenter) < Math.Abs(offSetAdjustment))
                {
                    offSetAdjustment = itemHorizontalCenter - horizontalCenter;
                }
            }
            return new CGPoint(proposedContentOffset.X + offSetAdjustment, proposedContentOffset.Y);
        }
    }
}