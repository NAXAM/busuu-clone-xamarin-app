using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using Android.Support.V4.App;
using Java.Lang;
using Naxam.Busuu.Droid.Core.Utils;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Droid.Profile.Views;

namespace Naxam.Busuu.Droid.Profile.Utils
{
    public class ProfileAdapter : FragmentStatePagerAdapter
    {
        public IList<SocialModel> ItemsExercise;
        public IList<SocialModel> ItemsCorrection;
        public ProfileAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
        {

        }

        public override int Count => 2;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            if (position == 0)
            {
                return new ExerciseFragment
                {
                    //Items = ItemsExercise
                };
            }
            return new CorrectionFragment
            {
               // Items = ItemsCorrection
            };
        }
        public override ICharSequence GetPageTitleFormatted(int position)
        {
            ICharSequence title = new Java.Lang.String("");
            switch (position)
            {
                case 0:
                    title = new Java.Lang.String("MY EXERCISES");
                    break;
                case 1:
                    title = new Java.Lang.String("MY CORRECTIONS");
                    break;
            }

            return title;
        }
    }
}