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
using Android.Support.V7.App;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class MemoriseBodyView : LinearLayout
    { 
        private ExerciseModel _Item;
        public EventHandler<ExerciseModel> ItemChanged;
        public ExerciseModel Item
        {
            get { return _Item; }
            set
            {
                if (_Item != value)
                {
                    _Item = value;
                    ItemChanged?.Invoke(this, _Item);
                }
            }
        }

       
        public MemoriseBodyView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public MemoriseBodyView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public MemoriseBodyView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected MemoriseBodyView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        
    }
}