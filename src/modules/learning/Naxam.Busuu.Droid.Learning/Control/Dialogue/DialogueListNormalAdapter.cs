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
using Java.Lang;
using Naxam.Busuu.Learning.Models;
using Com.Bumptech.Glide;
using System.Text.RegularExpressions;
using Android.Text;
using Naxam.Busuu.Droid.Learning.Util;
using Android.Graphics;
using static Android.Widget.TextView;
using Android.Text.Method;
using Naxam.Busuu.Droid.Learning.TargetBinding;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class DialogueListNormalAdapter : BaseAdapter
    {
        public event EventHandler<int> AnswerClickedHandler;
        public IList<UnitModel> ItemSource;
        public Context context;
        public int FocusIndex;
        IList<IList<AnswerModel>> answer;
        protected DialogueListNormalAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
        public DialogueListNormalAdapter(Context context, IList<UnitModel> ItemSource, int focusIndex)
        {
            this.ItemSource = ItemSource;
            this.context = context;
            FocusIndex = focusIndex;
            answer = ItemSource.Select(d => d.Answers).ToList();
        }

        public override int Count => ItemSource.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.FromContext(context).Inflate(Resource.Layout.conversation_fill_list_sentence_item, null);
            }

            var options = new RequestOptions()
                .Transform(new CircleTransform(parent.Context));
            ImageView imgAvatar = convertView.FindViewById<ImageView>(Resource.Id.imgAvatar);
            TextView txtName = convertView.FindViewById<TextView>(Resource.Id.txtName);
            TextView txtInput = convertView.FindViewById<TextView>(Resource.Id.txtInput);
            if (ItemSource[position].Images?.Count > 0)
                Glide.With(context).Load(ItemSource[position].Images[0]).Apply(options).Into(imgAvatar);
            txtName.Text = ItemSource[position].Title;
            List<string> listString = Regex.Split(ItemSource[position].Inputs[0], "%%").ToList();

            string input = "";
            for (int i = 0; i < listString.Count; i++)
            {
                if (i < listString.Count - 1)
                {
                    input = input + listString[i] + answer[position][i].Text.Trim();
                }
                else
                {
                    input = input + listString[i];
                }
            }

            txtInput.Text = input;
            if (position == FocusIndex)
            {
                convertView.SetBackgroundColor(Color.ParseColor("#CFEAFC"));
            }
            else
            {
                convertView.SetBackgroundColor(Color.Transparent);
            }
            return convertView;
        }
    }
}