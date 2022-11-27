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
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;

namespace Naxam.Busuu.Droid.Adapter
{
    public class MainViewPagerAdapter : FragmentPagerAdapter
    {
        public class FragmentInfo
        {
            public string Title { get; set; }
            public Type FragmentType { get; set; }
            public IMvxViewModel ViewModel { get; set; }
        }

        private readonly Context _context;

        public MainViewPagerAdapter(Context context, Android.Support.V4.App.FragmentManager fragmentManager, IEnumerable<FragmentInfo> fragments) : base(fragmentManager)
        {
            _context = context;
            Fragments = fragments;
        }

        public IEnumerable<FragmentInfo> Fragments { get; private set; }

        public override int Count
        {
            get { return Fragments.Count(); }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            var frag = Fragments.ElementAt(position);
            var fragment = Android.Support.V4.App.Fragment.Instantiate(_context,
                                                FragmentJavaName(frag.FragmentType));
            ((MvxFragment)fragment).DataContext = frag.ViewModel;
            return fragment;
        }

        protected virtual string FragmentJavaName(Type fragmentType)
        {
            var namespaceText = fragmentType.Namespace ?? "";
            if (namespaceText.Length > 0)
                namespaceText = namespaceText.ToLowerInvariant() + ".";
            return namespaceText + fragmentType.Name;
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int p0) { return new Java.Lang.String(Fragments.ElementAt(p0).Title); }
    }
}