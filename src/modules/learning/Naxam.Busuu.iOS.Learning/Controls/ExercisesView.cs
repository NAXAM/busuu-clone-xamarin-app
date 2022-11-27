using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using Naxam.Busuu.Learning.Models;
using UIKit;

namespace Naxam.Busuu.iOS.Learning.Controls
{
	[Register("ExcercisesView")]
	public class ExcercisesView : UIView
	{
		public ExcercisesView(IntPtr handle) : base(handle)
		{

		}

		IList<ExerciseModel> exercises;
        public event EventHandler<ExerciseClickEventArg> ExerciseClick;
		public IList<ExerciseModel> Exercises
		{
			get
			{
				return exercises;
			}

			set
			{
				if (ReferenceEquals(exercises, value)) return;

				exercises = value;
				SetData(exercises);
			}
		}

		public void SetData(IList<ExerciseModel> exercises)
		{
			nfloat x = this.Bounds.GetMinX(), y = this.Bounds.GetMinY();
			float width = 24, height = 24;
            for (int i = 0; i < exercises.Count;i++)
			{
				UIView imView = new UIView(new CGRect(x, y, 44, 44));
				UIImageView img = new UIImageView(new CGRect(10, 10, width, height));
				img.TintColor = Core.Extensions.ColorUtils.ColorFromHex(exercises[i].Color);
			
                switch (exercises[i].Type)
				{
					case ExerciseModel.ExerciseType.Memorise:
						img.Image = UIImage.FromBundle("memorise_icon"); break;
					case ExerciseModel.ExerciseType.Discover:
						img.Image = UIImage.FromBundle("meaning_icon"); break;
					case ExerciseModel.ExerciseType.Practice:
						img.Image = UIImage.FromBundle("practice_icon"); break;
					case ExerciseModel.ExerciseType.Vocabulary:
						img.Image = UIImage.FromBundle("vocabulary_icon"); break;
                    case ExerciseModel.ExerciseType.Dialogue:
						img.Image = UIImage.FromBundle("dialogue_icon"); break; 
				}

                imView.Tag = i;
				imView.ClipsToBounds = true;
				imView.Layer.CornerRadius = imView.Bounds.Size.Height / 2;
				imView.BackgroundColor = UIColor.White;
				imView.AddSubview(img);
				this.AddSubview(imView);
				x += 64;

                imView.AddGestureRecognizer(new UITapGestureRecognizer(ExerciseTapped));
			}
		}

        void ExerciseTapped(UITapGestureRecognizer sender)
        {
			var view = sender.View;
			var tempIndex = (int)view.Tag;
			var tempEx = Exercises[tempIndex];

			ExerciseClick?.Invoke(this, new ExerciseClickEventArg
			{
				Exercise = tempEx,
				ExerciseIndex = tempIndex
			});
        }
    }
}
