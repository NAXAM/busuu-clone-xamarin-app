using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.iOS.Learning.Cells;
using UIKit;

namespace Naxam.Busuu.iOS.Learning.Common
{
    public class ChangeLanguageDataSource : MvxCollectionViewSource
    {
		public ChangeLanguageDataSource(UICollectionView collectionView, string identifier) : base(collectionView, new NSString(identifier))
        {

		}

		public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
		{
            return (ChangeLanguageCollectionViewCell)base.GetCell(collectionView, indexPath);
		}

        //[Export("collectionView:layout:sizeForItemAtIndexPath:")]
        //public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        //{
        //    //var sid = (UIScreen.MainScreen.Bounds.Width - (54 * 2) - 39) / 2;
        //    //return new CGSize(sid, sid * 1.78);
        //}
    }
}
