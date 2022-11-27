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
    public class DialogueNormalListSentenceViewModel : DialogueBaseViewModel
    {
        ILearningService learningService;
        public DialogueNormalListSentenceViewModel(ILearningService learningService)
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
            get { return _NextCommand = _NextCommand ?? new MvxCommand(RunNextCommand); }

        }

        void RunNextCommand()
        {
            ShowViewModel<DialogueFillListSentenceViewModel>();
        }



    }
}
