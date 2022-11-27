using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class ChooseCountryViewModel : MvxViewModel
    {
        private MvxObservableCollection<CountryModel> _countries;

        public MvxObservableCollection<CountryModel> Countries
        {
            get { return _countries; }
            set
            {
                if (_countries != value)
                {
                    _countries = value;
                    RaisePropertyChanged(() => Countries);
                }
            }
        }
        private CountryModel _countrySelected;

        public CountryModel CountrySelected
        {
            get { return _countrySelected; }
            set
            {
                if (_countrySelected != value)
                {
                    _countrySelected = value;
                    if (_countrySelected != null)
                    {
                        ShowViewModel<RegisterViewModel>(CountrySelected);
                    }
                    RaisePropertyChanged("CountrySelected");
                }
            }
        }


        public ChooseCountryViewModel()
        {

            Random random = new Random();
            Countries = new MvxObservableCollection<CountryModel>();
            for (int i = 0; i < 100; i++)
            {
                Countries.Add(new CountryModel
                {
                    Country = (char)random.Next(97, 122) + " Country " + i,
                    PhoneCode = "+" + i
                });
            }

        }
    }
}
