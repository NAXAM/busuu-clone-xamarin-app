using System.Text.RegularExpressions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Start.Models;
using Naxam.Busuu.ViewModels;

namespace Naxam.Busuu.Start.ViewModels
{
    public class RegisterViewModel : MvxViewModel
    {
        bool _IsEnableSignBtn;

		public bool IsEnableSignBtn
		{
			get { return _IsEnableSignBtn; }
			set
			{
				if (_IsEnableSignBtn != value)
				{
					_IsEnableSignBtn = value;
					RaisePropertyChanged(() => IsEnableSignBtn);
				}
			}
		}

		string _emailphone = "Email";

        public string EmailPhone
        {
            get { return _emailphone; }
            set
            {
                if (_emailphone != value)
                {
                    _emailphone = value;
                    IsEnableSignBtn = CheckPhoneNumber(EmailPhone, UserName, Password);
                    RaisePropertyChanged();
                }
            }
        }

        string _phoneCode = "+1";

        public string PhoneCode
        {
            get { return _phoneCode; }
            set
            {
                if (_phoneCode != value)
                {
                    _phoneCode = value;
                    RaisePropertyChanged();
                }
            }
        }

        string _userName = "Username";

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
					IsEnableSignBtn = CheckPhoneNumber(EmailPhone, UserName, Password);
					RaisePropertyChanged();
                }
            }
        }

        string _password = "Password (minimum 6 characters)";

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
					IsEnableSignBtn = CheckPhoneNumber(EmailPhone, UserName, Password);
					RaisePropertyChanged();
                }
            }
        }

		public IMvxCommand LoginCommend
		{
			get { return new MvxCommand(() => ShowViewModel<MainViewModel>()); }
		}
              
        public IMvxCommand PhoneCodeCommand
        {
            get { return new MvxCommand(() => ShowViewModel<ChooseCountryViewModel>()); }
        }

        public void Init(CountryModel country)
        {
            PhoneCode = country.PhoneCode;
            if (string.IsNullOrEmpty(PhoneCode))
                PhoneCode = "+1";      
        }

		bool CheckPhoneNumber(string email, string user, string pass)
		{
            if ((EmailPhone != "Email") && (EmailPhone != "Phone number") && (UserName != "Username") && (Password != "Password (minimum 6 characters)"))
			{
				Regex regex = new Regex("^[a-zA-Z0-9-_\\.]+@[a-z0-9]+\\.[a-z]{2,4}$");
				bool checkMail = regex.IsMatch(email);

				Regex regexP = new Regex("^+?[0-9]{9,13}$");
				bool checkPhone = regexP.IsMatch(email);

                return (checkMail || checkPhone) && user.Length > 0 && pass.Length >= 6;
			}

			return false;
		}

        CountryModel _User;

		public CountryModel User
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


		MvxSubscriptionToken token;

		public RegisterViewModel(IMvxMessenger messenger)
		{
            token = messenger.Subscribe<ChooseCountryModel>(OnReceiveMessage);
		}

		void OnReceiveMessage(ChooseCountryModel obj)
		{
            PhoneCode = obj.Value.PhoneCode;
		}
	}
}
