using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class NXExpandableListView : MvxExpandableListView
    {
        public NXExpandableListView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public NXExpandableListView(Context context, IAttributeSet attrs, MvxExpandableListAdapter adapter) : base(context, attrs, adapter)
        {
        }

        protected NXExpandableListView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public IMvxCommand DownloadCommand { set; get; }

    }
}