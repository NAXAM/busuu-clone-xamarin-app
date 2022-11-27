using System;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;

namespace Naxam.Busuu.Core.Models
{
    public enum ViewType { Reply, Correct, Accpect, Request, Like };

    public class NotificationModel : MvxNotifyPropertyChanged
    {
        private NotificationType _Type;
        public NotificationType Type
        {
            get => _Type;
            set => SetProperty(ref _Type, value);
        }

        private int _Id;
        public int Id
        {
            get => _Id;
            set => SetProperty(ref _Id, value);
        }

        private UserModel _User;
        public UserModel User
        {
            get => _User;
            set => SetProperty(ref _User, value);
        }

        private DateTime _Time;
        public DateTime Time
        {
            get => _Time;
            set => SetProperty(ref _Time, value);
        }

        private bool _IsRead;
        public bool IsRead
        {
            get => _IsRead;
            set => SetProperty(ref _IsRead, value);
        }
    }
}
