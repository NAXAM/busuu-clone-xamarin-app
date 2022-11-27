using Acr.UserDialogs;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Profile.Models;
using Naxam.Busuu.Profile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class ProfileSettingViewModel : MvxViewModel
    {
        IUserDialogs userDialogs;
        IDataProfileService profileService;
        MvxSubscriptionToken token;

        public ProfileSettingViewModel(IDataProfileService profileService, IMvxMessenger messenger, IUserDialogs userDialogs)
        {
            this.userDialogs = userDialogs;
            this.profileService = profileService;
            User = profileService.GetUser(1).Result;
            token = messenger.Subscribe<SettingModel>(OnReceiveMessage);
        }

        private void OnReceiveMessage(SettingModel obj)
        {
            switch (obj.Key)
            {
                case nameof(User.Name):
                    User.Name = obj.Value.Name;
                    break;
                case nameof(User.Email):
                    User.Email = obj.Value.Email;
                    break;
                case nameof(User.AboutMe):
                    User.AboutMe = obj.Value.AboutMe;
                    break;
                case nameof(User.PhoneNumber):
                    User.PhoneNumber = obj.Value.PhoneNumber;
                    break;
                case nameof(User.Gender):
                    User.Gender = obj.Value.Gender;
                    break;
                case nameof(User.Country):
                    User.Country = obj.Value.Country;
                    break;
                case nameof(User.interfaceLanguage):
                    User.interfaceLanguage.Language = obj.Value.interfaceLanguage.Language;
                    break;
                case nameof(User.SpeakLanguages):
                    User.SpeakLanguages = new List<LanguageModel>(obj.Value.SpeakLanguages);
                    break;
                default:
                    break;
            }
        }
        #region Property

        private bool _IsCleanedData;
        public bool IsCleanedData
        {
            get => _IsCleanedData;
            set => SetProperty(ref _IsCleanedData, value);
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
        private IMvxCommand _BackCommand;

        public IMvxCommand BackCommand
        {
            get { return _BackCommand = _BackCommand ?? new MvxCommand(RunBackCommand); }

        }

        void RunBackCommand()
        {
            ShowViewModel<ProfileViewModel>();
        }

        private IMvxCommand _EditNameCommand;

        public IMvxCommand EditNameCommand
        {
            get { return _EditNameCommand = _EditNameCommand ?? new MvxCommand(RunEditNameCommand); }

        }

        void RunEditNameCommand()
        {
            ShowViewModel<SettingInputTextViewModel>(new
            {
                Value = User.Name,
                Key = nameof(User.Name)
            });
        }

        private IMvxCommand _EditAboutMeCommand;

        public IMvxCommand EditAboutMeCommand
        {
            get { return _EditAboutMeCommand = _EditAboutMeCommand ?? new MvxCommand(RunEditAboutMe); }

        }

        void RunEditAboutMe()
        {
            ShowViewModel<SettingInputTextViewModel>(new
            {
                Value = User.AboutMe,
                Key = nameof(User.AboutMe)
            });
        }

        private IMvxCommand _EditEmailCommand;

        public IMvxCommand EditEmailCommand
        {
            get { return _EditEmailCommand = _EditEmailCommand ?? new MvxCommand(RunEditEmail); }

        }

        void RunEditEmail()
        {
            ShowViewModel<SettingInputTextViewModel>(new
            {
                Value = User.Email,
                Key = nameof(User.Email)
            });
        }

        private IMvxCommand _EditGenderCommand;

        public IMvxCommand EditGenderCommand
        {
            get { return _EditGenderCommand = _EditGenderCommand ?? new MvxCommand(RunEditGender); }

        }

        void RunEditGender()
        {
            ShowViewModel<SettingGenderViewModel>(new
            {
                Value = User.Gender,
                Key = nameof(User.Gender)
            });
        }

        private IMvxCommand _EditCountryCommand;

        public IMvxCommand EditCountryCommand
        {
            get { return _EditCountryCommand = _EditCountryCommand ?? new MvxCommand(RunEditCountryCommand); }

        }

        void RunEditCountryCommand()
        {
            ShowViewModel<SettingCountryViewModel>(new
            {
                Value = User.Country.Country,
                Key = nameof(User.Country)
            });
        }

        private IMvxCommand _EditLanguageSpeakCommand;

        public IMvxCommand EditLanguageSpeakCommand
        {
            get { return _EditLanguageSpeakCommand = _EditLanguageSpeakCommand ?? new MvxCommand(RunEditLanguageSpeakCommand); }

        }

        void RunEditLanguageSpeakCommand()
        {
            ShowViewModel<SettingLanguageSpeakViewModel>(new { id = User.Id });
        }

        IMvxCommand _EditInterfaceLanguageCommand;

        public IMvxCommand EditInterfaceLanguageCommand
        {
            get { return _EditInterfaceLanguageCommand = _EditInterfaceLanguageCommand ?? new MvxCommand(RunEditInterfaceLanguageCommand); }

        }

        void RunEditInterfaceLanguageCommand()
        {
            ShowViewModel<SettingInterfaceLanguageViewModel>(new
            {
                Key = nameof(User.interfaceLanguage)
            });
        }

        #endregion Command


        public IMvxCommand GoBackCommand
        {
            get { return new MvxCommand(() => Close(this)); }
        }

        public IMvxCommand NotificationSettingViewCommand
        {
            get { return new MvxCommand(() => ShowViewModel<NotificationSettingViewModel>()); }
        }

        public IMvxCommand ContactUsViewCommand
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<ContactUsViewModel>());
            }
        }

        public IMvxCommand ItWorksViewCommand
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<ItWorksViewModel>());
            }
        }

        private IMvxCommand _ClearDataCommand;

        public IMvxCommand ClearDataCommand
        {
            get { return _ClearDataCommand = _ClearDataCommand ?? new MvxCommand(RunClearDataCommand); }

        }

        void RunClearDataCommand()
        {
            userDialogs.Alert("Clear Success!");
            IsCleanedData = true;
        }


    }
}
