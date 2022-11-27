using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Start.ViewModels
{
    public class ChooseLanguageViewModel : MvxViewModel
    {
		public IMvxCommand btnBackCommand
		{
			get { return new MvxCommand(() => Close(this)); }
		}

        // Để lại cái này để em dùng
		public IMvxCommand RegisterCommand
		{
            get { return new MvxCommand(() => ShowViewModel<RegisterViewModel>()); }
		}

        MvxObservableCollection<LanguageModel> _languages;

        public MvxObservableCollection<LanguageModel> Languages
        {
            get { return _languages; }
            set
            {
                if (_languages != value)
                {
                    _languages = value;
                    RaisePropertyChanged(() => Languages);
                }
            }
        }

        LanguageModel _selectedLanguage;

        public LanguageModel SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    if (_selectedLanguage != null)
                    {
                        ShowViewModel<RegisterViewModel>(_selectedLanguage);
                    }
                    RaisePropertyChanged();
                }
            }
        }		

		public ChooseLanguageViewModel()
        {
            string[] flags = {
                "flag_small_arabic",
                 "flag_small_chinese",
                  "flag_small_english",
                   "flag_small_french",
                    "flag_small_german",
                     "flag_small_indonesia",
                      "flag_small_italian",
                       "flag_small_japanese",
                        "flag_small_polish",
                         "flag_small_portuguese",
                          "flag_small_russian",
                           "flag_small_spanish",
                            "flag_small_turkish"
            };
            string[] langs = {
                "Arabic",
                 "Chinese",
                  "English",
                   "French",
                    "German",
                     "Indonesia",
                      "Italian",
                       "Japanese",
                        "Polish",
                         "Portuguese",
                          "Russian",
                           "Spanish",
                            "Turkish"
            };
            Languages = new MvxObservableCollection<LanguageModel>();
            for (int i = 0; i < 13; i++)
            {
                Languages.Add(new LanguageModel
                {
                    Flag = flags[i],
                    Language = langs[i]
                });
            }
        }
    }
}
