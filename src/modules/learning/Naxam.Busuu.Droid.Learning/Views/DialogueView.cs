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
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Droid.Learning.Control;
using Naxam.Busuu.Learning.ViewModels;
using Naxam.Busuu.Learning.Models;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Theme = "@style/AppTheme.NoActionBar", ParentActivity = typeof(LearnView))]
    public class DialogueView : MvxCachingFragmentCompatActivity<DialogueViewModel>
    {
        ExerciseModel Item;
        int Corrrect;
        int step;
        int CountInput;
        Android.Support.V7.App.ActionBar actionBar; 
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.dialogue_activity);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "";
            SetSupportActionBar(toolbar);
            actionBar = SupportActionBar;
             
            //Item = Item ?? (ViewModel as DialogueViewModel).Exercise;
            //CountInput = Item.Units.Select(d => d.Answers.Where(an => an.Value).ToList().Count).Sum();
            //InitFragment();
        }

        private void InitFragment()
        {
            switch (step)
            {
                case 0:
                    //AddFragment(new DialogueNormalListSentence(Item.Units));
                    break;
                case 1:
                    //AddFragment(new DialogueFillListSentence(Item.Units));
                    break;
                case 2:
                    //actionBar.Hide();
                    //Summary summary = new Summary(Corrrect, CountInput);
                    //transaction = manager.BeginTransaction();
                    //transaction.Replace(Resource.Id.layout, summary, CountInput + "");
                    //transaction.Commit();
                    break;
            }


        }
    }
}