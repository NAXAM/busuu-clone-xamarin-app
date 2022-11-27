using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Profile.Services;
using Naxam.Busuu.Social.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class ExerciseViewModel : MvxViewModel
    {
        IDataProfileService dataProfileService;
        public ExerciseViewModel(IDataProfileService dataProfileService)
        {
            this.dataProfileService = dataProfileService;
            Exercises = dataProfileService.GetExercises(new UserModel
            {
                Name = "Do Thanh",
                Country = new CountryModel
                {
                    Country = "England"
                },
                Id = 1,
                Photo = "",

            }).Result;
        }

        #region Property
        private IList<SocialModel> _Exercises;

        public IList<SocialModel> Exercises
        {
            get { return _Exercises; }
            set
            {
                if (_Exercises != value)
                {
                    _Exercises = value;
                    RaisePropertyChanged();
                }
            }
        }
        private UserModel _User;

        public UserModel User
        {
            get { return _User; }
            set
            {
                if (_User != value)
                {
                    _User = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion Property

        #region Command
        private IMvxCommand _SelectItemCommand;

        public IMvxCommand SelectItemCommand
        {
            get { return _SelectItemCommand = _SelectItemCommand ?? new MvxCommand<SocialModel>(RunSelectItemCommand); }

        }

        void RunSelectItemCommand(SocialModel item)
        {
            ShowViewModel<SocialDetailViewModel>(item);
        }



        #endregion Command
    }
}
