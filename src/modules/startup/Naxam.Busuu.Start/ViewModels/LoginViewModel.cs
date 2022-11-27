using MvvmCross.Core.ViewModels;
using Naxam.Busuu.ViewModels;
using System.Text.RegularExpressions;
namespace Naxam.Busuu.Start.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        bool _IsEnableLoginBtn;

        public bool IsEnableLoginBtn
        {
            get { return _IsEnableLoginBtn; }
            set
            {
                if (_IsEnableLoginBtn != value)
                {
                    _IsEnableLoginBtn = value;
                    RaisePropertyChanged(()=> IsEnableLoginBtn);
                }
            }
        }

        string _TextPass = "Password (minimum 6 characters)";

        public string TextPass
        {
            get { return _TextPass; }
            set
            {
                if (_TextPass != value)
                {
                    _TextPass = value;
                    IsEnableLoginBtn = CheckPhoneNumber(TextEmail, TextPass);
                    RaisePropertyChanged();
                }
            }
        }

        string _TextEmail = "Email address or phone number";

        public string TextEmail
        {
            get { return _TextEmail; }
            set
            {
                if (_TextEmail != value)
                {
                    _TextEmail = value;
                    IsEnableLoginBtn = CheckPhoneNumber(TextEmail, TextPass);
                    RaisePropertyChanged();
                }
            }
        }
       
        public IMvxCommand ForgotPasswordCommand
        {
            get { return new MvxCommand(() => ShowViewModel<ForgotPasswordViewModel>()); }
        }

        public IMvxCommand LoginCommand
        {
            get { return new MvxCommand(() => ShowViewModel<MainViewModel>()); }
        }
           
        bool CheckPhoneNumber(string email, string pass)
        {
            if ((TextEmail != "Email address or phone number") && (TextPass != "Password (minimum 6 characters)"))
            {
                Regex regex = new Regex("^[a-zA-Z0-9-_\\.]+@[a-z0-9]+\\.[a-z]{2,4}$");
                bool checkMail = regex.IsMatch(email);

                Regex regexP = new Regex("^+?[0-9]{9,13}$");
                bool checkPhone = regexP.IsMatch(email);

				return (checkMail || checkPhone) && pass.Length >= 6;
            }

            return false;
        }
    }
}
