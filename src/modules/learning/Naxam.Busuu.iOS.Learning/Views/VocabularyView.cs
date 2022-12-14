// This file has been autogenerated from a class added in the UI designer.

using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using Naxam.Busuu.iOS.Learning.Common;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Learning.ViewModels;
using UIKit;

namespace Naxam.Busuu.iOS.Learning.Views
{
	[MvxFromStoryboard(StoryboardName = "Learning")]
	[MvxModalPresentation(WrapInNavigationController = true)]
    public partial class VocabularyView : MvxViewController<VocabularyViewModel>, IAnswerClick
	{
		ExerciseModel Item;
		int PositionStep = 1, PositionMax;
		MemoriseBaseView ExerciseBody;

		public VocabularyView(IntPtr handle) : base (handle)
        {
		}

		void StudyNewUnit()
		{
			btnNext.Hidden = true;

			try
			{
				string exerciseName = Item.Units[PositionStep - 1].Type.ToString();

				if (exerciseName == "OrderWord")
				{
					ExerciseBody = OrderWordView.Create(Item.Units[PositionStep - 1]);
				}
				else if (exerciseName == "SelectWordImage")
				{
					ExerciseBody = SelectWordImageView.Create(Item.Units[PositionStep - 1]);
				}
				else if (exerciseName == "ConversationSentence")
				{
					ExerciseBody = ConversationSentenceView.Create(Item.Units[PositionStep - 1]);
				}
				else if (exerciseName == "MatchingSentence")
				{
					ExerciseBody = MatchingSentenceView.Create(Item.Units[PositionStep - 1]);
				}
				else if (exerciseName == "CompleteSentence")
				{
					ExerciseBody = CompleteSentenceView.Create(Item.Units[PositionStep - 1]);
				}
				else if (exerciseName == "SelectWord")
				{
					ExerciseBody = SelectWordView.Create(Item.Units[PositionStep - 1]);
				}
				else if (exerciseName == "ChooseWord")
				{
					ExerciseBody = ChooseWordView.Create(Item.Units[PositionStep - 1]);
				}
				else if (exerciseName == "FillSentence")
				{
					ExerciseBody = FillSentenceView.Create(Item.Units[PositionStep - 1]);
				}
				else if (exerciseName == "TrueFalseQuestion")
				{
					ExerciseBody = TrueFalseHearQuestionView.Create(Item.Units[PositionStep - 1]);
				}
				else if (exerciseName == "HearAndRepeat")
				{
					ExerciseBody = ExerciseSpeakView.Create(Item.Units[PositionStep - 1]);
				}

				ExerciseBody.Frame = new CGRect(0, 0, ViewExercise.Bounds.Width, ViewExercise.Bounds.Height);

				ViewExercise.AddSubview(ExerciseBody);

				lbProcess.Text = string.Format("{0}/{1}", PositionStep, PositionMax);
				viewProcessValueWidthConstraint.Constant = viewProcess.Bounds.Width / PositionMax * PositionStep;
				viewProcessValue.Layer.CornerRadius = viewProcessValue.Bounds.Height / 2;
			}
			catch
			{
				NavigationController.NavigationBarHidden = true;

				ExerciseBody = ResultView.Create();
				ExerciseBody.Frame = new CGRect(0, 0, viewEnd.Bounds.Width, viewEnd.Bounds.Height);
				viewEnd.AddSubview(ExerciseBody);
				viewEnd.Hidden = false;
			}

			ExerciseBody.Answered = this;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			NavigationController.NavigationBarHidden = true;

			viewOfTitle.Layer.ShadowRadius = 2;
			viewOfTitle.Layer.ShadowOpacity = 0.25f;
			viewOfTitle.Layer.ShadowOffset = new CGSize(0, 2);

			btnStart.Layer.CornerRadius = btnStart.Bounds.Height / 2;
			btnStart.Layer.ShadowRadius = 2;
			btnStart.Layer.ShadowOpacity = 0.25f;
			btnStart.Layer.ShadowOffset = new CGSize(0, 2);

			NavigationItem.Title = "Vocabulary";
			var s = new UIBarButtonItem(UIImage.FromBundle("back_arrow"), UIBarButtonItemStyle.Plain, null);
			NavigationItem.LeftBarButtonItem = s;
            var bindingSet = this.CreateBindingSet<VocabularyView, VocabularyViewModel>();
			bindingSet.Bind(s).To(vm => vm.GoBackCommand);
			bindingSet.Apply();
			viewProcess.Layer.CornerRadius = viewProcess.Bounds.Size.Height / 2;

			Item = this.ViewModel.Exercise;
			PositionMax = Item.Units.Count;

			btnNext.Layer.CornerRadius = btnNext.Bounds.Size.Height / 2;
			btnNext.Layer.MasksToBounds = false;
			btnNext.Layer.ShadowRadius = 2;
			btnNext.Layer.ShadowOpacity = 0.25f;
			btnNext.Layer.ShadowOffset = new CGSize(0, 2);

			if (Item != null)
			{
				LearnView.myItemExercise = Item.Units.Count;
				StudyNewUnit();
			}
		}

		partial void btnNext_Click(Foundation.NSObject sender)
		{
			ExerciseBody.RemoveFromSuperview();

			if (PositionStep < PositionMax)
			{
				PositionStep += 1;
			}
			else
			{
				PositionStep = PositionMax + 1;
			}

			StudyNewUnit();
		}

		partial void btnStart_Click(Foundation.NSObject sender)
		{
			viewStart.Hidden = true;
			NavigationController.NavigationBarHidden = false;
		}

		public void DidAnswer(UIView view)
		{
			btnNext.Hidden = false;
		}

		public void NextAnswer(UIView view)
		{
			ExerciseBody.RemoveFromSuperview();

			if (PositionStep < PositionMax)
			{
				PositionStep += 1;

			}
			else
			{
				PositionStep = PositionMax + 1;
			}

			StudyNewUnit();
		}

		public void GoLearnView(UIView view)
		{
			this.ViewModel.GoBackCommand.Execute();
		}

		public void ResetExercise(UIView view)
		{
			ExerciseBody.RemoveFromSuperview();
			this.NavigationController.NavigationBarHidden = false;
			PositionStep = 1;
			StudyNewUnit();
			viewEnd.Hidden = true;
		}
	}
}
