using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Core.Models
{
    public class CountryModel : MvxNotifyPropertyChanged
    {
        private string _country;

        public string Country
        {
            get { return _country; }
            set
            {
                if (_country != value)
                {
                    _country = value;
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

    }
}
