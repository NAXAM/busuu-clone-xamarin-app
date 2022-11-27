using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.Models
{
    public class ContactUsModel: MvxNotifyPropertyChanged
    {
        private string _Email;

        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _Subject;

        public string Subject
        {
            get { return _Subject; }
            set
            {
                if (_Subject != value)
                {
                    _Subject = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    RaisePropertyChanged();
                }
            }
        }





    }
}
