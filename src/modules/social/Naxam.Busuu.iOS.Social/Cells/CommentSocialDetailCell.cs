using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.iOS.Social.Common;
using Naxam.Busuu.iOS.Social.Views;
using UIKit;
using Naxam.Busuu.Core.Converter;

namespace Naxam.Busuu.iOS.Social.Cells
{
	public partial class CommentSocialDetailCell : MvxTableViewCell
	{
        public event EventHandler<CustomFeedbackModel> ViewSocialDetailHandler;

        readonly MvxImageViewLoader _loaderImageUser;

		public CommentSocialDetailCell (IntPtr handle) : base (handle)
		{
			_loaderImageUser = new MvxImageViewLoader(() => this.imgUserAvatar);
			_loaderImageUser.DefaultImagePath = "res:user_avatar_placeholder.png";

			this.DelayBind(() =>
            {
                var setBinding = this.CreateBindingSet<CommentSocialDetailCell, FeedbackModel>();
                setBinding.Bind(_loaderImageUser).To(f => f.User.Photo);
                setBinding.Bind(lblUserName).To(f => f.User.Name);
                setBinding.Bind(lblCountry).To(f => f.User.Country.Country);
                setBinding.Bind(lblTimePublic).To(f => f.PostedDate).WithConversion(nameof(PostedToStringConverter));
                setBinding.Bind(lblReply).To(f => f.Feedback);
                setBinding.Apply();
            });
		}

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            var bbcolor = UIColor.FromRGB(217, 217, 217);

			btnAddFriends.Layer.BorderWidth = 0.75f;
			btnAddFriends.Layer.BorderColor = bbcolor.CGColor;
			btnAddFriends.ImageEdgeInsets = new UIEdgeInsets(4, 12, 4, 12);
			btnAddFriends.Layer.CornerRadius = btnAddFriends.Frame.Height / 2;

            btnReport.ImageEdgeInsets = new UIEdgeInsets(6, 0, 6, 0);
            btnReply.ContentEdgeInsets = new UIEdgeInsets(5,18,5,18);
        }

        partial void btnAddFriends_TouchUpInside(NSObject sender)
        {
			var img = UIImage.FromBundle("friendship_request_sent.png");
			btnAddFriends.SetImage(img, UIControlState.Normal);
			btnAddFriends.SetImage(img, UIControlState.Highlighted);
        }

        partial void btnReport_TouchUpInside(NSObject sender)
        {
            SocialDetailView.messengerReport.Publish(new ShowReportDialogMessage(this));
        }

        partial void btnReply_TouchUpSide(NSObject sender)
        {
			ViewSocialDetailHandler?.Invoke(this, (CustomFeedbackModel)DataContext);
		}

        partial void btnLike_TouchUpSide(NSObject sender)
        {
            if (lblLike.TextColor != UIColor.White)
            {
				viewBtnDislike.BackgroundColor = UIColor.FromRGB(242, 245, 248);
				lblDisLike.TextColor = UIColor.FromRGB(116, 125, 131);
				imgBtnDislike.Image = UIImage.FromFile("ic_comment_thumbsdown.png");
				lblDisLike.Text = "0";

				UIView.Animate(0.15, 0, UIViewAnimationOptions.Autoreverse, () =>
				{
					//UIView.SetAnimationRepeatCount(1);
					viewBtnLike.Transform = CGAffineTransform.MakeScale(1.2f, 1.2f);
				}, LikeHandleAction);

				viewBtnLike.BackgroundColor = UIColor.FromRGB(57, 169, 246);
				lblLike.TextColor = UIColor.White;
				imgBtnLike.Image = UIImage.FromFile("ic_comment_thumbsup_selected.png");
				lblLike.Text = "1";
            }
            else
            {
                // chang biet lam j nua :))
            }
        }

        partial void btnDislike_TouchUpInside(NSObject sender)
        {
            if (lblDisLike.TextColor != UIColor.White)
            {
                viewBtnLike.BackgroundColor = UIColor.FromRGB(242, 245, 248);
                lblLike.TextColor = UIColor.FromRGB(116, 125, 131);
                imgBtnLike.Image = UIImage.FromFile("ic_comment_thumbsup.png");
                lblLike.Text = "0";

                UIView.Animate(0.15, 0, UIViewAnimationOptions.Autoreverse, () =>
                {
                //UIView.SetAnimationRepeatCount(1);
                viewBtnDislike.Transform = CGAffineTransform.MakeScale(1.2f, 1.2f);
                }, DislikeHandleAction);

                viewBtnDislike.BackgroundColor = UIColor.FromRGB(57, 169, 246);
                lblDisLike.TextColor = UIColor.White;
                imgBtnDislike.Image = UIImage.FromFile("ic_comment_thumbsdown_selected.png");
                lblDisLike.Text = "1";
            }
        }

		void LikeHandleAction()
		{
			viewBtnLike.Transform = CGAffineTransform.MakeScale(1f, 1f);
		}

        void DislikeHandleAction()
        {
            viewBtnDislike.Transform = CGAffineTransform.MakeScale(1f, 1f);
        }
    }
}
