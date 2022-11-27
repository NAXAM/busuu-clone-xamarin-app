using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Droid.Learning.Models;

namespace Naxam.Busuu.Learning.ViewModels
{
    public class HearConversationViewModel : MvxViewModel
    {
        MvxObservableCollection<ConversationSentence> _conversation;

        public MvxObservableCollection<ConversationSentence> Conversation
        {
            get { return _conversation; }
            set
            {
                if (_conversation != value)
                {
                    _conversation = value;
                    RaisePropertyChanged();
                }
            }
        }

        MvxObservableCollection<ConversationSentence> conversations;

        public HearConversationViewModel()
        {
            conversations = new MvxObservableCollection<ConversationSentence>();
            for (int i = 0; i < 10; i++)
            {
                conversations.Add(new ConversationSentence()
                {
                    AvataImage = i % 2 == 1 ? "gavin" : "gavin2",
                    PersonalName = i % 2 == 1 ? "Jack" : "Gavin",
                    Sentence = "This is verry long sentence " + i
                });
            }

            Conversation = new MvxObservableCollection<ConversationSentence>(conversations);
        }
    }
}
