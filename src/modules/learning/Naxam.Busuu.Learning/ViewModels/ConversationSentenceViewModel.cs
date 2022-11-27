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
    public class ConversationSentenceViewModel : MemoriseBaseViewModel
    {
        ILearningService learningService;
        public ConversationSentenceViewModel(ILearningService learningService)
        {
            this.learningService = learningService;
        }

        protected async override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            Item = await learningService.GetUnitByType(UnitModel.UnitType.ConversationSentence);
            Image = Item.Images?.Count > 0 ? Item.Images[0] : "";
            Audio = Item.Audios?.Count > 0 ? Item.Audios[0].Link : "";
        }

        private IMvxCommand _NextCommand;

        public IMvxCommand NextCommand
        {
            get { return _NextCommand = _NextCommand ?? new MvxCommand(RunNextCommand); }

        }

        async void RunNextCommand()
        {
            var units = await learningService.GetUnitByExercise(new ExerciseModel());
            NextUnit(units);
            Close(this);
        }
    }
}
