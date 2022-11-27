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
using Naxam.Busuu.Droid.Learning.Models;

namespace Naxam.Busuu.Droid.Learning.Adapters
{
    public class HearConversationQuestionAdapter : BaseAdapter<Conversation>
    {
        Activity context;
        List<ConversationSentence> objects;
        int layoutId;

        public HearConversationQuestionAdapter(Activity context, int resource, List<ConversationSentence> objects)
        {
            this.context = context;
            this.objects = objects;
            this.layoutId = resource;
        }

        public override Conversation this[int position] => throw new NotImplementedException();

        public override int Count
        {
            get { return this.objects.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = context.LayoutInflater;

            if (convertView == null)
            {
                convertView = inflater.Inflate(layoutId, null);
            }


            RelativeLayout loQuestion = convertView.FindViewById<RelativeLayout>(Resource.Id.conversation_lo_question);
            for(int i=0; i < 5; i++)
            {
                EditText edittext = new EditText(context);
                RelativeLayout.LayoutParams param = new RelativeLayout.LayoutParams(48, 80);
                param.AlignWithParent = true;
            }

            return convertView;
        }
    }
}