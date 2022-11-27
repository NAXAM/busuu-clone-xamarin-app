using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Learning.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.ViewModels
{
    public class DialogueFillListSentenceViewModel : DialogueBaseViewModel
    {
        ILearningService learningService;
        public DialogueFillListSentenceViewModel(ILearningService learningService)
        {
            this.learningService = learningService;
            Item = new ExerciseModel
            {
                Type = ExerciseModel.ExerciseType.Dialogue
            };
            Item.Units = new MvxObservableCollection<UnitModel>(learningService.GetUnitByExercise(Item).Result);
        }

        private IMvxCommand _NextCommand;

        public IMvxCommand NextCommand
        {
            get { return _NextCommand = _NextCommand ?? new MvxCommand<int>(RunNextCommand); }

        }

        void RunNextCommand(int correct)
        {
            ShowViewModel<SummaryViewModel>(new
            {
                Correct = correct,
                Total = Item.Units.Sum(d=>d.Answers.Count),
                ExerciseId = Item.ExerciseId
            });
        }
    }
}
