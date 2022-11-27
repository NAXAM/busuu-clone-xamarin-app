using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Core.Models
{
    public class VoucherModel : MvxNotifyPropertyChanged
    {
        private string _voucherCode;

        public string voucherCode
        {
            get { return _voucherCode; }
            set
            {
                if (_voucherCode != value)
                {
                    _voucherCode = value;
                    RaisePropertyChanged();
                }
            }
        }

        private float _voucherValue;

        public float voucherValue
        {
            get { return _voucherValue; }
            set
            {
                if (_voucherValue != value)
                {
                    _voucherValue = value;
                    RaisePropertyChanged();
                }
            }
        }


    }
}
