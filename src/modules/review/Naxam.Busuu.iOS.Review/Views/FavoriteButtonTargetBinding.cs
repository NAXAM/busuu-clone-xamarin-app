using System;
using System.Reflection;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using UIKit;

namespace Naxam.Busuu.iOS.Review.Views
{
    public class FavoriteButtonTargetBinding : MvxPropertyInfoTargetBinding<UIButton>
    {
        public FavoriteButtonTargetBinding(object target, PropertyInfo targetPropertyInfo) : base(target, targetPropertyInfo)
        {
            this.View.TouchUpInside += HandleTouchUpInside;
        }

        bool _isFavorite;

        private void HandleTouchUpInside(object sender, EventArgs e)
        {
			var view = this.View;
			if (view == null)
			{
				return;
			}
			FireValueChanged(view.ImageForState(UIControlState.Normal));
        }

        public override MvxBindingMode DefaultMode
        {
           get { return MvxBindingMode.TwoWay; }
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
			if (isDisposing)
			{
				var view = this.View;
				if (view != null)
				{
					view.TouchUpInside -= HandleTouchUpInside;
				}
			}
        }
    }
}
