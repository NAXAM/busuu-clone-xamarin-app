using Foundation;
using System;
using UIKit;
using Naxam.Busuu.Learning.Models;
using ObjCRuntime;
using CoreGraphics;

namespace Naxam.Busuu.iOS.Learning.Views
{
    public partial class ResultView : MemoriseBaseView
    {
        NSTimer update_timer;
        int rk;
       
        public ResultView (IntPtr handle) : base (handle)
        {
         
        }

		public static ResultView Create()
		{
			var arr = NSBundle.MainBundle.LoadNib("Result", null, null);
            var v = Runtime.GetNSObject<ResultView>(arr.ValueAt(0));    
			return v;
		}

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            viewPoint.Layer.CornerRadius = viewPoint.Bounds.Height / 2;

            btnContinue.Layer.CornerRadius = btnContinue.Bounds.Height / 2;
            btnContinue.Layer.ShadowRadius = 2;
            btnContinue.Layer.ShadowOpacity = 0.25f;
            btnContinue.Layer.ShadowOffset = new CGSize(0, 2);

            lblPointMax.Text = "out of " + LearnView.myItemExercise.ToString();
            lblPointMin.Text = "You need to score at least " + (LearnView.myItemExercise - 1).ToString() + " to pass";

            Random ran = new Random();

            if (update_timer == null)
            {
                InvokeOnMainThread(() =>
                {
					update_timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(0.25), delegate
					{
						rk += 1;
						lblPoint.Text = rk.ToString();

						if (rk >= LearnView.myPoint)
						{
							update_timer.Invalidate();
							update_timer = null;
						}
					});
                });
            }

            UIView.Animate(0.25, 0, UIViewAnimationOptions.CurveEaseIn | UIViewAnimationOptions.Repeat, () =>
            {
                UIView.SetAnimationRepeatCount(LearnView.myPoint);
                viewPoint.Transform = CGAffineTransform.MakeTranslation(5f, 0);
            }, () =>
            {
                viewPoint.Transform = CGAffineTransform.MakeTranslation(0, 0);
                if (update_timer != null)
                {
                    update_timer.Invalidate();
                    update_timer = null;
                }
            });
        }

        partial void btnContinue_TouchUpInside(NSObject sender)
        {
            Answered.GoLearnView(this);
        }

        partial void btnTryAgain_TouchUpInside(NSObject sender)
        {
            Answered.ResetExercise(this);
            LearnView.myPoint = 0;
        }
    }
}