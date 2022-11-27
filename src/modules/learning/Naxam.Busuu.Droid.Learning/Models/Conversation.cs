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
using Android.Graphics.Drawables;
using Android.Content.Res;

namespace Naxam.Busuu.Droid.Learning.Models
{
    public class Conversation
    {
        public List<ConversationSentence> Conversations { get; set; }
        public string Title { get; set; }
        public Conversation()
        {
            // demo data 
            Conversations = new List<ConversationSentence>();
            for (int i = 0; i < 10; i++)
            {
                Conversations.Add(new ConversationSentence()
                {
                    PersonalName = "Personal " + i % 2,
                    AvataImage = i % 2 == 1 ? Resource.Drawable.gavin : Resource.Drawable.gavin2,
                    Sentence = "This is verry long sentence " + i
                });
            }
        }
    }

    public class ConversationSentence
    {
        public string PersonalName { get; set; }
        public int AvataImage { get; set; }
        public string Sentence { get; set; }
    }
}