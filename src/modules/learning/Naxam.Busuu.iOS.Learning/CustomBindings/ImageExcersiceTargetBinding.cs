using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using Naxam.Busuu.Learning.Models;
using UIKit;

namespace Naxam.Busuu.iOS.Learning.CustomBindings
{
    public class ImageExersiceTargetBinding : MvxTargetBinding
    {
        public ImageExersiceTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType
        {
            get
            {
                return typeof(string);
            }
        }

        public override MvxBindingMode DefaultMode
        {
            get
            {
                return MvxBindingMode.OneWay;
            }
        }

        public override void SetValue(object value)
        {
            if (value is List<ExerciseModel> exercises)
            {
                if (Target is UIView view)
                {
                    nfloat x = view.Bounds.GetMinX(), y = view.Bounds.GetMinY();
                    float width = 24, height = 24;
                    foreach (var exercise in exercises)
                    {
                        UIView imView = new UIView(new CGRect(x, y, 40, 40));
                        UIImageView img = new UIImageView(new CGRect(8, 8, width, height));
                        img.TintColor = Core.Extensions.ColorUtils.ColorFromHex(exercise.Color);
                        switch (exercise.Type)
                        {
                            case ExerciseModel.ExerciseType.Memorise:
                                img.Image = UIImage.FromBundle("memorise_icon"); break;
                            case ExerciseModel.ExerciseType.Discover:
                                img.Image = UIImage.FromBundle("meaning_icon"); break;
                            case ExerciseModel.ExerciseType.Practice:
                                img.Image = UIImage.FromBundle("practice_icon"); break;
                            case ExerciseModel.ExerciseType.Vocabulary:
                                img.Image = UIImage.FromBundle("vocabulary_icon"); break;
                        }

                        imView.ClipsToBounds = true;
                        imView.Layer.CornerRadius = imView.Bounds.Size.Height / 2;
                        imView.BackgroundColor = UIColor.White;
                        imView.AddSubview(img);
                        view.AddSubview(imView);
                        x += 64;
                    }
                }

            }
        }
    }
}
