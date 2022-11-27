using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Java.Lang;
using Android.Text;

namespace Naxam.Busuu.Droid.Core.Adapter
{
    public class ViewPagerFragmentAdapter : FragmentPagerAdapter
    {
        IList<Android.Support.V4.App.Fragment> Items;
        IList<string> Titles;
        public ViewPagerFragmentAdapter(Android.Support.V4.App.FragmentManager fm, IList<Android.Support.V4.App.Fragment> Items, IList<string> Titles) : base(fm)
        {
            this.Items = Items;
            this.Titles = Titles;
        }

        public override int Count => Items.Count;

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new SpannableStringBuilder(Titles[position]);
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return Items[position];
        }
    }
}