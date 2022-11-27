using System;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Learning.Services;
using System.Collections.Generic;

namespace Naxam.Busuu.Learning.ViewModels
{
    public class DialogueViewModel : MvxViewModel
    {
		readonly ILearningService learningService;
		ExerciseModel _exercise;

        public ExerciseModel Exercise
        {
            get { return _exercise; }
            set
            {
                if (_exercise != value)
                {
                    _exercise = value;
                    RaisePropertyChanged();
                }
            }
        }
        public DialogueViewModel(ILearningService learningService)
        {
            this.learningService = learningService;
        }
        

        protected async override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            Exercise = new ExerciseModel
            {
                Type = ExerciseModel.ExerciseType.Dialogue,
                HasSample = true
            };
            Exercise.Units = await learningService.GetUnitByExercise(Exercise);
            if (Exercise.HasSample)
            {
                ShowViewModel<DialogueNormalListSentenceViewModel>();
            }
            else
            {
                ShowViewModel<DialogueFillListSentenceViewModel>();
            }
        }
		//public void Init(int id)
		//{

		//}

		IMvxCommand _GoBackCommand;
		public IMvxCommand GoBackCommand
		{
			get { return _GoBackCommand = _GoBackCommand ?? new MvxCommand(() => Close(this)); }
		}

		IMvxCommand _StartCommand;

		public IMvxCommand StartCommand
		{
			get { return _StartCommand = _StartCommand ?? new MvxCommand(RunNextCommand); }

		}

		void RunNextCommand()
		{
            ShowViewModel<DialogueNormalListSentenceViewModel>();
		}
    }
}
