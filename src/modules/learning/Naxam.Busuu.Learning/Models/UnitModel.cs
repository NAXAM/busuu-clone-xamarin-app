using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.Models
{
    public class UnitModel : MvxNotifyPropertyChanged
    {
        public enum UnitType
        {
            Unknow,
            SelectWord,
            FillSentence,
            ChooseWord,
            MatchingSentence,
            SelectWordImage,
            CompleteSentence,
            HearAndRepeat,
            OrderWord,
            ConversationSentence,
            TrueFalseQuestion
        }


        private UnitType _Type;
        public UnitType Type
        {
            get => _Type;
            set => SetProperty(ref _Type, value);
        }

        private TipModel _Tip;
        public TipModel Tip
        {
            get => _Tip;
            set => SetProperty(ref _Tip, value);
        }

        private string _Title;
        public string Title
        {
            get => _Title;
            set => SetProperty(ref _Title, value);
        }

        private IList<string> _Images;
        public IList<string> Images
        {
            get => _Images;
            set => SetProperty(ref _Images, value);
        }

        public string Image
        {
            get
            {
                if (Images == null)
                    return "";
                if (Images.Count == 0)
                    return "";
                return Images[0];
            }
        }

        private IList<AudioModel> _Audios;
        public IList<AudioModel> Audios
        {
            get => _Audios;
            set => SetProperty(ref _Audios, value);
        }

        public AudioModel Audio
        {
            get
            {
                if (Audios == null)
                    return null;
                if (Audios.Count == 0)
                    return null;
                return Audios[0];
            }
        }

        private IList<string> _Inputs;
        public IList<string> Inputs
        {
            get => _Inputs;
            set => SetProperty(ref _Inputs, value);
        }

        public string Input
        {
            get
            {
                if (Inputs == null)
                    return "";
                if (Inputs.Count == 0)
                    return "";
                return Inputs[0];
            }
        }

        private IList<AnswerModel> _Answers;
        public IList<AnswerModel> Answers
        {
            get => _Answers;
            set => SetProperty(ref _Answers, value);
        }

        public AnswerModel Answer
        {
            get
            {
                if (Answers == null)
                    return null;
                if (Answers.Count == 0)
                    return null;
                return Answers[0];
            }
        }

        public string NormalInput
        {
            get
            {
                List<string> listString = Regex.Split(Input, "%%").ToList();
                string input = "";
                for (int i = 0; i < listString.Count; i++)
                {
                    if (i < listString.Count - 1)
                    {
                        try
                        {
                            input = input + listString[i] + Answers[i].Text.Trim();
                        }
                        catch
                        {
                            break;
                        }
                    }
                    else
                    {
                        input = input + listString[i];
                    }
                }
                return input;
            }
        }

    }
}
