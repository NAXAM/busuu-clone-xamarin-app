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
    public class SelectWordImageViewModel : MemoriseBaseViewModel
    {
        ILearningService learningService;
        public SelectWordImageViewModel(ILearningService learningService)
        {
            this.learningService = learningService;
        }
        protected async override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            Item = await learningService.GetUnitByType(UnitModel.UnitType.SelectWordImage);
            Image = Item.Images?.Count > 0 ? Item.Images[0] : "";
            Audio = Item.Audios?.Count > 0 ? Item.Audios[0].Link : "";
        }


        private bool _VisibleResult;
        public bool VisibleResult
        {
            get => _VisibleResult;
            set => SetProperty(ref _VisibleResult, value);
        }

        public override async void RunNextCommand()
        {
            var units = await learningService.GetUnitByExercise(new ExerciseModel());
            NextUnit(units);
            Close(this);
        }
    }
}
