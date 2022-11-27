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
using Naxam.Busuu.Droid.Learning.Adapters;
using Naxam.Busuu.Droid.Learning.Transformers;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Droid.Learning.Control.Memo;

namespace Naxam.Busuu.Droid.Learning.Control.Vocabulary
{
    public class VocabularyViewPagerFragment : BaseFragment
    {
        Android.Support.V4.App.FragmentManager manager;
        IList<Android.Support.V4.App.Fragment> listFragment;
        public VocabularyViewPagerFragment(Android.Support.V4.App.FragmentManager manager, UnitModel Item)
        {
            this.manager = manager;
            this.Item = Item;
            listFragment = new List<Android.Support.V4.App.Fragment>();
        }
       
        VocabularyViewPager view;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = (VocabularyViewPager)inflater.Inflate(Resource.Layout.vocabulary_viewpager_layout, container, false);

            view.SetAllowedSwipeDirection(VocabularyViewPager.SwipeDirection.Right);

           // VocabularyPagerAdapter adapter = new VocabularyPagerAdapter(manager, listFragment);
           // view.Adapter = adapter;
           // view.SetPageTransformer(true, new ForegroundToBackgroundTransformer());
            return view;
        }
    }
}