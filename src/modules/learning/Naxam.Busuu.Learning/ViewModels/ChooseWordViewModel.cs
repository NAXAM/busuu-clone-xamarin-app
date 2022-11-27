using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Learning.Services;

namespace Naxam.Busuu.Learning.ViewModels
{
    public class ChooseWordViewModel : MemoriseBaseViewModel
    {
        ILearningService learningService;
        public ChooseWordViewModel(ILearningService learningService)
        {
            this.learningService = learningService;
        }

        protected async override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            Item = await learningService.GetUnitByType(UnitModel.UnitType.ChooseWord);
            Image = Item.Images?.Count > 0 ? Item.Images[0] : "";
            Audio = Item.Audios?.Count > 0 ? Item.Audios[0].Link : "";
        }

        public override async void RunNextCommand()
        {
            var units = await learningService.GetUnitByExercise(new ExerciseModel());
            NextUnit(units);
            Close(this);
        }

    }
}
