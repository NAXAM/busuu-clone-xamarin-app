using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.Models
{
    public class TipModel : MvxNotifyPropertyChanged
    {
        private string _tip;

        public string Tip
        {
            get { return _tip; }
            set
            {
                if (_tip != value)
                {
                    _tip = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<string> _samples;

        public IList<string> Samples
        {
            get { return _samples; }
            set
            {
                if (_samples != value)
                {
                    _samples = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _detail;

        public string Detail
        {
            get { return _detail; }
            set
            {
                if (_detail != value)
                {
                    _detail = value;
                    RaisePropertyChanged();
                }
            }
        }
         
    }
}
