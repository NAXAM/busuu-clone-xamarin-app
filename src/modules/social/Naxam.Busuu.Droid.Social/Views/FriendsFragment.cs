using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Naxam.Busuu.Droid.Social.Adapter;
using static Android.Support.V7.Widget.RecyclerView;
using Android.Graphics;
using Naxam.Busuu.Droid.Core.Utils;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Droid.Core;
using Naxam.Busuu.Droid.Core.Listener;
using MvvmCross.Droid.Support.V4;
using Naxam.Busuu.Social.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;

namespace Naxam.Busuu.Droid.Social.Views
{
    public class FriendsFragment : MvxFragment<FriendsViewModel>
    {
        public FriendsFragment()
        {

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.friend_layout, container, false);
            return view;
        }
    }
}