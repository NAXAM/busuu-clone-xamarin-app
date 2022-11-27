using System;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Models;

namespace Naxam.Busuu.Learning.ViewModels
{
    public class TrueFalseHearQuestionViewModel : MvxViewModel
    {
        // handle when click true button
        IMvxCommand _RightCommand;

        public IMvxCommand RightCommand
        {
            get { return _RightCommand = _RightCommand ?? new MvxCommand(HandleRightCommand); }

        }

        void HandleRightCommand()
        {

        }

        // handle when click false button
        IMvxCommand _WrongCommand;

        public IMvxCommand WrongCommand
        {
            get { return _WrongCommand = _WrongCommand ?? new MvxCommand(HandleWrongCommand); }

        }

        void HandleWrongCommand()
        {

        }

        // handle when click continue button
        IMvxCommand _ContinueCommand;

        public IMvxCommand ContinueCommand
        {
            get { return _ContinueCommand = _ContinueCommand ?? new MvxCommand(HandleContinueCommand); }

        }

        void HandleContinueCommand()
        {

        }

        bool _trueFalseBtEnable;

        public bool trueFalseBtEnable
        {
            get { return _trueFalseBtEnable; }
            set
            {
                if (_trueFalseBtEnable != value)
                {
                    _trueFalseBtEnable = value;
                    RaisePropertyChanged();
                }
            }
        }

        TrueFalseHearQuestionModel _question;

        public TrueFalseHearQuestionModel question
        {
            get { return _question; }
            set
            {
                if (_question != value)
                {
                    _question = value;
                    RaisePropertyChanged();
                }
            }
        }


        public TrueFalseHearQuestionViewModel()
        {
            trueFalseBtEnable = true;
            TrueFalseHearQuestionModel model = new TrueFalseHearQuestionModel()
            {
                mp3Link = "",
                sentence = "What is your name",
                trueAnswer = true
            };

            question = model;
        }
    }
}
