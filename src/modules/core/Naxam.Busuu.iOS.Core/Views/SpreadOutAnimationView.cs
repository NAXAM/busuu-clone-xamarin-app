using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Naxam.Busuu.iOS.Core.Views
{
    public class SpreadOutAnimationView: UIView
    {
		struct ScaleType
		{
			public static string Scale = "transform.scale";
			public static string Up = "scaleUp";
			public static string Down = "scaleDown";
		}

		public double AnimationDuration = 0.35;

		public Action AnimationDidStartFunc;
		public Action<bool> AnimationDidStopFunc;


		CAShapeLayer Shape;
		nfloat Radius = 0.0f;
		UIColor DefaultColor { get; set; }
		SpreadOutAnimationViewDelegate AnimationDelegate { get; set; }

		private UIView _ParentView
		{
			get;
			set;
		}
		public UIView ParentView
		{
			get
			{
				return _ParentView;
			}
			set
			{
				_ParentView = value;
				DefaultColor = value?.BackgroundColor;
				if (_ParentView != null)
				{
					_ParentView.Layer.MasksToBounds = true;
					_ParentView.Layer.InsertSublayer(Shape, 0);
				}
			}
		}
        public SpreadOutAnimationView(UIView view = null, UIColor color = null): base(CGRect.Empty)
        {
			Shape = new CAShapeLayer();
			CommonInit(view);
        }

		public SpreadOutAnimationView(NSCoder coder) : base(coder) { }

		public override void AwakeFromNib()
		{
			CommonInit(ParentView ?? Superview);
			base.AwakeFromNib();
		}

        private void CommonInit(UIView parentView= null)
        {
            ParentView = parentView;
			Layer.BorderWidth = 0.5f;
			Layer.BorderColor = UIColor.White.CGColor;
			Layer.CornerRadius = Frame.Size.Height / 2;
			Shape.FillColor = DefaultColor.CGColor;
			Shape.MasksToBounds = true;

			AnimationDelegate = new SpreadOutAnimationViewDelegate();
			AnimationDelegate.AnimationDidStartFunc = () =>
			{
				if (ParentView != null)
				{
                    ParentView.BackgroundColor = DefaultColor;
					AnimationDidStartFunc?.Invoke();
				}
			};
			AnimationDelegate.AnimationDidStopFunc = (finished) =>
			{
				if (finished && ParentView != null)
				{
					ParentView.BackgroundColor = UIColor.Clear;
				}
				AnimationDidStopFunc?.Invoke(finished);
			};
        }
    }


    public class SpreadOutAnimationViewDelegate: CAAnimationDelegate
    {

		public Action AnimationDidStartFunc;
		public Action<bool> AnimationDidStopFunc;

		[Export("animationDidStart:"),]
		public override void AnimationStarted(CAAnimation anim)
		{
			AnimationDidStartFunc?.Invoke();
		}

		[Export("animationDidStop:finished:"),]
		public override void AnimationStopped(CAAnimation anim, bool finished)
		{
			AnimationDidStopFunc?.Invoke(finished);
		}
    }
}
