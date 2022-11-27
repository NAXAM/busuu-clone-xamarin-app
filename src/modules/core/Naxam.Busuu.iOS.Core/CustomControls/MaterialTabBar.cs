using System;
using UIKit;
using CoreGraphics;
using Foundation;
using System.Linq;
using CoreAnimation;
using System.Collections.Generic;
using FBKVOControllerNS;

namespace Naxam.Busuu.iOS.Core.CustomControls
{
	class BadgeLabel : UILabel
	{
		public BadgeLabel() : base(CGRect.Empty)
		{
			BackgroundColor = UIColor.Red;
			Layer.MasksToBounds = true;
			TextColor = UIColor.White;
			Font = UIFont.SystemFontOfSize(13);
			TextAlignment = UITextAlignment.Center;
			Hidden = true;
		}

		public override CGRect Bounds
		{
			get
			{
				return base.Bounds;
			}
			set
			{
				base.Bounds = value;
				Layer.CornerRadius = value.Height / 2;
			}
		}

		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				Hidden = value == null;
			}
		}
	}

	public interface IMaterialTabBarDelegate
	{
		void SelectedIndex(MaterialTabBar tabbar, int index);
	}

	public class MaterialTabBar : UIView
	{
		#region Private properties
		UIColor _SelectedColor;
		UIColor _UnselectedColor;
		CGSize _IconSize = new CGSize(24, 24);
        nfloat _LabelHeight = 20.5f;
		CAShapeLayer _RippleLayer = new CAShapeLayer()
		{
			AnchorPoint = new CGPoint(0.5f, 0.5f)
		};
        NSLayoutConstraint _HeightConstraint;
		static int MAIN_STACK_TAG = 1001;
        static int SUB_STACK_TAG = 2001;
        static int ICON_TAG = 201;
        static int LABEL_TAG = 202;
        static int ITEM_TAG = 301;
        static nfloat BAR_HEIGHT = 49f;


        List<BadgeLabel> _Badges = new List<BadgeLabel>();

		UIColor _RippleColor;

		UIView _RippleContainer = new UIView()
		{
			BackgroundColor = UIColor.Clear,
			ClipsToBounds = true
		};

		FBKVOController _KVOController;
		#endregion

		#region Public properties

		public UIColor RippleColor
		{
			get => _RippleColor;
			set
			{
				_RippleColor = value;
				_RippleLayer.FillColor = (value ?? UIColor.Clear).CGColor;
			}
		}

        public bool IsShowing
        {
            get => _HeightConstraint != null && _HeightConstraint.Constant.Equals(BAR_HEIGHT);
        }

		public IMaterialTabBarDelegate Delegate;

		public int SelectedIndex
		{
			get; private set;
		}
		#endregion

		public MaterialTabBar(UITabBar tabbar, UIColor selectedColor = null, UIColor unselectedColor = null) : base(CGRect.Empty)
		{
			if (tabbar == null) throw new Exception("Tabbar cannot be null");

            tabbar.Hidden = true;

			_SelectedColor = selectedColor ?? UIColor.FromRGB(14.0f / 255.0f, 122.0f / 255.0f, 254.0f / 255.0f);
			_UnselectedColor = unselectedColor ?? UIColor.Gray;
			_RippleColor = _SelectedColor.ColorWithAlpha(0.2f);

			BackgroundColor = UIColor.White;
			Layer.ShadowColor = UIColor.Black.CGColor;
			Layer.ShadowOpacity = 0.25f;
			Layer.ShadowOffset = new CGSize(0, -2);

            var sview = tabbar.Superview;
			sview.Add(this);
			TranslatesAutoresizingMaskIntoConstraints = false;
            var marginsGuide = sview.LayoutMarginsGuide;
            LeadingAnchor.ConstraintEqualTo(sview.LeadingAnchor, -8).Active = true;
            BottomAnchor.ConstraintEqualTo(marginsGuide.BottomAnchor).Active = true;
            TrailingAnchor.ConstraintEqualTo(sview.TrailingAnchor, 8).Active = true;
            _HeightConstraint = HeightAnchor.ConstraintEqualTo(BAR_HEIGHT);
            _HeightConstraint.Active = true;
            sview.AddConstraint(_HeightConstraint);

			_RippleContainer.ClipsToBounds = true;
			AddSubview(_RippleContainer);
			_RippleContainer.Layer.AddSublayer(_RippleLayer);

			var mainStack = new UIStackView()
			{
				Axis = UILayoutConstraintAxis.Horizontal,
				Distribution = UIStackViewDistribution.FillEqually,
				Alignment = UIStackViewAlignment.Fill,
				ClipsToBounds = true,
				Tag = MAIN_STACK_TAG
			};
			mainStack.Layer.MasksToBounds = true;

			AddSubview(mainStack);
			mainStack.TranslatesAutoresizingMaskIntoConstraints = false;
			mainStack.TopAnchor.ConstraintEqualTo(LayoutMarginsGuide.TopAnchor, -5).Active = true;
			mainStack.LeadingAnchor.ConstraintEqualTo(LayoutMarginsGuide.LeadingAnchor, -8).Active = true;
			mainStack.BottomAnchor.ConstraintEqualTo(LayoutMarginsGuide.BottomAnchor, 6).Active = true;
			mainStack.TrailingAnchor.ConstraintEqualTo(LayoutMarginsGuide.TrailingAnchor, 8).Active = true;

			_RippleLayer.Path = UIBezierPath.FromOval(new CGRect(0, 0, 1, 1)).CGPath;
			_RippleLayer.FillColor = _RippleColor.CGColor;

			if (tabbar.SelectedItem != null)
			{
				SelectedIndex = tabbar.Items.ToList().IndexOf(tabbar.SelectedItem);
			}
			else
			{
				SelectedIndex = 0;
			}

			UserInteractionEnabled = true;

			_KVOController = new FBKVOController(this);

			UpdateItems(tabbar.Items);
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			var touch = touches.First() as UITouch;
			var touchedPoint = touch.LocationInView(this);
			var itemSize = Bounds.Size.Width / ViewWithTag(MAIN_STACK_TAG).Subviews.Length;
			var index = Math.Floor(touchedPoint.X / itemSize);
			SetSelectedIndex((int)index, true);
			Delegate?.SelectedIndex(this, (int)index);
			base.TouchesEnded(touches, evt);
		}

		public void UpdateItems(UITabBarItem[] items)
		{
			_KVOController.UnobserveAll();
			var mainStack = ViewWithTag(MAIN_STACK_TAG) as UIStackView;
			foreach (UIView sview in mainStack.Subviews)
			{
				sview.RemoveFromSuperview();
			}
			foreach (BadgeLabel label in _Badges)
			{
				label.RemoveFromSuperview();
			}
			_Badges.Clear();

			var index = 0;
			foreach (UITabBarItem item in items)
			{
				var stack = new UIStackView()
                {
                    Axis = UILayoutConstraintAxis.Vertical,
                    Distribution = UIStackViewDistribution.Fill,
					Alignment = UIStackViewAlignment.Center,
                    Tag = index + SUB_STACK_TAG
				};

                var isSelected = (index == SelectedIndex);

				var iv = new UIImageView(new CGRect(CGPoint.Empty, _IconSize))
				{
					TintColor = isSelected ? _SelectedColor : _UnselectedColor,
					ContentMode = UIViewContentMode.ScaleAspectFit,
                    Tag = ICON_TAG
				};

				UIImage image = null;
				UIImage highlightedImage = null;
				if (isSelected)
				{
					image = item.SelectedImage ?? item.Image;
					highlightedImage = item.Image ?? item.SelectedImage;
				}
				else
				{
					image = item.Image ?? item.SelectedImage;
					highlightedImage = item.SelectedImage ?? item.Image;
				}

				iv.Image = image?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
				iv.HighlightedImage = highlightedImage?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
				stack.AddArrangedSubview(iv);
				iv.TranslatesAutoresizingMaskIntoConstraints = false;
				iv.HeightAnchor.ConstraintGreaterThanOrEqualTo(_IconSize.Height).Active = true;

				var label = new UILabel
				{
					Text = item.Title,
					TextAlignment = UITextAlignment.Center,
					Font = UIFont.SystemFontOfSize(10),
					AdjustsFontSizeToFitWidth = true,
					MinimumScaleFactor = 0.5f,
					TextColor = isSelected ? _SelectedColor : _UnselectedColor,
                    Tag = LABEL_TAG,
					Hidden = !isSelected
				};
				stack.AddArrangedSubview(label);

				mainStack.AddArrangedSubview(stack);

				var badge = new BadgeLabel();
				AddSubview(badge);
				badge.TranslatesAutoresizingMaskIntoConstraints = false;
				badge.CenterXAnchor.ConstraintEqualTo(iv.RightAnchor).Active = true;
				badge.TopAnchor.ConstraintEqualTo(LayoutMarginsGuide.TopAnchor, -5).Active = true;
				badge.WidthAnchor.ConstraintGreaterThanOrEqualTo(badge.HeightAnchor).Active = true;
				badge.Text = item.BadgeValue;
				_Badges.Add(badge);

                item.Tag = ITEM_TAG + index;

				_KVOController.Observe(item, "badgeValue", NSKeyValueObservingOptions.New, UpdateBadgeForItem);

                index++;
			}
		}

		void UpdateBadgeForItem(NSObject arg0, NSObject arg1, NSDictionary<NSString, NSObject> arg2)
		{
			if (arg1 is UITabBarItem item)
			{
                UpdateBadge((int)item.Tag - ITEM_TAG, item.BadgeValue);
			}
		}

		public void SetSelectedIndex(int index, bool animated)
		{
			var mainStack = ViewWithTag(MAIN_STACK_TAG);

			if (index == SelectedIndex || index < 0 || index >= mainStack.Subviews.Length)
			{
				return;
			}
            var oldIV = mainStack.ViewWithTag(SelectedIndex + SUB_STACK_TAG).ViewWithTag(ICON_TAG) as UIImageView;
			var oldLabel = mainStack.ViewWithTag(SelectedIndex + SUB_STACK_TAG).ViewWithTag(LABEL_TAG) as UILabel;
			var newStack = mainStack.ViewWithTag(index + SUB_STACK_TAG);
			var newIV = newStack.ViewWithTag(ICON_TAG) as UIImageView;
			var newLabel = newStack.ViewWithTag(LABEL_TAG) as UILabel;
			var width = Bounds.Size.Width / mainStack.Subviews.Length;
			var oldCache = oldIV.Image;
			var newCache = newIV.Image;
			Action finalState = () =>
			{
				oldIV.TintColor = _UnselectedColor;
				oldLabel.TextColor = _UnselectedColor;
				oldLabel.Hidden = true;
				oldIV.Image = oldIV.HighlightedImage;
				oldIV.HighlightedImage = oldCache;

				newIV.TintColor = _SelectedColor;
				newLabel.TextColor = _SelectedColor;
				newLabel.Hidden = false;
				newIV.Image = newIV.HighlightedImage;
				newIV.HighlightedImage = newCache;

				if (animated)
				{
					_RippleLayer.AffineTransform = CGAffineTransform.MakeScale(width, width);
				}
			};
			SelectedIndex = index;
			if (animated)
			{
				_RippleContainer.Frame = new CGRect(width * index, 0, width, Bounds.Height);
				_RippleLayer.Frame = new CGRect(_RippleContainer.Bounds.Size.Width / 2, _RippleContainer.Bounds.Size.Height / 2, 1, 1);
				_RippleContainer.Hidden = false;

				UIView.AnimateNotify(0.3, finalState, (finished) =>
				{
					_RippleContainer.Hidden = true;
					_RippleLayer.AffineTransform = CGAffineTransform.MakeIdentity();
				});
			}
			else
			{
				finalState.Invoke();
			}
		}

		public void UpdateBadge(int index, string badgeValue)
		{
			if (index < 0 || index >= _Badges.Count) return;
			_Badges[index].Text = badgeValue;
		}

        public void Toggle(bool animated) {
            _HeightConstraint.Constant = BAR_HEIGHT - _HeightConstraint.Constant;
            Superview?.UpdateConstraints();
            if (animated) {
				UIView.Animate(0.2, () =>
				{
					Superview?.LayoutIfNeeded();
				});
            }
            else {
                Superview?.LayoutIfNeeded();
            }
        }
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (_KVOController != null)
			{
				_KVOController.UnobserveAll();
				_KVOController.Dispose();
				_KVOController = null;
			}
		}
	}
}
