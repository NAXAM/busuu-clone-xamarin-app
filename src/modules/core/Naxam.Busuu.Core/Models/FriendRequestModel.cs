using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Core.Models
{
    public class FriendRequestModel : MvxNotifyPropertyChanged
    {
        private UserModel _User;
        public UserModel User
        {
            get => _User;
            set => SetProperty(ref _User, value);
        }

        private bool _IsFriend;
        public bool IsFriend
        {
            get => _IsFriend;
            set => SetProperty(ref _IsFriend, value);
        }

        private bool _RejectFriend;
        public bool RejectFriend
        {
            get => _RejectFriend;
            set => SetProperty(ref _RejectFriend, value);
        } 
    }
}
