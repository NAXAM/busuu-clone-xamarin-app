using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.ViewModels
{
    public class DialogueBaseViewModel : MvxViewModel
    {
        private ExerciseModel _Item;
        public ExerciseModel Item
        {
            get => _Item;
            set => SetProperty(ref _Item, value);
        }


        private bool _IsCompleted;
        public bool IsCompleted
        {
            get => _IsCompleted;
            set => SetProperty(ref _IsCompleted, value);
        }

    }
}
