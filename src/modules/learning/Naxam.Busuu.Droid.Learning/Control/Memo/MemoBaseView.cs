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
using Naxam.Busuu.Learning.Models;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Binding.Attributes;

namespace Naxam.Busuu.Droid.Learning.Control.Memo
{
    public class MemoBaseView : LinearLayout
    {
        public virtual event EventHandler<int> NextClick;
        public UnitModel Item
        {
            set; get;
        }

        public bool IsCompleted { get; set; }
        public bool IsCorrect { get; set; }
         

        public MemoBaseView(Context context) : base(context)
        {
        }

        public MemoBaseView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public MemoBaseView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public MemoBaseView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected MemoBaseView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
        

       
    }
}