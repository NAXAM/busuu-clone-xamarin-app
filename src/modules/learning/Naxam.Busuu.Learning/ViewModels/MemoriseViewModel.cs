
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Learning.Services;

namespace Naxam.Busuu.Learning.ViewModels
{
    public class MemoriseViewModel : MemoriseBaseViewModel
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

        protected async override void InitFromBundle(IMvxBundle parameters)
        {
            int id = 0;
            base.InitFromBundle(parameters);
            if (parameters.Data.ContainsKey("ExerciseId"))
            {
                id = int.Parse(parameters.Data["ExerciseId"]);
            }
            Exercise = new ExerciseModel
            {
                ExerciseId = id
            };
            Exercise.Units = await learningService.GetUnitByExercise(Exercise);
            StepText = Position + "/" + Exercise.Units.Count;
        }

        public MemoriseViewModel(ILearningService learningService)
        {
            this.learningService = learningService;
        }


        public override void Start()
        {
            //NextUnit(Exercise.Units);
        }

        private string _StepText;
        public string StepText
        {
            get => _StepText;
            set => SetProperty(ref _StepText, value);
        }

        IMvxCommand _GoBackCommand;
        public IMvxCommand GoBackCommand
        {
            get { return _GoBackCommand = _GoBackCommand ?? new MvxCommand(() => Close(this)); }
        }

        private IMvxCommand _FinishCommand;

        public IMvxCommand FinishCommand
        {
            get { return _FinishCommand = _FinishCommand ?? new MvxCommand<int>(RunFinishCommand); }

        }

        void RunFinishCommand(int correct)
        {
            ShowViewModel<SummaryViewModel>(new
            {
                Correct = correct,
                Total = Exercise.Units.Count,
                ExerciseId = Exercise.ExerciseId
            });
            Close(this);
        }


    }
}
