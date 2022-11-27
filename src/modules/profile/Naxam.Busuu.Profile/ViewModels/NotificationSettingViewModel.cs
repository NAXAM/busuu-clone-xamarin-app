using System;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.ViewModels;
using Naxam.Busuu.Profile.Models;
using Plugin.Settings.Abstractions;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class NotificationSettingViewModel : ViewModelBase
    {

        private bool _IsPrivateMode;
        public bool IsPrivateMode
        {
            get => _IsPrivateMode;
            set => SetProperty(ref _IsPrivateMode, value);
        }

        private bool _Notifications;
        public bool TurnOnNotification
        {
            get => _Notifications;
            set => SetProperty(ref _Notifications, value);
        }


        private bool _OnCorrectionReceived;
        public bool TurnOnCorrectionReceived
        {
            get => _OnCorrectionReceived;
            set => SetProperty(ref _OnCorrectionReceived, value);
        }


        private bool _OnCorrectionAdded;
        public bool TurnOnCorrectionAdded
        {
            get => _OnCorrectionAdded;
            set => SetProperty(ref _OnCorrectionAdded, value);
        }

        private bool _OnReplies;
        public bool TurnOnReplies
        {
            get => _OnReplies;
            set => SetProperty(ref _OnReplies, value);
        }


        private bool _OnFriendRequests;
        public bool TurnOnFriendRequests
        {
            get => _OnFriendRequests;
            set => SetProperty(ref _OnFriendRequests, value);
        }


        private bool _OnCorrectionRequest;
        public bool TurnOnCorrectionRequest
        {
            get => _OnCorrectionRequest;
            set => SetProperty(ref _OnCorrectionRequest, value);
        }

        ISettings settings;
        public NotificationSettingViewModel(ISettings settings)
        {
            this.settings = settings;
        }

        public override void Start()
        {
            base.Start();

            IsPrivateMode = settings.GetValueOrDefault(nameof(IsPrivateMode), false);
            TurnOnNotification = settings.GetValueOrDefault(nameof(TurnOnNotification), true);
            TurnOnCorrectionRequest = settings.GetValueOrDefault(nameof(TurnOnCorrectionRequest), true);
            TurnOnCorrectionReceived = settings.GetValueOrDefault(nameof(TurnOnCorrectionReceived), true);
            TurnOnCorrectionAdded = settings.GetValueOrDefault(nameof(TurnOnCorrectionAdded), true);
            TurnOnFriendRequests = settings.GetValueOrDefault(nameof(TurnOnFriendRequests), true);
            TurnOnReplies = settings.GetValueOrDefault(nameof(TurnOnReplies), true);
        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();
            settings.AddOrUpdateValue(nameof(IsPrivateMode), IsPrivateMode);
            settings.AddOrUpdateValue(nameof(TurnOnNotification), TurnOnNotification);
            settings.AddOrUpdateValue(nameof(TurnOnCorrectionRequest), TurnOnCorrectionRequest);
            settings.AddOrUpdateValue(nameof(TurnOnCorrectionReceived), TurnOnCorrectionReceived);
            settings.AddOrUpdateValue(nameof(TurnOnCorrectionAdded), TurnOnCorrectionAdded);
            settings.AddOrUpdateValue(nameof(TurnOnFriendRequests), TurnOnFriendRequests);
            settings.AddOrUpdateValue(nameof(TurnOnReplies), TurnOnReplies);
        }
    }
}
