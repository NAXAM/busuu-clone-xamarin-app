using System;
using System.Linq;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Notification.Services;

namespace Naxam.Busuu.Notification.ViewModels
{
    public class FriendRequestViewModel : MvxViewModel
    {
        readonly IDataNotification _dataFriendRequest;

        public FriendRequestViewModel(IDataNotification dataFriendRequest)
        {
            _dataFriendRequest = dataFriendRequest;
        }

        public async override void Start()
        {
            FriendsRequest = new MvxObservableCollection<FriendRequestModel>((await _dataFriendRequest.GetFriendRequest(1)).Select(d => new FriendRequestModel
            {
                User = d
            }));
            base.Start();
        }

        private MvxObservableCollection<FriendRequestModel> _FriendsRequest;
        public MvxObservableCollection<FriendRequestModel> FriendsRequest
        {
            get => _FriendsRequest;
            set => SetProperty(ref _FriendsRequest, value);
        }

        IMvxCommand _ViewFriendsYesCommand;
        public IMvxCommand ViewFriendsYesCommand
        {
            get
            {
                return (_ViewFriendsYesCommand = _ViewFriendsYesCommand ?? new MvxCommand<FriendRequestModel>(ExecuteViewFriendsYesCommand));
            }
        }

        void ExecuteViewFriendsYesCommand(FriendRequestModel item)
        {
            item.RejectFriend = true;
            //item.Friends = true;
        }

        public IMvxCommand GoBackCommand
        {
            get { return new MvxCommand(() => Close(this)); }
        }

        IMvxCommand _ViewFriendsNoCommand;
        public IMvxCommand ViewFriendsNoCommand
        {
            get
            {
                return (_ViewFriendsNoCommand = _ViewFriendsNoCommand ?? new MvxCommand<FriendRequestModel>(ExecuteViewFriendsNoCommand));
            }
        }

        void ExecuteViewFriendsNoCommand(FriendRequestModel item)
        {
            // item.IsRead = true;
            item.IsFriend = true;
        }

    }
}
