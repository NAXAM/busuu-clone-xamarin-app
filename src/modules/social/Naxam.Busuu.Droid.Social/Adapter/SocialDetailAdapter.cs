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
using Com.Iarcuschin.Simpleratingbar;
using Naxam.Busuu.Droid.Social.Controls;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Droid.Core.Transform;
using Com.Bumptech.Glide;
using Naxam.Busuu.Droid.Social.Dialogs;
using Naxam.Busuu.Droid.Core.Controls;
using Naxam.Busuu.Droid.Core.Utils;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Social.Adapter
{
    public class SocialDetailAdapter : RecyclerView.Adapter
    {
        public event EventHandler<object> GiveFeedBack;
        public event EventHandler<int> Reply;
        private const int SocialDetailType = 1;
        private const int FeedBackType = 2;
        private const int ReplyType = 3;

        SocialModel Item;
        IList<FeedbackModel> Items;
        Dictionary<int, int> listHeaderPosition;
        Dictionary<int, int> listChildCount;
        public SocialDetailAdapter(SocialModel Item)
        {
            this.Item = Item;
            Items = Item.Feedbacks;
            listHeaderPosition = new Dictionary<int, int>();
            listChildCount = new Dictionary<int, int>();
        }

        public override int ItemCount
        {
            get
            {
                listHeaderPosition.Clear();
                listChildCount.Clear();
                int count = 0;
                for (int i = 0; i < Items.Count; i++)
                {
                    listHeaderPosition.Add(count, i);
                    count++;
                    var y = Items[i].Replies;
                    listChildCount.Add(i, y == null ? 0 : y.Count);
                    count += y == null ? 0 : y.Count;
                }
                return count + 1;
            }
        }

        public override int GetItemViewType(int position)
        {
            if (position == 0)
                return SocialDetailType;
            if (listHeaderPosition.Keys.Contains(position - 1))
                return FeedBackType;
            return ReplyType;
        }
        public FeedbackModel GetFeedback(int position)
        {
            return Items[listHeaderPosition[position]];
        }
        public int GetFeedbackByReply(int position)
        {
            var lstlast = listHeaderPosition.Keys.Where(d => d < position).OrderByDescending(d => d);
            return lstlast.FirstOrDefault();
        }

        public FeedbackModel GetReply(int position)
        {
            var lstlast = listHeaderPosition.Keys.Where(d => d < position).OrderByDescending(d => d);
            int maxCount = 0;
            for (int i = 0; i < listHeaderPosition[lstlast.FirstOrDefault()]; i++)
            {
                maxCount++;
                maxCount += listChildCount[i];
            }
            return Items[listHeaderPosition[lstlast.FirstOrDefault()]].Replies[position - maxCount - 1];
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            switch (GetItemViewType(position))
            {
                case SocialDetailType:
                    SocialDetailViewHolder socialHolder = (SocialDetailViewHolder)holder;
                    socialHolder.BindData(Item);
                    break;
                case FeedBackType:
                    FeedBackViewHolder feedbackHolder = (FeedBackViewHolder)holder;
                    feedbackHolder.BindData(GetFeedback(position - 1), listHeaderPosition[position - 1]);
                    break;
                case ReplyType:
                    FeedBackViewHolder replyHolder = (FeedBackViewHolder)holder;
                    replyHolder.BindData(GetReply(position - 1), listHeaderPosition[GetFeedbackByReply(position - 1)]);
                    break;
            }
        }

        private void FeedbackHolder_ReplyEvent(object sender, int e)
        {
            Reply?.Invoke(this, e);
        }


        private void SocialHolder_ClickGiveFeedBack(object sender, EventArgs e)
        {
            GiveFeedBack?.Invoke(this, Item);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            switch (viewType)
            {
                case SocialDetailType:
                    SocialDetailViewHolder socialHolder = new SocialDetailViewHolder(LayoutInflater.FromContext(parent.Context).Inflate(Resource.Layout.social_detail_item, parent, false));
                    socialHolder.ClickGiveFeedBack -= SocialHolder_ClickGiveFeedBack;
                    socialHolder.ClickGiveFeedBack += SocialHolder_ClickGiveFeedBack;
                    return socialHolder;
                case FeedBackType:
                    FeedBackViewHolder feedbackHolder = new FeedBackViewHolder(LayoutInflater.FromContext(parent.Context).Inflate(Resource.Layout.feedback_item, parent, false));
                    feedbackHolder.ReplyEvent -= FeedbackHolder_ReplyEvent;
                    feedbackHolder.ReplyEvent += FeedbackHolder_ReplyEvent;
                    return feedbackHolder;
            }
            FeedBackViewHolder replyHolder = new FeedBackViewHolder(LayoutInflater.FromContext(parent.Context).Inflate(Resource.Layout.reply_item, parent, false));
            replyHolder.ReplyEvent -= FeedbackHolder_ReplyEvent;
            replyHolder.ReplyEvent += FeedbackHolder_ReplyEvent;
            return replyHolder;
        }
    }


    public class SocialDetailViewHolder : RecyclerView.ViewHolder
    {
        public event EventHandler ClickGiveFeedBack;
        TextView txtName, txtLocation, txtContent, txtExercise, txtPosted, txtNumberRate, txtFeedBack;
        ImageView imgAvtar, btnReport, imgExercise;
        AddFriendButton btnAddFriend;
        SimpleRatingBar ratingBar;
        PlayerSocial player;
        Context context;
        Android.Widget.PopupMenu popup;
        public SocialDetailViewHolder(View view) : base(view)
        {
            context = view.Context;
            txtName = view.FindViewById<TextView>(Resource.Id.txtName);
            btnAddFriend = view.FindViewById<AddFriendButton>(Resource.Id.btnAddUser);
            txtLocation = view.FindViewById<TextView>(Resource.Id.txtLocation);
            txtContent = view.FindViewById<TextView>(Resource.Id.txtContent);
            txtExercise = view.FindViewById<TextView>(Resource.Id.txtExercise);
            txtPosted = view.FindViewById<TextView>(Resource.Id.txtPosted);
            txtNumberRate = view.FindViewById<TextView>(Resource.Id.txtNumberRate);
            txtFeedBack = view.FindViewById<TextView>(Resource.Id.txtFeedBack);
            imgAvtar = view.FindViewById<ImageView>(Resource.Id.imgAvatar);
            imgExercise = view.FindViewById<ImageView>(Resource.Id.imgExercise);
            btnReport = view.FindViewById<ImageView>(Resource.Id.btnReport);
            ratingBar = view.FindViewById<SimpleRatingBar>(Resource.Id.ratingBar);
            player = view.FindViewById<PlayerSocial>(Resource.Id.btnPlayer);
            popup = new Android.Widget.PopupMenu(context, btnReport);
            popup.MenuInflater.Inflate(Resource.Menu.menu_report, popup.Menu);
            popup.MenuItemClick -= Popup_MenuItemClick;
            popup.MenuItemClick += Popup_MenuItemClick;
            btnReport.Click -= BtnReport_Click;
            btnReport.Click += BtnReport_Click;
            txtFeedBack.Click -= TxtFeedBack_Click;
            txtFeedBack.Click += TxtFeedBack_Click;
        }

        private void Popup_MenuItemClick(object sender, Android.Widget.PopupMenu.MenuItemClickEventArgs e)
        {
            if (e.Item.ItemId == Resource.Id.action_report)
            {
                Dialog reportDialog = new ReportDialog(context);
                reportDialog.Show();
            }
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            popup.Show();
        }

        private void TxtFeedBack_Click(object sender, EventArgs e)
        {
            ClickGiveFeedBack?.Invoke(this, null);
        }

        public void BindData(SocialModel Item)
        {
            txtName.Text = Item.User.Name;
            txtLocation.Text = Item.User.Country.Country;
            btnAddFriend.Active = false;
            txtContent.Text = Item.Content;
            txtPosted.Text = Util.GetTextDate(Item.DatePosted);

            var options = new RequestOptions()
                .Transform(new CircleTransform(context));
            Glide.With(context).Load(Item.User.Photo).Apply(options).Into(imgAvtar);


            txtExercise.Text = Item.TextQuestion;
            Glide.With(context).Load(Item.ImgQuestion).Into(imgExercise);

            var feedback = Item.Feedbacks;
            int countFeedback = feedback?.Count == 0 ? 0 : feedback.Count;
            ratingBar.Rating = countFeedback == 0 ? 0 : (float)feedback.Sum(d => d.Rating) / countFeedback;
            txtNumberRate.Text = "(" + countFeedback + ")";

            txtNumberRate.Visibility = ViewStates.Visible;
            ratingBar.Visibility = ViewStates.Visible;
            imgExercise.Visibility = ViewStates.Visible;
            txtExercise.Visibility = ViewStates.Visible;
            txtFeedBack.Visibility = ViewStates.Visible;
            if (Item.Type == SocialModel.SocialType.Speaking)
            {
                player.Visibility = ViewStates.Visible;
                txtContent.Visibility = ViewStates.Gone;
            }
            else
            {
                player.Visibility = ViewStates.Gone;
                txtContent.Visibility = ViewStates.Visible;
            }
        }
    }

    public class FeedBackViewHolder : RecyclerView.ViewHolder
    {
        public event EventHandler<int> ReplyEvent;
        TextView txtName, txtLocation, txtContent, txtPosted;
        ImageView imgAvtar, btnReply, btnReport;
        AddFriendButton btnAddFriend;
        LikeButton btnLike, btnUnlike;
        Context context;
        int position;
        Android.Widget.PopupMenu popup;
        public FeedBackViewHolder(View view) : base(view)
        {
            context = view.Context;
            btnAddFriend = view.FindViewById<AddFriendButton>(Resource.Id.btnAddUser);
            txtName = view.FindViewById<TextView>(Resource.Id.txtName);
            txtLocation = view.FindViewById<TextView>(Resource.Id.txtLocation);
            txtContent = view.FindViewById<TextView>(Resource.Id.txtContent);
            btnLike = view.FindViewById<LikeButton>(Resource.Id.btnLike);
            btnUnlike = view.FindViewById<LikeButton>(Resource.Id.btnUnlike);
            txtPosted = view.FindViewById<TextView>(Resource.Id.txtPosted);
            btnReport = view.FindViewById<ImageView>(Resource.Id.btnReport);
            btnReply = view.FindViewById<ImageView>(Resource.Id.btnReply);
            imgAvtar = view.FindViewById<ImageView>(Resource.Id.imgAvatar);
            popup = new Android.Widget.PopupMenu(context, btnReport);
            popup.MenuInflater.Inflate(Resource.Menu.menu_report, popup.Menu);
            popup.MenuItemClick -= Popup_MenuItemClick;
            popup.MenuItemClick += Popup_MenuItemClick;
            btnReport.Click -= BtnReport_Click;
            btnReport.Click += BtnReport_Click;
            btnAddFriend.AddFriend -= BtnAddFriend_AddFriend;
            btnLike.StateChange -= BtnLike_StateChange;
            btnUnlike.StateChange -= BtnUnlike_StateChange;
            btnAddFriend.AddFriend += BtnAddFriend_AddFriend;
            btnLike.StateChange += BtnLike_StateChange;
            btnUnlike.StateChange += BtnUnlike_StateChange;

        }

        private void BtnUnlike_StateChange(object sender, bool e)
        {
            if (e)
            {
                btnLike.UnActive();
            }
        }

        private void BtnLike_StateChange(object sender, bool e)
        {
            if (e)
            {
                btnUnlike.UnActive();
            }
        }

        private void BtnAddFriend_AddFriend(object sender, bool e)
        { 
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            popup.Show();
        }

        private void Popup_MenuItemClick(object sender, Android.Widget.PopupMenu.MenuItemClickEventArgs e)
        {
            if (e.Item.ItemId == Resource.Id.action_report)
            {
                Dialog reportDialog = new ReportDialog(context);
                reportDialog.Show();

            }
        }

        public void BindData(FeedbackModel feedback, int position)
        {
            this.position = position;
            txtName.Text = feedback.User.Name;
            txtLocation.Text = feedback.User.Country.Country;
            txtContent.Text = feedback.Feedback;

            var options = new RequestOptions()
                .Transform(new CircleTransform(context));
            Glide.With(context).Load(feedback.User.Photo).Apply(options).Into(imgAvtar);
            txtPosted.Text = Util.GetTextDate(feedback.PostedDate);
            btnLike.Active = false;
            btnUnlike.Active = false;
            btnLike.SetText((feedback.Likes == null ? 0 : feedback.Likes.Count).ToString());
            btnAddFriend.Active = false;
            btnUnlike.SetText((feedback.Unlikes == null ? 0 : feedback.Unlikes.Count).ToString());
            btnReply.Click -= BtnReply_Click;
            btnReply.Click += BtnReply_Click;
        }

        private void BtnReply_Click(object sender, EventArgs e)
        {
            ReplyEvent?.Invoke(this, position);
        }
    }
}