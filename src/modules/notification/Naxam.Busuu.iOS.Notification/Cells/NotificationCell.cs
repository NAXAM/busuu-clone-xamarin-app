using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.Core.Converter;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.iOS.Core.Converter;

namespace Naxam.Busuu.iOS.Notification.Cells
{
	public partial class NotificationCell : MvxTableViewCell
	{
        readonly MvxImageViewLoader _loaderImageUser;

		public NotificationCell (IntPtr handle) : base(handle)
		{
            _loaderImageUser = new MvxImageViewLoader(() => this.imgUser);
            _loaderImageUser.DefaultImagePath = "res:user_avatar_placeholder.png";

			this.DelayBind(() =>
			{
				var setBinding = this.CreateBindingSet<NotificationCell, NotificationModel>();
                setBinding.Bind(_loaderImageUser).To(n => n.User.Photo);
                setBinding.Bind(lblDetail).For("FormattedText").To(n => n).WithConversion(nameof(NotifyTextToFormattedTextConverter));
                setBinding.Bind(lblTime).To(n => n.Time).WithConversion(nameof(NotificationDatetimeConverter));
                setBinding.Bind(ContentView).For(n => n.BackgroundColor).To(n => n.IsRead).WithConversion(nameof(NotifyToColorConverter));
				setBinding.Apply();	
			});
		}

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();


        }
	}
}
