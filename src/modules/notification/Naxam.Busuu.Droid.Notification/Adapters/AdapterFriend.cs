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
using Android.Support.V7.Widget;
using Android.Views.Animations;
using Com.Bumptech.Glide;
using Android.Animation;
using Naxam.Busuu.Droid.Core.Transform;
using Naxam.Busuu.Droid.Notification.Models;
using Naxam.Busuu.Notification.ViewModels;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Droid.Core.Utils;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Notification.Adapters
{
    public class AdapterFriend : RecyclerView.Adapter
    {
        public event EventHandler<FriendRequestModel> ConfirmRequest;
        public event EventHandler<FriendRequestModel> DeleteRequest;

        private Context context;
        private IList<FriendRequestModel> listFriend;
         
        public AdapterFriend(IList<FriendRequestModel> listFriend, Context context)
        {
            this.listFriend = listFriend;
            this.context = context; 
        }

        public override int ItemCount => listFriend.Count;


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var friend = listFriend[position];
            RecyclerViewHolder vholder = (RecyclerViewHolder)holder;
            vholder.BindData(friend);
            vholder.ConfirmRequestEvent += (s, e) =>
            {
                ConfirmRequest?.Invoke(this, friend);
            };
            vholder.DeleteRequestEvent += (s, e) =>
            {
                DeleteRequest?.Invoke(this, friend);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemview = inflater.Inflate(Resource.Layout.item_friend_request, parent, false);
            return new RecyclerViewHolder(itemview);
        }

        public class RecyclerViewHolder : RecyclerView.ViewHolder
        {
            public event EventHandler DeleteRequestEvent;
            public event EventHandler ConfirmRequestEvent;
            Context context;
            private ScaleAnimation scal = new ScaleAnimation(0, 1f, 0, 1f, Dimension.RelativeToSelf, (float)0.5, Dimension.RelativeToSelf, (float)0.5);
            private ScaleAnimation scalDeleteImage = new ScaleAnimation(0, 1f, 0, 1f, Dimension.RelativeToSelf, (float)0.5, Dimension.RelativeToSelf, (float)0.5);

            private Animation fadeOut = new AlphaAnimation(1, 0);
            public FrameLayout mFrameLayout;
            public TextView txtName;
            public ImageView imgAvatar, imgConfirm, imgDelete, imgBlueConfirm, imgHiddenDelete;



            public RecyclerViewHolder(View itemView) : base(itemView)
            {
                context = itemView.Context;
                scal.Duration = 300;
                scal.FillAfter = true;
                //
                scalDeleteImage.Duration = 300;
                scalDeleteImage.FillAfter = true;
                //
                fadeOut.FillAfter = true;
                fadeOut.Duration = 500;

                mFrameLayout = (FrameLayout)itemView.FindViewById(Resource.Id.myFrameLayout);
                imgBlueConfirm = (ImageView)itemView.FindViewById(Resource.Id.imgBlueConfirm);
                txtName = (TextView)itemView.FindViewById(Resource.Id.txtName);
                imgAvatar = (ImageView)itemView.FindViewById(Resource.Id.imgAvatar);
                imgConfirm = (ImageView)itemView.FindViewById(Resource.Id.imgConfirm);
                imgDelete = (ImageView)itemView.FindViewById(Resource.Id.imgDelete);
                imgHiddenDelete = itemView.FindViewById<ImageView>(Resource.Id.imgHiddenDelete);
                //
                scalDeleteImage.AnimationEnd += (s, e) =>
                {
                    imgConfirm.Visibility = ViewStates.Gone;
                    imgBlueConfirm.Visibility = ViewStates.Gone;


                };
                scal.AnimationEnd += (s, e) =>
                {
                    ObjectAnimator anim = ObjectAnimator.OfFloat(mFrameLayout, "translationX", 0, Util.PxFromDp(context,48));
                    anim.SetDuration(300);
                    anim.Start();
                    imgDelete.StartAnimation(fadeOut);

                };
                fadeOut.AnimationEnd += (s, e) =>
                {
                    imgDelete.Visibility = ViewStates.Invisible;

                };
                var options = new RequestOptions()
                    .Transform(new CircleTransform(itemView.Context));
                Glide.With(itemView.Context).Load(Resource.Drawable.ic_grey_tick).Apply(options).Into(imgConfirm);
                Glide.With(itemView.Context).Load(Resource.Drawable.ic_white_cross).Apply(options).Into(imgHiddenDelete);
                Glide.With(itemView.Context).Load(Resource.Drawable.ic_grey_cross).Apply(options).Into(imgDelete);
                Glide.With(itemView.Context).Load(Resource.Drawable.ic_blue_tick).Apply(options).Into(imgBlueConfirm);

                imgHiddenDelete.Clickable = true;
                imgConfirm.Clickable = true;
                imgConfirm.Click += (s, e) =>
                {
                    imgHiddenDelete.Visibility = ViewStates.Invisible;
                    imgConfirm.Visibility = ViewStates.Invisible;
                    imgBlueConfirm.StartAnimation(scal);
                    ConfirmRequestEvent?.Invoke(this, null);

                };
                imgHiddenDelete.Click += (s, e) =>
                {
                    imgDelete.Visibility = ViewStates.Invisible;
                    imgHiddenDelete.StartAnimation(scalDeleteImage);
                    DeleteRequestEvent?.Invoke(this, null);
                };

            }

            public void BindData(FriendRequestModel friend)
            {
                var options = new RequestOptions()
                    .Transform(new CircleTransform(ItemView.Context));
                Glide.With(context).Load(friend.User.Photo).Apply(options).Into(imgAvatar);
                txtName.Text = friend.User.Name;
            }

        }

    }
}