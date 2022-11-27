using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.Models
{
    public class TrueFalseHearQuestionModel : MvxNotifyPropertyChanged
    {
        private string _mp3Link;

        public string mp3Link
        {
            get { return _mp3Link; }
            set
            {
                if (_mp3Link != value)
                {
                    _mp3Link = value;
                    RaisePropertyChanged();
                }
            }
        }


        private string _sentence;

        public string sentence
        {
            get { return _sentence; }
            set
            {
                if (_sentence != value)
                {
                    _sentence = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _trueAnswer;

        public bool trueAnswer
        {
            get { return _trueAnswer; }
            set
            {
                if (_trueAnswer != value)
                {
                    _trueAnswer = value;
                    RaisePropertyChanged();
                }
            }
        }


    }
}
