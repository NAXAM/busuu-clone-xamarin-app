using System;
using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Support.V7.Widget;
using Naxam.Busuu.Droid.Core.Listener;
using Naxam.Busuu.Droid.Profile.Adapter;
using Naxam.Busuu.Core.Models;
using MvvmCross.Droid.Support.V4;
using Naxam.Busuu.Profile.ViewModels;

namespace Naxam.Busuu.Droid.Profile.Utils
{
    public class ExerciseFragment : MvxFragment<ExerciseViewModel>
    { 
        public ExerciseFragment()
        {

        }
        public override void OnViewModelSet()
        {
            base.OnViewModelSet();
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RecyclerView recyclerView = new RecyclerView(Context);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            recyclerView.AddOnItemTouchListener(new RecyclerItemTouchListener(Context, (position) =>
            {
                ViewModel.SelectItemCommand?.Execute(ViewModel.Exercises[position]);
            }));
            CorrectionAdapter adapter = new CorrectionAdapter(ViewModel.Exercises, 0);
             
            recyclerView.SetAdapter(adapter);
            return recyclerView;
        }
    }
}