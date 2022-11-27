using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Naxam.Busuu.IOS.Review.Floaty
{
    public class ActionButton : NSObject
    {
        public Action<ActionButton> Action;
        /// The action the button should perform when tapped

        /// The button's background color : set default color and selected color

        UIColor _backgroundColor = UIColor.Orange;
        public UIColor BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
            }
        }

        /// The button's background color : set default color
        UIColor backgroundColorSelected = UIColor.Orange;

        /// Indicates if the buttons is active (showing its items)
        bool Active = false;

        /// An array of items that the button will present

        ActionButtonItem[] _items;
        ActionButtonItem[] items
        {
            get 
            {
                return _items; 
            }
            set
            {
                if (_items != null)
                {
                    foreach (ActionButtonItem item in _items)
                    {
                        item.View.RemoveFromSuperview();
                    }
                }
                    _items = value;
                if (_items != null)
                {
                    PlaceButtonItems();
                    ShowActive(true);
                }
            }
        }

        /// The button that will be presented to the user
        UIButton FloatButton;

        /// View that will hold the placement of the button's actions
        UIView ContentView;

        /// View where the *floatButton* will be displayed
        UIView ParentView;

        /// Blur effect that will be presented when the button is active
        UIVisualEffectView blurVisualEffect;

        // Distance between each item action
        nfloat ItemOffset = -55;

        /// the float button's radius
        nfloat FloatButtonRadius = 50;

        public ActionButton(UIView view, ActionButtonItem[] itemss) : base()
        {
            this.ParentView = view;
            var bounds = view.Bounds;
            FloatButton = new UIButton(UIButtonType.Custom);
            FloatButton.Layer.CornerRadius = new nfloat(FloatButtonRadius / 2);
            FloatButton.Layer.ShadowOpacity = 1;
            FloatButton.Layer.ShadowRadius = 2;
            FloatButton.Layer.ShadowOffset = new CGSize(width: 1, height: 1);
            FloatButton.Layer.ShadowColor = UIColor.Gray.CGColor;
            FloatButton.SetTitle("+", UIControlState.Normal);
            FloatButton.SetImage(null, UIControlState.Normal);
            FloatButton.BackgroundColor = this.BackgroundColor;
            FloatButton.TitleLabel.Font = UIFont.FromName("HelveticaNeue-Light",35);
            FloatButton.ContentEdgeInsets = new UIEdgeInsets(top: 0, left: 0, bottom: 8, right: 0);
            FloatButton.UserInteractionEnabled = true;
            FloatButton.TranslatesAutoresizingMaskIntoConstraints = false;

            FloatButton.TouchDown += (object sender, EventArgs e) => {
                animatePressingWithScale(0.2f);
            };
            FloatButton.TouchUpInside += (object sender, EventArgs e) => 
            {
                animatePressingWithScale(0.2f);
                if(Action!=null){ Action.Invoke(this);}
            };

			ParentView.AddSubview(FloatButton);

            ContentView = new UIView(bounds);
            blurVisualEffect = new UIVisualEffectView(UIBlurEffect.FromStyle(UIBlurEffectStyle.ExtraLight));
            blurVisualEffect.Layer.Opacity = 0.6f;
            blurVisualEffect.Frame = ContentView.Frame;
            ContentView.AddSubview(blurVisualEffect);

            this.items = itemss;
            var tap = new UITapGestureRecognizer((UITapGestureRecognizer obj) => {
                if(Active){Toggle();}
            });
            this.ContentView.AddGestureRecognizer(tap);
            this.InstallConstraints();
		}

        //MARK: - Set Methods
        public void SetTitle(string title, UIControlState state)
        {
            FloatButton.SetImage(null, state);
            FloatButton.SetTitle(title, state);
            FloatButton.ContentEdgeInsets = new UIEdgeInsets(top: 0, left: 0, bottom: 8, right: 0);
        }

        void SetImage(UIImage image, UIControlState state)
        {
            SetTitle(null, state);
            FloatButton.SetImage(image, state);
            FloatButton.AdjustsImageWhenHighlighted = false;
            FloatButton.ContentEdgeInsets = UIEdgeInsets.Zero;
        }

        //MARK: - Auto Layout Methods
        /**
            Install all the necessary constraints for the button. By the default the button will be placed at 15pts from the bottom and the 15pts from the right of its *parentView*
*/
        void InstallConstraints()
        {
			var views = new NSMutableDictionary();
			views.Add(new NSString("FloatButton"), FloatButton);
            var metric = new NSMutableDictionary();
            metric.Add(new NSString("FloatButtonRadius"), NSNumber.FromNFloat(FloatButtonRadius));

			var width = NSLayoutConstraint.FromVisualFormat("H:[FloatButton(FloatButtonRadius)]-16-|", NSLayoutFormatOptions.AlignAllCenterX, metric, views);
			var height = NSLayoutConstraint.FromVisualFormat("V:[FloatButton(FloatButtonRadius)]-64-|", NSLayoutFormatOptions.AlignAllCenterX, metric, views);
			ParentView.AddConstraints(width);
			ParentView.AddConstraints(height);
		}

        //MARK: - Custom Methods
        /**
            Presents or hides all the ActionButton's actions
            */
        public void ToggleMenu()
        {
            this.PlaceButtonItems();
            this.Toggle();
        }

        //MARK: - Action Button Items Placement
        /**
            Defines the position of all the ActionButton's actions
*/
        void PlaceButtonItems()
        {
            if (items != null)
            {
                var y = FloatButton.Center.Y;
                foreach (ActionButtonItem item in items)
                {
                    item.View.Center = new CGPoint(FloatButton.Center.X-83, y);
                    item.View.RemoveFromSuperview();
                    this.ContentView.AddSubview(item.View);
                    y -= 50;
					UIView.BeginAnimations("slideAnimation");
					UIView.SetAnimationDuration(0.3f);
					UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
					UIView.SetAnimationDelegate(this);
					item.View.Center = new CGPoint(FloatButton.Center.X - 83, y);
					UIView.CommitAnimations();
                }
            }
        }

        //MARK - Float Menu Methods
        /**
            Presents or hides all the ActionButton's actions and changes the *active* state
*/
        void Toggle()
        {
            AnimateMenu();
            ShowBlur();
            Active = !this.Active;
            FloatButton.BackgroundColor = this.Active ? backgroundColorSelected : BackgroundColor;
            FloatButton.Selected = this.Active;
        }

        void AnimateMenu()
        {
            var rotation = this.Active ? 0 : (nfloat)Math.PI / 4;
            UIView.Animate(0.1, 0, UIViewAnimationOptions.AllowAnimatedContent, () =>
            {
                if (FloatButton.ImageView?.Image == null)
                {
                    FloatButton.Transform = CGAffineTransform.MakeRotation(rotation);
                }
                ShowActive(false);
            }, () =>
            {
                if (!Active)
                {
                    HideBlur();
                }
            });
        }

        void ShowActive(bool active)
        {
            if (Active == active)
            {
                ContentView.Alpha = (nfloat)1.0;
                if (items != null)
                {
                    for (int index = 0; index < items.Length; index++)
                    {
                        var offset = index + 1;
                        var translation = ItemOffset * offset;
                        items[index].View.Transform.Translate(0, translation);
                        items[index].View.Alpha = 1;
                    }
                }
            }
            else
            {
                this.ContentView.Alpha = 0.0f;
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        item.View.Transform.Translate(0, 0);
                        item.View.Alpha = 0;
                    }
                }
            }
        }

        void ShowBlur()
        {
            ParentView.InsertSubviewBelow(ContentView, FloatButton);
        }

        void HideBlur()
        {
            this.ContentView.RemoveFromSuperview();
        }

        /**
            Animates the button pressing, by the default this method just scales the button down when it's pressed and returns to its normal size when the button is no longer pressed

            - parameter scale: how much the button should be scaled
*/
        void animatePressingWithScale(nfloat scale)
        {
            UIView.Animate(0.1, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
            {
                FloatButton.Transform.Scale(scale, scale);
            }, null);
        }

    }
}
