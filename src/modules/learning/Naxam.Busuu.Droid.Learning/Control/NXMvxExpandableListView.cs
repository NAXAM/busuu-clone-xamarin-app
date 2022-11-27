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
    public class NXMvxExpandableListView : MvxExpandableListView
    {
        public NXMvxExpandableListView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            DividerHeight = 0;
            ChoiceMode = ChoiceMode.None;
        }

        public NXMvxExpandableListView(Context context, IAttributeSet attrs, MvxExpandableListAdapter adapter) : base(context, attrs, adapter)
        {
            DividerHeight = 0;
            ChoiceMode = ChoiceMode.None;
        }

        protected NXMvxExpandableListView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            DividerHeight = 0;
            ChoiceMode = ChoiceMode.None;
        }


        public IMvxCommand DownloadCommand { set; get; }
        public IMvxCommand ExerciseClickCommand { set; get; }
    }
}