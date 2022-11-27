using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class ChooseLanguageViewModel : MvxViewModel
    {
        private MvxObservableCollection<LanguageModel> _languages;

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

        private LanguageModel _selectedLanguage;

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
            string[] flags = new string[] {
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
            string[] langs = new string[] {
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
