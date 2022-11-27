using System.Text.RegularExpressions;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Start.ViewModels
{
    public class ForgotPasswordViewModel : MvxViewModel
    {
		bool _IsEnableLoginSend;

		public bool IsEnableLoginSend
		{
			get { return _IsEnableLoginSend; }
			set
			{
				if (_IsEnableLoginSend != value)
				{
					_IsEnableLoginSend = value;
					RaisePropertyChanged(() => IsEnableLoginSend);
				}
			}
		}

        string _emailPhone = "Email address or phone number";

        public string EmailPhone
        {
            get { return _emailPhone; }
            set
            {
                if (_emailPhone != value)
                {
                    _emailPhone = value;
                    IsEnableLoginSend = CheckPhoneNumber(EmailPhone);
                    RaisePropertyChanged(() => EmailPhone);
                }
            }
        }    

        public IMvxCommand ForgotPasswordCommand
        {
            get { return new MvxCommand(() => ShowViewModel<RegisterViewModel>(EmailPhone)); }
        }

		bool CheckPhoneNumber(string email)
		{
            if (EmailPhone != "Email address or phone number")
			{
				Regex regex = new Regex("^[a-zA-Z0-9-_\\.]+@[a-z0-9]+\\.[a-z]{2,4}$");
				bool checkMail = regex.IsMatch(email);

				Regex regexP = new Regex("^+?[0-9]{9,13}$");
				bool checkPhone = regexP.IsMatch(email);

				return (checkMail || checkPhone);
			}

			return false;
		}
    }
}
