using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Start.Models;

namespace Naxam.Busuu.Start.ViewModels
{
    public class ChooseCountryViewModel : MvxViewModel
    {
        IMvxMessenger messenger;

		public ChooseCountryViewModel(IMvxMessenger messenger)
		{
			this.messenger = messenger;
		}

		void RunDoneCommand()
		{
            messenger.Publish(new ChooseCountryModel("")
			{
                Value = CountrySelected
			});
			Close(this);
		}

		CountryModel _Value;

		public CountryModel Value
		{
			get { return _Value; }
			set
			{
				if (_Value != value)
				{
					_Value = value;
					RaisePropertyChanged();
				}
			}
		}

        MvxObservableCollection<CountryModel> _countries;

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

        CountryModel _countrySelected;

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
                        RunDoneCommand();
                    }
                    RaisePropertyChanged("CountrySelected");
                }
            }
        }

		public override void Start()
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
			base.Start();
		}
    }
}
