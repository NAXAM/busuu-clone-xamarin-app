using Android.App;
using Android.Widget;
using Android.OS;
using MvvmCross.Droid.Views;
using Naxam.Busuu.Droid.Core;
using Naxam.Busuu.ViewModels;
using Naxam.Busuu.Social.ViewModels;
using Acr.UserDialogs;
using Naxam.Busuu.Learning.ViewModels;

namespace Naxam.Busuu.Droid
{
    [Activity(
        Label = "Busuu",
        NoHistory = true,
        Name = "naxam.busuu.droid.SplashView",
        Theme = "@style/AppTheme.Splash", MainLauncher = true
    )]
    public class SplashView : MvxSplashScreenActivity
    {
        static SplashView()
        { 
            NxFragmentAttribute.framgentContainerMappings.Add(BusuuFragmentHosts.Vocabulary, Resource.Id.layout);
            NxFragmentAttribute.framgentContainerMappings.Add(BusuuFragmentHosts.Dialogue, Resource.Id.layout);
            NxFragmentAttribute.framgentContainerMappings.Add(BusuuFragmentHosts.Memorise, Resource.Id.layout);
            NxFragmentAttribute.framgentContainerMappings.Add(BusuuFragmentHosts.MainView, Resource.Id.main_content);
            NxFragmentAttribute.typeMappings.Add(BusuuFragmentHosts.MainView, typeof(ViewModels.MainViewModel));
            NxFragmentAttribute.typeMappings.Add(BusuuFragmentHosts.Dialogue, typeof(DialogueViewModel));
            NxFragmentAttribute.typeMappings.Add(BusuuFragmentHosts.Memorise, typeof(MemoriseViewModel)); 
            NxFragmentAttribute.typeMappings.Add(BusuuFragmentHosts.Vocabulary, typeof(VocabularyViewModel));
        }

        public SplashView() : base(Resource.Layout.splash_view)
        {

        }
    }
}

