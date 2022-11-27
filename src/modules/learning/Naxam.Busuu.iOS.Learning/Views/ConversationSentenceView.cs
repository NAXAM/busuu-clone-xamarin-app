using Foundation;
using System;
using UIKit;
using Naxam.Busuu.Learning.Models;
using ObjCRuntime;

namespace Naxam.Busuu.iOS.Learning.Views
{
    public partial class ConversationSentenceView : MemoriseBaseView
    {
		public event EventHandler<AnswerModel> AnswerClick;

		public ConversationSentenceView(IntPtr handle) : base(handle)
        {
		}

		public static ConversationSentenceView Create(UnitModel item)
		{
			var arr = NSBundle.MainBundle.LoadNib("ConversationSentence", null, null);
			var v = Runtime.GetNSObject<ConversationSentenceView>(arr.ValueAt(0));
			v.Item = item;
			//v.InitData();
			return v;
		}
    }
}