using MvvmCross.Core.ViewModels;
using System.Collections.Generic;

namespace Naxam.Busuu.Droid.Learning.Models
{
    public class Conversation
    {
        public List<ConversationSentence> Conversations { get; set; }
        public string Title { get; set; }
    }

    public class ConversationSentence: MvxNotifyPropertyChanged
    {
        public string PersonalName { get; set; }
        public string AvataImage { get; set; }
        public string Sentence { get; set; }
    }
}