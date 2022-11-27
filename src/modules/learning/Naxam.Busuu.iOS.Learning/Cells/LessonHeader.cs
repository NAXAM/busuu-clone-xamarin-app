using Foundation;
using System;
using UIKit;
using ObjCRuntime;

namespace Naxam.Busuu.iOS.Learning.Cells
{
    public partial class LessonHeader : UIView
    {
        public LessonHeader (IntPtr handle) : base (handle)
        {
        }
		public static LessonHeader Create()
		{
			var arr = NSBundle.MainBundle.LoadNib("LessonHeader", null, null);
			var v = Runtime.GetNSObject<LessonHeader>(arr.ValueAt(0));
			return v;
		}

		public UILabel Title
		{
			get => lbHeader;
			set => lbHeader = value;
		}
    }
}