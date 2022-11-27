using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Core.Models
{
    public class ReplyModel : MvxNotifyPropertyChanged
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


        private string _reply;

        public string Reply
        {
            get { return _reply; }
            set
            {
                if (_reply != value)
                {
                    _reply = value;
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
