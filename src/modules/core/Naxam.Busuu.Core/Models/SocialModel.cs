using System;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic; 

namespace Naxam.Busuu.Core.Models
{
    public class SocialModel : MvxNotifyPropertyChanged
    {
        private UserModel _user;

        public UserModel User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<FeedbackModel> _feedbacks;

        public IList<FeedbackModel> Feedbacks
        {
            get { return _feedbacks; }
            set
            {
                if (_feedbacks != value)
                {
                    _feedbacks = value;
                    RaisePropertyChanged();
                }
            }
        }

        private DateTime _datePosted;
        public DateTime DatePosted
        {
            get { return _datePosted; }
            set
            {
                if (_datePosted != value)
                {
                    _datePosted = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _content;

        public string Content
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    _content = value;
                    RaisePropertyChanged();
                }
            }
        }


        public enum SocialType : int
        {
            Writing, Speaking
        }

        private SocialType _type;

        public SocialType Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    RaisePropertyChanged();
                }
            }
        }


        int _Id;

        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    RaisePropertyChanged();
                }
            }
        }

        string _ImageSpeakLanguage;

        public string ImageSpeakLanguage
        {
            get { return _ImageSpeakLanguage; }
            set
            {
                if (_ImageSpeakLanguage != value)
                {
                    _ImageSpeakLanguage = value;
                    RaisePropertyChanged();
                }
            }
        }

        string _ImageLearn;

        public string ImageLearn
        {
            get { return _ImageLearn; }
            set
            {
                if (_ImageLearn != value)
                {
                    _ImageLearn = value;
                    RaisePropertyChanged();
                }
            }
        }

        string _TextLearn;

        public string TextLearn
        {
            get { return _TextLearn; }
            set
            {
                if (_TextLearn != value)
                {
                    _TextLearn = value;
                    RaisePropertyChanged();
                }
            }
        }
              
        double _Star;

        public double Star
        {
            get { return _Star; }
            set
            {
                if (_Star != value)
                {
                    _Star = value;
                    RaisePropertyChanged();
                }
            }
        }

        bool _Friends;

        public bool Friends
        {
            get { return _Friends; }
            set
            {
                if (_Friends != value)
                {
                    _Friends = value;
                    RaisePropertyChanged();
                }
            }
        }

        string _ImgQuestion;

        public string ImgQuestion
        {
            get { return _ImgQuestion; }
            set
            {
                if (_ImgQuestion != value)
                {
                    _ImgQuestion = value;
                    RaisePropertyChanged();
                }
            }
        }

        string _TextQuestion;

        public string TextQuestion
        {
            get { return _TextQuestion; }
            set
            {
                if (_TextQuestion != value)
                {
                    _TextQuestion = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
