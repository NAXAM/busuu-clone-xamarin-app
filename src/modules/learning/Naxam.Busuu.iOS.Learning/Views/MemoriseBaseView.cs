using System;
using Naxam.Busuu.iOS.Learning.Common;
using Naxam.Busuu.Learning.Models;
using UIKit;

namespace Naxam.Busuu.iOS.Learning.Views
{
    public class MemoriseBaseView : UIView
    {
		//public virtual event EventHandler<bool> NextClicked;
        public IAnswerClick Answered;
		public UnitModel Item;

		public MemoriseBaseView(IntPtr handle): base(handle)
        {

		}
    }
}
