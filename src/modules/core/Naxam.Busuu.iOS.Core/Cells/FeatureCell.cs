using System;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.iOS.Core.Converter;

namespace Naxam.Busuu.iOS.Core.Cells
{
    public partial class FeatureCell : MvxTableViewCell
    {
        readonly MvxImageViewLoader imgFeatureLoader;

        public FeatureCell (IntPtr handle) : base (handle)
        {
            imgFeatureLoader = new MvxImageViewLoader(() => imgFeature);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
			this.DelayBind(() =>
			{
				var setBinding = this.CreateBindingSet<FeatureCell, PremiumFeatureModel>();
				setBinding.Bind(lbFeature).To(m => m.Feature);
				setBinding.Bind(imgFeatureLoader).To(m => m.Image).WithConversion(nameof(ImageUriConverter));
				setBinding.Apply();
			});
            imgFeature.TintColor = UIColor.FromRGB(249,155,42);
        }
    }	
}