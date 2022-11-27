using Foundation;
using System;
using UIKit;
using ObjCRuntime;
using Naxam.Busuu.iOS.Core.Extensions;

namespace Naxam.Busuu.iOS.Core.Views
{
    public partial class BuyPremiumCell : UIView
    {
        public BuyPremiumCell (IntPtr handle) : base (handle)
        {
        }

		public static BuyPremiumCell Create()
		{
			var arr = NSBundle.MainBundle.LoadNib("BuyPremiumCell", null, null);
			var v = Runtime.GetNSObject<BuyPremiumCell>(arr.ValueAt(0));
			return v;
		}

		public override void AwakeFromNib()
		{
			var premiumShape = new RippleLayer(viewRipple, UIColor.LightGray.BrighterColor(), UIColor.White);
			var cellGesture = new UITapGestureRecognizer((UITapGestureRecognizer obj) =>
						{
							var touchLocation = obj.LocationInView(this);
							premiumShape.WillAnimateTapGesture(touchLocation);
                            BtnGo.SendActionForControlEvents(UIControlEvent.TouchUpInside);
						});
			contentView.AddGestureRecognizer(cellGesture);

            viewRipple.Layer.MasksToBounds = true;

            contentView.Layer.ShadowColor = UIColor.LightGray.CGColor;
			contentView.Layer.ShadowOpacity = 0.8f;
			contentView.Layer.ShadowRadius = 3.0f;
			contentView.Layer.ShadowOffset = new CoreGraphics.CGSize(1.0, 1.0);
			BtnGo.Layer.CornerRadius = BtnGo.Bounds.Size.Height / 2;
		}

        public UIButton BtnGo
        {
            get => btnGo;
            set => btnGo = value;
        }

        public UIView ContentView
        {
            get => contentView;
            set => contentView = value;
        }
    }
}