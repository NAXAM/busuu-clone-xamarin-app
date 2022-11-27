using Foundation;
using System;
using UIKit;
using CoreGraphics;
using Naxam.Busuu.iOS.Learning.Views;
using Naxam.Busuu.Learning.Models;
using ObjCRuntime;

namespace Naxam.Busuu.iOS.Learning.Views
{
    public partial class TipView : MemoriseBaseView
    {
        public TipView (IntPtr handle) : base (handle)
        {
        }

		public static TipView Create(UnitModel item)
		{
			var arr = NSBundle.MainBundle.LoadNib("Tip", null, null);
			var v = Runtime.GetNSObject<TipView>(arr.ValueAt(0));
			v.Item = item;
			return v;
		}

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            ViewTip.Layer.ShadowRadius = 2;
            ViewTip.Layer.ShadowOpacity = 0.25f;
            ViewTip.Layer.ShadowOffset = new CGSize(0, 2);

			imgTip.Layer.ShadowRadius = 2;
			imgTip.Layer.ShadowOpacity = 0.25f;
			imgTip.Layer.ShadowOffset = new CGSize(0, 2);
        }

        partial void btnOk_TouchUpInside(NSObject sender)
        {
            Answered.NextAnswer(this);
        }
    }
}