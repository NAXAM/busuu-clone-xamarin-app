using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using System.Reflection;
using Naxam.Busuu.Droid.Profile.Views;
using Naxam.Busuu.Profile.ViewModels;
using Naxam.Busuu.Core.Converter;
using System.Collections;
using Naxam.Busuu.Droid.Learning.Views;
using Naxam.Busuu.Learning.ViewModels;
using MvvmCross.Binding.Bindings.Target.Construction;
using Naxam.Busuu.Droid.Learning.Control;
using Naxam.Busuu.Droid.Learning.TargetBinding;
using Com.Github.Lzyzsd.Circleprogress;
using Naxam.Busuu.Droid.Learning.Converter;
using Naxam.Busuu.Droid.Review.Views;
using Naxam.Busuu.Droid.Social.Views;
using Naxam.Busuu.Review.ViewModels;
using Naxam.Busuu.Social.ViewModels;
using Com.Iarcuschin.Simpleratingbar;
using Naxam.Busuu.Droid.Core.TargetBinding;
using Naxam.Busuu.Droid.Social.TargetBindings;
using Android.Support.V7.Widget;
using Naxam.Busuu.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Platform;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using System.Linq;
using MvvmCross.Droid.Shared.Presenter;
using MvvmCross.Droid.Views;
using Naxam.Busuu.Droid.Profile.Controls;
using Naxam.Busuu.Droid.Profile.TargetBindings;
using Naxam.Busuu.Notification.ViewModels; 
using Naxam.Busuu.Droid.Notification.Views;
using Naxam.Busuu.Core.ViewModels;
using Naxam.Busuu.Droid.Core.Controls;
using Naxam.Busuu.Droid.Review.TargetBindings;

namespace Naxam.Busuu.Droid
{
    public class Setup : MvxAppCompatSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MvxFragmentsPresenter(AndroidViewAssemblies);
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IEnumerable<Assembly> ValueConverterAssemblies
        {
            get
            {
                var toReturn = base.ValueConverterAssemblies as IList;
                toReturn.Add(typeof(IsMatchPatternBase64Converter).Assembly);
                toReturn.Add(typeof(AlphaColorConverter).Assembly);
                return (IEnumerable<Assembly>)toReturn;

            }
        }
        protected override IEnumerable<Assembly> GetViewModelAssemblies()
        {
            var list = new List<Assembly>();
            list.AddRange(base.GetViewModelAssemblies());
            list.Add(typeof(ForgotPasswordViewModel).Assembly);
            list.Add(typeof(ReviewViewModel).Assembly);
            list.Add(typeof(LearnViewModel).Assembly);
            list.Add(typeof(SocialViewModel).Assembly);
            list.Add(typeof(NotificationViewModel).Assembly);
            list.Add(typeof(ViewModels.MainViewModel).Assembly);
            list.Add(typeof(Busuu.Core.ViewModels.PremiumViewModel).Assembly);
            return list.ToArray();
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies
        {
            get
            {
                var assemblies = new List<Assembly>(base.AndroidViewAssemblies)
                {
                    typeof(NavigationView).Assembly,
                    typeof(NXMvxExpandableListView).Assembly,
                    typeof(FloatingActionButton).Assembly,
                    typeof(Android.Support.V7.Widget.Toolbar).Assembly, 
                    typeof(ViewPager).Assembly,
                    typeof(MvvmCross.Droid.Support.V7.RecyclerView.MvxRecyclerView).Assembly,
                    typeof(NXRecyclerView).Assembly
                };

                assemblies.AddRange(GetViewAssemblies());

                return assemblies.Distinct().ToList();
            }
        }
        
        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
		{
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<CircleProgress>("Progress", view => new PercentTargetBinding(view));
            registry.RegisterCustomBindingFactory<CircleProgress>("FinishColor", view => new FinishColorTargetBinding(view));
            registry.RegisterCustomBindingFactory<LessonHeaderBackground>("BackgroundColor", view => new LessonHeaderTargetBinding(view));
            registry.RegisterCustomBindingFactory<View>("BackgroundColor", view => new BackgroundColorTargetBinding(view));
            registry.RegisterCustomBindingFactory<View>("BackgroundColor160", view => new BackgroundColor160TargetBinding(view));
            registry.RegisterCustomBindingFactory<View>("Background", view => new BackgroundTargetBinding(view));
            registry.RegisterCustomBindingFactory<View>("BorderColor", view => new BorderTargetBinding(view));

            registry.RegisterCustomBindingFactory<ImageView>("TintColor", view => new TintColorTargetBinding(view));
            registry.RegisterCustomBindingFactory<ImageView>("ImageResource", view => new ImageResourceTargetBinding(view));
            registry.RegisterCustomBindingFactory<ImageView>("Source", view => new ImageSourceTargetBinding(view));
            registry.RegisterCustomBindingFactory<ImageView>("SourceNormal", view => new ImageSourceNormalTargetBinding(view));
            registry.RegisterCustomBindingFactory<ExerciesView>("NXXItemsSource", view => new ExerciesItemsSourceTargetBinding(view));
            registry.RegisterCustomBindingFactory<ExerciesView>("Color", view => new ExerciesColorTargetBinding(view));
            registry.RegisterCustomBindingFactory<NXMvxExpandableListView>("DownloadCommand", view => new DownloadCommandTargetBinding(view));
            registry.RegisterCustomBindingFactory<NXMvxExpandableListView>("ExerciseClick", view => new ExerciseClickCommandTargetBinding(view));
            registry.RegisterCustomBindingFactory<MemoriseBodyView>("Item", view => new MemoriseTargetBinding(view));
            registry.RegisterCustomBindingFactory<SimpleRatingBar>("Rating", view => new ValueRatingTargetBinding(view));
            //registry.RegisterCustomBindingFactory<RecyclerView>("NXItemsSource", view => new ItemSourceListViewTargetBinding(view));
            registry.RegisterCustomBindingFactory<RecyclerView>("NXItemsSource", view => new ItemSourceSocialDetailListViewTargetBinding(view));

            registry.RegisterCustomBindingFactory<LanguagesTextView>("Languages", view => new LanguagesTextViewTargetBinding(view));
            registry.RegisterCustomBindingFactory<FriendsImageView>("Friends", view => new FriendsImageViewTargetBinding(view));
            registry.RegisterCustomBindingFactory<FriendRequestButton>("State", view => new FriendRequestButtonStateTargetBinding(view));
            registry.RegisterCustomBindingFactory<ImageView>("GlideUrl", view => new UrlGlideTargetBinding(view));
            registry.RegisterCustomBindingFactory<ImageView>("GlideUrl", view => new UrlGlideTargetBinding(view));
            registry.RegisterCustomBindingFactory<EditText>("Hint", view => new TextHintTargetBinding(view));
            registry.RegisterCustomBindingFactory<SettingNotificationItem>("Checked", view => new SettingNotificationTargetBinding(view));
            registry.RegisterCustomBindingFactory<SettingNotificationItem>("IsEnabled", view => new SettingNotificationEnableTargetBinding(view));

            //project Review
            registry.RegisterCustomBindingFactory<HeaderListView>("HeaderItemsSource", view => new HeaderListViewItemsSourceTargetBinding(view));
            
        }

        protected override void FillBindingNames(MvvmCross.Binding.BindingContext.IMvxBindingNameRegistry registry)
        {
            base.FillBindingNames(registry);
        }

        protected override IEnumerable<Assembly> GetViewAssemblies()
        {
            var list = new List<Assembly>();
            list.AddRange(base.GetViewAssemblies());
            list.Add(typeof(ForgotPasswordView).Assembly);
            list.Add(typeof(ReviewFragment).Assembly);
            list.Add(typeof(LearnView).Assembly);
            list.Add(typeof(NotificationView).Assembly);
            list.Add(typeof(SocialFragment).Assembly);
            return list.ToArray();
        }
    }
}