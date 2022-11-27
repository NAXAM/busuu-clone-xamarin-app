using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Naxam.Busuu.IOS.Review.Floaty
{
    public class ActionButtonItem : NSObject
    {
        /// The action the item should perform when tapped
        public Action<ActionButtonItem> ActionPerform;

        /// Description of the item's action
        string _text;
        string text
        {
            get => _text;

            set
            {
                _text = value;
            }
        }
        /// View that will hold the item's button and label
        public UIView View;

        /// Label that contain the item's *text*
        UILabel Label;

        /// Main button that will perform the defined action
        UIButton Button;

        /// Size needed for the *view* property presente the item's content
        CGSize ViewSize = new CGSize(width: 200, height: 35);

        /// Button's size by default the button is 35x35
        CGSize ButtonSize = new CGSize(width: 35, height: 35);

        /**
            :param: title Title that will be presented when the item is active
            :param: image Item's image used by the it's button
        */
        public ActionButtonItem(string optionalTitle, UIImage image, UIColor colorButton)
        {
            View = new UIView(new CGRect(new CGPoint(0, 0), ViewSize));

            View.Alpha = 0;

            View.UserInteractionEnabled = true;

            View.BackgroundColor = UIColor.Clear;

            Button = new UIButton(UIButtonType.Custom);
            Button.Frame = new CGRect(new CGPoint(x: ViewSize.Width - ButtonSize.Width, y: 0), ButtonSize);
            Button.Layer.CornerRadius = ButtonSize.Height / 2;
            Button.Layer.ShadowOpacity = 1;
            Button.Layer.ShadowRadius = 2;
            Button.Layer.ShadowOffset = new CGSize(width: 1, height: 1);
            Button.Layer.ShadowColor = UIColor.Gray.CGColor;
            Button.BackgroundColor = colorButton;
            View.AddSubview(Button);
            Button.TouchUpInside += (sender, e) => { ActionPerform?.Invoke(this); };

            if (image != null)
            {
                Button.SetImage(image, UIControlState.Normal);
            }
            text = optionalTitle;
            if (!String.IsNullOrEmpty(text))
            {
                Label = new UILabel();
                Label.Font = UIFont.FromName("HelveticaNeue-Medium", 12);
                Label.TextColor = UIColor.Black;
                Label.TextAlignment = UITextAlignment.Right;
                Label.Text = text;
                Label.AddGestureRecognizer(new UITapGestureRecognizer((UITapGestureRecognizer obj) => { ActionPerform?.Invoke(this); }));
                Label.SizeToFit();
                Label.Center = new CGPoint(Button.Center.X - 25 - Label.Bounds.Size.Width / 2, Button.Center.Y);
                UIButton button = new UIButton(new CGRect(new CGPoint(Label.Frame.Left, Label.Frame.Top), new CGSize(Label.Bounds.Size.Width + 40, 20)));
                button.BackgroundColor = UIColor.Clear;
                button.TouchUpInside += (sender, e) => { ActionPerform?.Invoke(this); };
                View.AddSubview(Label);
                View.AddSubview(button);
            }
        }
    }
}