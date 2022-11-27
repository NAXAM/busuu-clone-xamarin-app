using System;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Learning.Services;

namespace Naxam.Busuu.Learning.ViewModels
{
    public class VocabularyViewModel : MemoriseBaseViewModel
    {
        ExerciseModel _exercise;
        readonly ILearningService learningService;

        public ExerciseModel Exercise
        {
            get { return _exercise; }
            set
            {
                if (_exercise != value)
                {
                    _exercise = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(() => UnitCount);
                    RaisePropertyChanged(() => VisibleTip);
                    RaisePropertyChanged(() => StepText);
                }
            }
        }

        private UnitModel _CurrentUnit;
        public UnitModel CurrentUnit
        {
            get => _CurrentUnit;
            set => SetProperty(ref _CurrentUnit, value);
        }


        private int _Correct;
        public int Correct
        {
            get => _Correct;
            set => SetProperty(ref _Correct, value);
        }

        private int _CurrentPosition;
        public int CurrentPosition
        {
            get => _CurrentPosition;
            set
            {
                SetProperty(ref _CurrentPosition, value); 
                
                if (value >= UnitCount)
                {
                    ShowViewModel<SummaryViewModel>(new
                    {
                        Correct = Correct,
                        Total = UnitCount,
                        ExerciseId = Exercise.ExerciseId
                    });
                    Close(this);
                }
                else
                {
                    CurrentUnit = Exercise.Units[value];
                }
                RaisePropertyChanged(() => StepText);
            }
        }

        public int UnitCount
        {
            get
            {
                return Exercise?.Units != null ? Exercise.Units.Count : 0;
            }
        }


        public bool VisibleTip
        {
            get
            {
                if (Exercise == null)
                    return false;
                if (Exercise.Units == null)
                    return false;
                return Exercise.Units[CurrentPosition].Tip != null;
            }
        }

        public string StepText
        {
            get
            {
                return (CurrentPosition + 1) + "/" + UnitCount;
            }
        }

        public VocabularyViewModel(ILearningService learningService)
        {
            this.learningService = learningService;
        }

        public async void Init(int id)
        {
			Exercise = new ExerciseModel
			{
				ExerciseId = id
			};
			Exercise.Units = await learningService.GetUnitByExercise(Exercise);
        }

		IMvxCommand _GoBackCommand;
		public IMvxCommand GoBackCommand
		{
			get { return _GoBackCommand = _GoBackCommand ?? new MvxCommand(() => Close(this)); }
		}
    }
}
