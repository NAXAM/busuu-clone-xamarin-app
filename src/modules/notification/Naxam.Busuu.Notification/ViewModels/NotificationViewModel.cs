using System;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Notification.Services;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.ViewModels;
using System.Linq;

namespace Naxam.Busuu.Notification.ViewModels
{
    public class NotificationViewModel : MvxViewModel
    {
        readonly IDataNotification _datanotification;

        private MvxObservableCollection<NotificationModel> _NotificationData;
        public MvxObservableCollection<NotificationModel> NotificationData
        {
            get => _NotificationData;
            set => SetProperty(ref _NotificationData, value);
        }


        private MvxObservableCollection<NotificationModel> _Notifications;
        public MvxObservableCollection<NotificationModel> Notifications
        {
            get => _Notifications;
            set => SetProperty(ref _Notifications, value);
        }

        private int _FriendRequestCount;
        public int FriendRequestCount
        {
            get => _FriendRequestCount;
            set => SetProperty(ref _FriendRequestCount, value);
        }

        public NotificationViewModel(IDataNotification datanotification)
        {
            _datanotification = datanotification;
        }

        public async override void Start()
        {
            NotificationData = new MvxObservableCollection<NotificationModel>(await _datanotification.GetNotification());
            Notifications = new MvxObservableCollection<NotificationModel>(NotificationData);
            var friendRequest = await _datanotification.GetFriendRequest(1);
            FriendRequestCount = friendRequest.Length;
            Notifications.Insert(0, new NotificationModelBase
            {
                Type = NotificationType.Request,
                CountRequest = FriendRequestCount,
                User = new UserModel
                {
                    Photo = "http://www.newsofbahrain.com/admin/post/upload/000PST_31-03-2016_1459426231_bYViJTGH2j.jpg"
                }
            });

            base.Start();
        }

        private IMvxCommand _GoFriendRequestViewCommand;

        public IMvxCommand GoFriendRequestViewCommand
        {
            get { return _GoFriendRequestViewCommand = _GoFriendRequestViewCommand ?? new MvxCommand(RunGoFriendRequestViewCommand); }
        }

        void RunGoFriendRequestViewCommand()
        {
            ShowViewModel<FriendRequestViewModel>();
        }

        IMvxCommand _ViewNotificationCommand;
        public IMvxCommand ViewNotificationCommand
        {
            get
            {
                return (_ViewNotificationCommand = _ViewNotificationCommand ?? new MvxCommand<NotificationModel>(ExecuteViewNotificationCommand));
            }
        }

        void ExecuteViewNotificationCommand(NotificationModel item)
        {
            item.IsRead = true;
            if (item.Type == NotificationType.Request)
            {
                ShowViewModel<FriendRequestViewModel>();
            }
        }
    }
}
