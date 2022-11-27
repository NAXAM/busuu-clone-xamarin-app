using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Core.Models
{
    public class FeedbackModel : MvxNotifyPropertyChanged
    {
        private DateTime _postedDate;

        public DateTime PostedDate
        {
            get { return _postedDate; }
            set
            {
                if (_postedDate != value)
                {
                    _postedDate = value;
                    RaisePropertyChanged();
                }
            }
        }

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

        private int _rating;

        public int Rating
        {
            get { return _rating; }
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _feedback;

        public string Feedback
        {
            get { return _feedback; }
            set
            {
                if (_feedback != value)
                {
                    _feedback = value;
                    RaisePropertyChanged();
                }
            }
        }


        private IList<FeedbackModel> _Replies;

        public IList<FeedbackModel> Replies
        {
            get { return _Replies; }
            set
            {
                if (_Replies != value)
                {
                    _Replies = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<UserModel> _likes;

        public IList<UserModel> Likes
        {
            get { return _likes; }
            set
            {
                if (_likes != value)
                {
                    _likes = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<UserModel> _unlikes;

        public IList<UserModel> Unlikes
        {
            get { return _unlikes; }
            set
            {
                if (_unlikes != value)
                {
                    _unlikes = value;
                    RaisePropertyChanged();
                }
            }
        }

    }
}
