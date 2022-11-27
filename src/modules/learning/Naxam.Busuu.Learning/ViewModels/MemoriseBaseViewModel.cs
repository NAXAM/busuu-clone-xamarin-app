using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.ViewModels
{
    public class MemoriseBaseViewModel : MvxViewModel
    {

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            if (parameters.Data.ContainsKey("Position"))
                Position = int.Parse(parameters.Data["Position"]);
            if (parameters.Data.ContainsKey("Correct"))
                Correct = int.Parse(parameters.Data["Correct"]);
            if (parameters.Data.ContainsKey("ExerciseId"))
                ExerciseId = int.Parse(parameters.Data["ExerciseId"]);
        }

        public void NextUnit(IList<UnitModel> units, bool first = false)
        {
            if (first)
                Position = -1;
            if (Position == units.Count - 1)
            {
                ShowViewModel<SummaryViewModel>(new
                {
                    Correct = Correct,
                    Total = units.Count,
                    ExerciseId = ExerciseId
                });
                return;
            }

            switch (units[Position + 1].Type)
            {
                case UnitModel.UnitType.ChooseWord:
                    ShowViewModel<ChooseWordViewModel>(new
                    {
                        Position = Position + 1,
                        Correct = Correct,
                        ExerciseId = ExerciseId
                    });
                    break;
                case UnitModel.UnitType.CompleteSentence:
                    ShowViewModel<CompleteSentenceViewModel>(new
                    {
                        Position = Position + 1,
                        Correct = Correct,
                        ExerciseId = ExerciseId
                    });
                    break;
                case UnitModel.UnitType.ConversationSentence:
                    ShowViewModel<ConversationSentenceViewModel>(new
                    {
                        Position = Position + 1,
                        Correct = Correct,
                        ExerciseId = ExerciseId
                    });
                    break;
                case UnitModel.UnitType.FillSentence:
                    ShowViewModel<FillSentenceViewModel>(new
                    {
                        Position = Position + 1,
                        Correct = Correct,
                        ExerciseId = ExerciseId
                    });
                    break;
                case UnitModel.UnitType.HearAndRepeat:
                    ShowViewModel<HearConversationViewModel>(new
                    {
                        Position = Position + 1,
                        Correct = Correct,
                        ExerciseId = ExerciseId
                    });
                    break;
                case UnitModel.UnitType.MatchingSentence:
                    ShowViewModel<MatchingSentenceViewModel>(new
                    {
                        Position = Position + 1,
                        Correct = Correct,
                        ExerciseId = ExerciseId
                    });
                    break;
                case UnitModel.UnitType.OrderWord:
                    ShowViewModel<OrderWordViewModel>(new
                    {
                        Position = Position + 1,
                        Correct = Correct,
                        ExerciseId = ExerciseId
                    });
                    break;
                case UnitModel.UnitType.SelectWord:
                    ShowViewModel<SelectWordViewModel>(new
                    {
                        Position = Position + 1,
                        Correct = Correct,
                        ExerciseId = ExerciseId
                    });
                    break;
                case UnitModel.UnitType.SelectWordImage:
                    ShowViewModel<SelectWordImageViewModel>(new
                    {
                        Position = Position + 1,
                        Correct = Correct,
                        ExerciseId = ExerciseId
                    });
                    break;
            }
        }

        private int _ExerciseId;
        public int ExerciseId
        {
            get => _ExerciseId;
            set => SetProperty(ref _ExerciseId, value);
        }

        private int _Correct;
        public int Correct
        {
            get => _Correct;
            set => SetProperty(ref _Correct, value);
        }


        private bool _IsCorrect;
        public bool IsCorrect
        {
            get => _IsCorrect;
            set
            {
                if (value == _IsCorrect)
                    return;
                SetProperty(ref _IsCorrect, value);
                if (value)
                {
                    Correct++;
                }
            }
        }

        private bool _Completed;
        public bool IsCompleted
        {
            get => _Completed;
            set => SetProperty(ref _Completed, value);
        }

        private int _Position;
        public int Position
        {
            get => _Position;
            set => SetProperty(ref _Position, value);
        }

        private UnitModel _Item;
        public UnitModel Item
        {
            get => _Item;
            set => SetProperty(ref _Item, value);
        }

        private string _Image;
        public string Image
        {
            get => _Image;
            set => SetProperty(ref _Image, value);
        }

        private string _Audio;
        public string Audio
        {
            get => _Audio;
            set => SetProperty(ref _Audio, value);
        }

        private bool _VisibleNextButton;
        public bool VisibleNextButton
        {
            get => _VisibleNextButton;
            set => SetProperty(ref _VisibleNextButton, value);
        }

        private IMvxCommand _NextCommand;

        public IMvxCommand NextCommand
        {
            get { return _NextCommand = _NextCommand ?? new MvxCommand(RunNextCommand); }

        }

        public virtual void RunNextCommand()
        {

        }


    }
}
