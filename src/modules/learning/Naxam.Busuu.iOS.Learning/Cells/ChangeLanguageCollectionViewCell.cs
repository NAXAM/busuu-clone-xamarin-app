using Foundation;
using System;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using Naxam.Busuu.iOS.Core.Converter;
using Naxam.Busuu.Core.Models;
using CoreGraphics;
using Xamarin.CircularProgress.iOS;
using MvvmCross.Platform.Converters;
using System.Globalization;

namespace Naxam.Busuu.iOS.Learning.Cells
{
    public class PercentToValueConverter : MvxValueConverter<int, double>
	{
        protected override double Convert(int value, Type targetType, object parameter, CultureInfo culture)
		{
            return (double)value / 100;
		}
	}

    public class PercentToTextConverter : MvxValueConverter<int, string>
	{
		protected override string Convert(int value, Type targetType, object parameter, CultureInfo culture)
		{
            return value + "% complete";
		}
	}

	public class LanaguageNameToTextConverter : MvxValueConverter<LanguageModel, NSAttributedString>
	{
		protected override NSAttributedString Convert(LanguageModel value, Type targetType, object parameter, CultureInfo culture)
		{
			var firstAttributes = new UIStringAttributes
			{
				Font = UIFont.BoldSystemFontOfSize(17f)
			};

            var prettyString = new NSMutableAttributedString(value.Language);

            if (value.IsCurrent)
            {
				prettyString.SetAttributes(firstAttributes.Dictionary, new NSRange(0, value.Language.Length));
			}

			return prettyString;
		}
	}


	public partial class ChangeLanguageCollectionViewCell : MvxCollectionViewCell
    {
        CircularProgress circularProgress;
        readonly MvxImageViewLoader _loaderImgLanaguage;

        public ChangeLanguageCollectionViewCell (IntPtr handle) : base (handle)
        {
            _loaderImgLanaguage = new MvxImageViewLoader(() => this.imgLan);

			this.DelayBind(() =>
			{
				var setBinding = this.CreateBindingSet<ChangeLanguageCollectionViewCell, LanguageModel>();
				setBinding.Bind(_loaderImgLanaguage).To(n => n.Flag).WithConversion(nameof(ImageUriConverter));
                setBinding.Bind(nameLan).For("FormattedText").To(n => n).WithConversion(new LanaguageNameToTextConverter());
                setBinding.Bind(circularProgress).For(n => n.Progress).To(n => n.Percent).WithConversion(new PercentToValueConverter());
                setBinding.Bind(lbPercent).To(n => n.Percent).WithConversion(new PercentToTextConverter());
                setBinding.Bind(lbPercent).For(n => n.Hidden).To(n => n.IsCurrent).WithConversion(nameof(VisibilityToHideConverter));
                setBinding.Apply();
			});
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            var frame = new CGRect(-1.5, -1.5, viewOfImg.Bounds.Width + 3, viewOfImg.Bounds.Height + 3);

		 	circularProgress = new CircularProgress(frame);
			circularProgress.StartAngle = circularProgress.EndAngle = -90;
			circularProgress.LineWidth = 4;
			circularProgress.Colors = new[]
			{
                UIColor.FromRGB(35, 131, 195).CGColor
			};

            viewOfImg.Layer.CornerRadius = viewOfImg.Bounds.Width / 2;
			viewOfImg.BackgroundColor = UIColor.FromRGB(207, 235, 252);


			viewOfImg.InsertSubview(circularProgress, 0);
        }
    }
}