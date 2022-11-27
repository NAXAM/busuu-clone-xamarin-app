using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.Profile.Services;
using Naxam.Busuu.Profile.Models;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class SettingInterfaceLanguageViewModel : MvxViewModel
    {
		IMvxMessenger messenger;
		IDataProfileService profileService;

		public SettingInterfaceLanguageViewModel(IMvxMessenger messenger, IDataProfileService profileService)
		{
			this.messenger = messenger;
			this.profileService = profileService;
            Languages = new MvxObservableCollection<LanguageModel>(profileService.GetAllLanguage().Result);
		}

		protected override void InitFromBundle(IMvxBundle parameters)
		{
			base.InitFromBundle(parameters);
			Key = parameters.Data["Key"];
		}

        #region Property
        private MvxObservableCollection<LanguageModel> _Languages;

        public MvxObservableCollection<LanguageModel> Languages
        {
            get { return _Languages; }
            set
            {
                if (_Languages != value)
                {
                    _Languages = value;
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

        private LanguageModel _Value;

        public LanguageModel Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    RaisePropertyChanged();
					RunDoneCommand();
                }
            }
        }

		private string _Key;

		public string Key
		{
			get { return _Key; }
			set
			{
				if (_Key != value)
				{
					_Key = value;
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
            Close(this);
        }

		void RunDoneCommand()
		{
			User = new UserModel
			{
				interfaceLanguage = Value
			};
			messenger.Publish(new SettingModel("")
			{
                Key = Key,
				Value = User
			});
			Close(this);
		}

		#endregion Command	

    }
}
