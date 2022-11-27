using System;
using CoreGraphics;
using UIKit;

namespace Naxam.Busuu.iOS.Profile.Common
{
	public class MyRadioButton : UIView
	{
		private CircleView circleView;
		private UILabel lbTitle;

		public bool State
		{
			get
			{
				return circleView.State;
			}
			set
			{
				circleView.State = value;
			}
		}

		public MyRadioButton(CGPoint pt, string title)
		{
			this.Frame = new CGRect(pt, new CGSize(0, 0));
			circleView = new CircleView(new CGRect(0, 0, 26, 26));
			lbTitle = new UILabel(new CGRect(26, 0, 120, 26));
            lbTitle.Font = UIFont.SystemFontOfSize(14);
			lbTitle.Text = title;
            lbTitle.TextAlignment = UITextAlignment.Left;
			this.AddSubview(circleView);
			this.AddSubview(lbTitle);
			this.BackgroundColor = UIColor.FromRGBA(1, 0, 0, 0.3f);

			UITapGestureRecognizer tapGR = new UITapGestureRecognizer(() => {
				State = !State;
			});
			this.AddGestureRecognizer(tapGR);
		}
	}

	class CircleView : UIView
	{
		private bool state = false;
		public bool State
		{
			get
			{
				return state;
			}
			set
			{
				state = value;
				this.SetNeedsDisplay();
			}
		}

		public CircleView(CGRect frame)
		{
			this.BackgroundColor = UIColor.Clear;
			this.Frame = frame;
		}

		public override void Draw(CGRect rect)
		{
			CGContext con = UIGraphics.GetCurrentContext();
			con.SetStrokeColor(UIColor.FromRGB(57, 169, 246).CGColor);
			con.SetFillColor(UIColor.FromRGB(57, 169, 246).CGColor);

			float padding = 5;
			con.AddEllipseInRect(new CGRect(padding, padding, rect.Width - 2 * padding, rect.Height - 2 * padding));
			con.StrokePath();

			if (state)
			{
				con.SetStrokeColor(UIColor.FromRGB(57, 169, 246).CGColor);
                con.SetFillColor(UIColor.FromRGB(57, 169, 246).CGColor);

                float insidePadding = 8.5f;
				con.AddEllipseInRect(new CGRect(insidePadding, insidePadding, rect.Width - 2 * insidePadding, rect.Height - 2 * insidePadding));
				con.FillPath();
			}
		}
	}
}
