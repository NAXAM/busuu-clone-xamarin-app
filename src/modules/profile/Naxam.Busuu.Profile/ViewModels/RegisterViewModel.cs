using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class RegisterViewModel : MvxViewModel
    {

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _policy;

        public string Policy
        {
            get { return _policy; }
            set
            {
                if (_policy != value)
                {
                    _policy = value;
                    RaisePropertyChanged();
                }
            }
        }


        private string _phoneCode;

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



        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IMvxCommand _phoneCodeCommand;

        public IMvxCommand PhoneCodeCommand
        {
            get { return _phoneCodeCommand = _phoneCodeCommand ?? new MvxCommand(RunPhoneCodeCommand); }
        }

        void RunPhoneCodeCommand()
        {
            ShowViewModel<ChooseCountryViewModel>();
        }
         

        public RegisterViewModel()
        {
            //ShowViewModel<ChooseCountryViewModel>(); 
        }

        public void Init(CountryModel country)
        {
            PhoneCode = country.PhoneCode;
            if (string.IsNullOrEmpty(PhoneCode))
                PhoneCode = "+1";
            
        }
    }
}
