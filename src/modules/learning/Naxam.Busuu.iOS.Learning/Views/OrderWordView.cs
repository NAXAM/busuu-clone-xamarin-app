using Foundation;
using System;
using UIKit;
using Naxam.Busuu.Learning.Models;
using ObjCRuntime;

namespace Naxam.Busuu.iOS.Learning.Views
{
    public partial class OrderWordView : MemoriseBaseView
    {
		public event EventHandler<AnswerModel> AnswerClick;
        //public override event EventHandler<bool> NextClicked;
		public OrderWordView(IntPtr handle) : base(handle)
        {
			//NextClicked?.Invoke(this, true);

		}

		public static OrderWordView Create(UnitModel item)
		{
			var arr = NSBundle.MainBundle.LoadNib("OrderWord", null, null);
			var v = Runtime.GetNSObject<OrderWordView>(arr.ValueAt(0));
			v.Item = item;
			//v.InitData();
			return v;
		}
    }
}