using Acr.UserDialogs;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Profile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class ContactUsViewModel: MvxViewModel
    {
        private ContactUsModel _ContactUs;

        public ContactUsModel ContactUs
        {
            get { return _ContactUs; }
            set
            {
                if (_ContactUs != value)
                {
                    _ContactUs = value;
                    RaisePropertyChanged();
                }
            }
        }
        private IMvxCommand _SendCmd;

        public IMvxCommand SendCmd
        {
            get { return _SendCmd = _SendCmd ?? new MvxCommand(RunSendCmd); }

        }

        void RunSendCmd()
        {
            userDialogs.Alert("Message has sent");
            Close(this);
        }

        private IMvxCommand _GoBackCommand;

        public IMvxCommand GoBackCommand
        {
            get { return _GoBackCommand = _GoBackCommand ?? new MvxCommand(RunGoBackCommand); }

        }

        void RunGoBackCommand()
        {
            Close(this);
        }

        IUserDialogs userDialogs;
        public ContactUsViewModel(IUserDialogs userDialogs)
        {
            this.userDialogs = userDialogs;
        }

        public override void ViewAppearing()
        {
            CreatData();
            base.ViewAppearing();
        }

        private void CreatData()
        {
            ContactUs = new ContactUsModel()
            {
                Description= "Naxam",
                Subject="Naxam",
                Email="admin@naxam.net"
            };
        }
    }
}
