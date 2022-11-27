using System;
using AVFoundation;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.Core.Messager;
using Naxam.Busuu.iOS.Review.Views;
using Naxam.Busuu.Review.Models;
using Naxam.Busuu.iOS.Core.Converter;
using UIKit;
using Naxam.Busuu.iOS.Core.Converter.ReviewTableViewCell;

namespace Naxam.Busuu.iOS.Review.Cells
{
    public partial class ReviewTableViewCell : MvxTableViewCell
    {
        public event EventHandler<ReviewModel> StarTapHandler;
        public event EventHandler<ReviewModel> TapHandler;

        AVAudioPlayer _ringtoneAudioPlayer;

        readonly MvxImageViewLoader _loaderImageUser;
        readonly MvxImageViewLoader _loaderImgStar;
        readonly MvxImageViewLoader _loaderImgStrengthLevel;

        public ReviewTableViewCell(IntPtr handle) : base(handle)
        {
            _loaderImageUser = new MvxImageViewLoader(() => this.imgWord);
			_loaderImageUser.DefaultImagePath = "res:user_avatar_placeholder2.png";

            _loaderImgStar = new MvxImageViewLoader(() => this.imgStar);
			_loaderImgStar.DefaultImagePath = "res:Stars/grey_star2.png";

            _loaderImgStrengthLevel = new MvxImageViewLoader(() => this.imgStrength);
			_loaderImgStrengthLevel.DefaultImagePath = "res:strength_0";

			this.DelayBind(() =>
			{
				var setBinding = this.CreateBindingSet<ReviewTableViewCell, ReviewModel>();
                setBinding.Bind(_loaderImageUser).To(n => n.ImgWord);
                setBinding.Bind(_loaderImgStrengthLevel).To(n => n.StrengthLevel).WithConversion(nameof(StrengthLevelToImageConverter));
                setBinding.Bind(lbTitle).To(n => n.Title);
                setBinding.Bind(lbSubtitle).To(n => n.SubTitle);
                setBinding.Bind(lblTitleSample).To(n => n.Sample.Title);
                setBinding.Bind(lblSubtitleSample).To(n => n.Sample.SubTitle);
                setBinding.Bind(_loaderImgStar).To(n => n.IsFavorite).WithConversion(nameof(IsFavoriteToImageStarConverter));
                setBinding.Bind(viewSample).For(n => n.Hidden).To(n => n.IsRead).WithConversion(nameof(VisibilityToHideConverter));
                setBinding.Bind(viewSampleHeithConstraint).For(n => n.Constant).To(n => n.IsRead).WithConversion(nameof(IsCheckToViewSampleHeithConverter));
                setBinding.Bind(viewSampleTopConstraint).For(n => n.Constant).To(n => n.IsRead).WithConversion(nameof(IsCheckToViewSampleTopConverter));
				setBinding.Apply();
			});
        }

        partial void btnStar_TouchUpInside(NSObject sender)
        {
            StarTapHandler?.Invoke(this, (ReviewModel)DataContext);
        }

        partial void btnTap_TouchUpInside(NSObject sender)
        {
            TapHandler?.Invoke(this, (ReviewModel)DataContext);
			ReviewView.messengerReset.Publish(new ResetTableViewMessager(this));
        }

        partial void btnPlay_TouchUpInside(NSObject sender)
        {
            _ringtoneAudioPlayer.Play();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            imgWord.Layer.CornerRadius = 3;

            viewSample.Layer.ShadowRadius = 2;
            viewSample.Layer.ShadowOpacity = 0.25f;
            viewSample.Layer.ShadowOffset = new CGSize(0, 2);

            _ringtoneAudioPlayer = AVAudioPlayer.FromUrl(NSUrl.FromFilename("Nokia-tune-Nokia-tune.mp3"));
            _ringtoneAudioPlayer.NumberOfLoops = 0;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            NSMutableAttributedString prettyString;

			var firstAttributes = new UIStringAttributes
			{
				ForegroundColor = UIColor.FromRGB(57, 169, 246)
			};

			string textSearch = ReviewView.textSearch;
			
            for (int i = 0; i < 4; i++)
            {
                int abcde = 0;
                string textBoss = "";

                if (i == 0)
                {
                    prettyString = new NSMutableAttributedString(lbTitle.Text);
                    textBoss = lbTitle.Text;
                }
                else if (i == 1)
                {
                    prettyString = new NSMutableAttributedString(lbSubtitle.Text);
                    textBoss = lbSubtitle.Text;
                }
				else if (i == 2)
				{
                    prettyString = new NSMutableAttributedString(lblTitleSample.Text);
                    textBoss = lblTitleSample.Text;
				}
				else
				{
                    prettyString = new NSMutableAttributedString(lblSubtitleSample.Text);
                    textBoss = lblSubtitleSample.Text;
				}

                for (int j = 0; j < textBoss.Length; j++)
				{
					int abcd = textBoss.IndexOf(textSearch, abcde);

					if (abcd != -1)
					{
						prettyString.SetAttributes(firstAttributes.Dictionary, new NSRange(abcd, textSearch.Length));
						abcde = abcd + textSearch.Length;
					}
				}

				if (i == 0)
				{
					lbTitle.AttributedText = prettyString;
				}
				else if (i == 1)
				{
                    lbSubtitle.AttributedText = prettyString;
				}
				else if (i == 2)
				{
                    lblTitleSample.AttributedText = prettyString;
				}
				else
				{
                    lblSubtitleSample.AttributedText = prettyString;
				}
            }
        }
    }
}       
