using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.iOS.Core.Converter;

namespace Naxam.Busuu.iOS.Start.Cells
{
    public partial class LanguageTableViewCell : MvxTableViewCell
	{
        readonly MvxImageViewLoader _loaderImgLanaguage;

		public LanguageTableViewCell (IntPtr handle) : base (handle)
		{
            _loaderImgLanaguage = new MvxImageViewLoader(() => this.imgLanguage);

			this.DelayBind(() =>
			{
				var setBinding = this.CreateBindingSet<LanguageTableViewCell, LanguageModel>();
                setBinding.Bind(_loaderImgLanaguage).To(n => n.Flag).WithConversion(nameof(ImageUriConverter));
                setBinding.Bind(nameLanguage).To(n => n.Language);
				setBinding.Apply();
			});
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

            imgLanguage.Layer.CornerRadius = imgLanguage.Frame.Width / 2;
		}
	}
}
