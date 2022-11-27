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
using Naxam.Busuu.Droid.Profile.Models;
using Naxam.Busuu.Profile.Models;

namespace Naxam.Busuu.Droid.Profile.DataTemplateSelectors
{
    public class FriendListTemplateSelector : IMvxTemplateSelector
    {
        public int GetItemLayoutId(int fromViewType)
        {
            if (fromViewType == 0)
            {
                return Resource.Layout.request_friend_item;
            }
            return Resource.Layout.friend_list_item;
        }

        public int GetItemViewType(object forItemObject)
        {
            if (forItemObject.GetType() == typeof(FriendListModel))
            {
                return 0;
            }
            return 1;
        }
    }
}