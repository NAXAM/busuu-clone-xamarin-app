using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.ViewModel
{
    public class ChangeLanguageViewModel : MvxViewModel
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
                    RaisePropertyChanged();
                }
            }
        }
        public ChangeLanguageViewModel()
        {
            string[] flags = new string[] {
                "flag_arabic",
                 "flag_chinese",
                  "flag_english",
                   "flag_french",
                    "flag_german",
                      "flag_italian",
                       "flag_japanese",
                        "flag_polish",
                         "flag_portuguese",
                          "flag_russian"
            };
            string[] langs = new string[] {
                "Arabic",
                 "Chinese",
                  "English",
                   "French",
                    "German",
                      "Italian",
                       "Japanese",
                        "Polish",
                         "Portuguese",
                          "Russian"
            };
            Random random = new Random();
            List<LanguageModel> temp = new List<LanguageModel>(); 
            for (int i = 0; i < 12; i++)
            {
                temp.Add(new LanguageModel
                {
                    IsCurrent = langs[i % 10] == "English",
                    Flag = flags[i%10],
                    Language = langs[i % 10],
                    Percent = random.Next(1,100)
                });
            }
            Languages = new MvxObservableCollection<LanguageModel>(temp.OrderByDescending(d => d.IsCurrent));
        }

        private IMvxCommand _BackCmd;

        public IMvxCommand BackCmd
        {
            get { return _BackCmd = _BackCmd ?? new MvxCommand(RunBackCmd); }

        }

        void RunBackCmd()
        {
            Close(this);
        }


    }
}
