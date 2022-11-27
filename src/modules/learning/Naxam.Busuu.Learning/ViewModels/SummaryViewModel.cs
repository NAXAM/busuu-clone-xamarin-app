using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.ViewModels
{
    public class SummaryViewModel : MvxViewModel
    {
        ILearningService learningService;
        public SummaryViewModel(ILearningService learningService)
        {
            this.learningService = learningService;
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            if (parameters.Data.ContainsKey("Correct"))
                Correct = int.Parse(parameters.Data["Correct"]);
            if (parameters.Data.ContainsKey("Total"))
                Total = int.Parse(parameters.Data["Total"]);
            if (parameters.Data.ContainsKey("ExerciseId"))
                ExerciseId = int.Parse(parameters.Data["ExerciseId"]);
        }


        private int _ExerciseId;
        public int ExerciseId
        {
            get => _ExerciseId;
            set => SetProperty(ref _ExerciseId, value);
        }

        private bool _IsCompleted;
        public bool IsCompleted
        {
            get => _IsCompleted;
            set => SetProperty(ref _IsCompleted, value);
        }


        public string Status
        {
            get
            {
                if (IsCompleted)
                    return "Rất Tốt";
                return "Ôi Không";
            }
        }


        public string ResultText
        {
            get
            {
                if (IsCompleted)
                    return "Hãy tiếp tục!";
                return "bạn cần ít nhất " + (Total - 1) + " Điểm để vượt qua";
            }
        }


        private int _Correct;
        public int Correct
        {
            get => _Correct;
            set => SetProperty(ref _Correct, value);
        }

        private int _Total;
        public int Total
        {
            get => _Total;
            set => SetProperty(ref _Total, value);
        }

        private IMvxCommand _NextCommand;

        public IMvxCommand NextCommand
        {
            get { return _NextCommand = _NextCommand ?? new MvxCommand(RunNextCommand); }

        }

        async void RunNextCommand()
        {
            var exercise = await learningService.GetExerciseByiId(ExerciseId);
            switch (exercise.Type)
            {
                case Models.ExerciseModel.ExerciseType.Dialogue:

                case Models.ExerciseModel.ExerciseType.Memorise:
                    ShowViewModel<MemoriseViewModel>(new
                    {
                        ExerciseId = ExerciseId
                    });
                    break;
            }

            Close(this);
        }

        private IMvxCommand _TryAgainCommand;

        public IMvxCommand TryAgainCommand
        {
            get { return _TryAgainCommand = _TryAgainCommand ?? new MvxCommand(RunTryAgainCommand); }

        }

        async void RunTryAgainCommand()
        {
            var exercise = await learningService.GetExerciseByiId(ExerciseId);
            switch (exercise.Type)
            {
                case Models.ExerciseModel.ExerciseType.Dialogue:
                    ShowViewModel<DialogueFillListSentenceViewModel>(new
                    {
                        ExerciseId = ExerciseId
                    });
                    break;
                case Models.ExerciseModel.ExerciseType.Memorise:
                    ShowViewModel<MemoriseViewModel>(new
                    {
                        ExerciseId = ExerciseId
                    });
                    break;
                case Models.ExerciseModel.ExerciseType.Vocabulary:
                    ShowViewModel<MemoriseViewModel>(new
                    {
                        ExerciseId = ExerciseId
                    });
                    break;
            }
            Close(this);
        }

    }
}
