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
    public class DialogueListAdapter : BaseAdapter
    {
        public event EventHandler<int> AnswerClickedHandler;
        public IList<UnitModel> ItemSource;
        public IList<AnswerModel> ListTextResult;
        public Context context;
        public int FocusIndex;
        IList<IList<AnswerModel>> answer;
        protected DialogueListAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
        public DialogueListAdapter(Context context, IList<UnitModel> ItemSource, IList<AnswerModel> ListTextResult)
        {

            this.ItemSource = ItemSource;

            this.context = context;
            this.ListTextResult = ListTextResult;
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
            Glide.With(context).Load(ItemSource[position].Images[0]).Apply(options).Into(imgAvatar);
            txtName.Text = ItemSource[position].Title;
            List<string> listString = Regex.Split(" "+ItemSource[position].Inputs[0].Trim()+" ", "%%").ToList();
            List<int> listIndex = new List<int>();
            string input = "";
            for (int i = 0; i < listString.Count; i++)
            {
                if (i < listString.Count - 1)
                {
                    input = input + listString[i] + ListTextResult[GetIndexByRowPosition(position, i)].Text;
                    listIndex.Add(input.Length - ListTextResult[GetIndexByRowPosition(position, i)].Text.Length);
                }
                else
                {
                    input = input + listString[i];
                }
            }
            SpannableStringBuilder ssb = new SpannableStringBuilder(input);
            for (int i = 0; i < listIndex.Count; i++)
            {
                int currentIndex = GetIndexByRowPosition(position, i);
                Integer temp = new Integer(i);
                ssb.SetSpan(new DrawBackgroundSpan
                {
                    HasShadow = !ListTextResult[currentIndex].Text.Contains("#####"),
                    strokeWidth = ListTextResult[currentIndex].Text.Contains("#####") ? FocusIndex == currentIndex ? (int)Util.Util.PxFromDp(context, 1) : 0 : 0,
                    strokeColor = ListTextResult[currentIndex].Text.Contains("#####") ? FocusIndex == currentIndex ? Color.ParseColor("#42ACF6") : Color.Transparent : Color.Transparent,
                    backgroundColor = ListTextResult[currentIndex].Text.Contains("#####") ? FocusIndex == currentIndex ? Color.ParseColor("#CFEAFC") : Color.ParseColor("#D6DEE6") : FocusIndex == -1? answer[position][i]== ListTextResult[currentIndex] ? Color.ParseColor("#74B826") : Color.ParseColor("#EB4331") : Color.White,
                    textColor = ListTextResult[currentIndex].Text.Contains("#####") ? Color.Transparent : FocusIndex==-1 ? Color.White: Color.Black,
                }, listIndex[i], listIndex[i] + ListTextResult[currentIndex].Text.Length, SpanTypes.InclusiveInclusive);
                ClickableSpanNoUnderline clickable = new ClickableSpanNoUnderline();
                clickable.Clicked += (s, e) =>
                {
                    AnswerClickedHandler?.Invoke(this, GetIndexByRowPosition(position, temp.IntValue()));
                };
                ssb.SetSpan(clickable, listIndex[i], listIndex[i] + ListTextResult[currentIndex].Text.Length, SpanTypes.InclusiveInclusive);
            }
            txtInput.SetText(ssb, BufferType.Normal);
            txtInput.MovementMethod = LinkMovementMethod.Instance;
            //txtInput.SetLineSpacing(2f, 2f);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
            {
                txtInput.SetLayerType(LayerType.Software, null);
            }
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


        

        private int GetIndexByRowPosition(int row, int position)
        {
            if (row >= ItemSource.Count)
                return -1;
            int index = 0;
            for (int i = 0; i < row; i++)
            {
                var temp = " " + ItemSource[i].Inputs[0].Trim() + " ";
                int count = Regex.Split(temp, "%%").Length - 1;
                if (count > 0)
                {
                    index += count;
                }

            }
            return index + position;
        }
    }
}