using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.RecyclerView.ItemTemplates;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Droid.Notification.Utils
{
    public class NotificationTemplateSelector : IMvxTemplateSelector
    {
        const int TypeFriendRequest = 1;
        const int TypeNotification = 2;
        public NotificationTemplateSelector()
        {

        }
        public int GetItemLayoutId(int fromViewType)
        {
            if (fromViewType == TypeFriendRequest)
                return Resource.Layout.has_friend_request;
            return Resource.Layout.normal_item_notification;
        }

        public int GetItemViewType(object forItemObject)
        {
            if (forItemObject.GetType() == typeof(NotificationModelBase))
            {
                return TypeFriendRequest;
            }
            return TypeNotification;
        }
    }
}