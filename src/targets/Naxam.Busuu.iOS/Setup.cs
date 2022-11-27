using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using Naxam.Busuu.iOS.Notification.Views;
using Naxam.Busuu.iOS.Profile.Views;
using Naxam.Busuu.iOS.Review.Views;
using Naxam.Busuu.Review.ViewModels;
using Naxam.Busuu.iOS.Social.Views;
using Naxam.Busuu.Notification.ViewModels;
using Naxam.Busuu.Profile.ViewModels;
using Naxam.Busuu.Social.ViewModels;
using MvvmCross.Binding.Bindings.Target.Construction;
using Naxam.Busuu.iOS.Core.CustomBindings;
using MvvmCross.Platform.Converters;
using MvvmCross.Binding.BindingContext;
using Naxam.Busuu.iOS.Core.Converter;
using UIKit;
using Naxam.Busuu.Core.ViewModels;
using Naxam.Busuu.iOS.Start.Views;
using Naxam.Busuu.iOS.Learning.Views;
using Naxam.Busuu.Learning.ViewModels;
using Naxam.Busuu.Core.Converter;
using CoreAnimation;
using Naxam.Busuu.iOS.Core.Views;
using Naxam.Busuu.iOS.Learning.CustomBindings;
using Naxam.Busuu.iOS.Core.Converter.LanguageSpeakCell;
using Naxam.Busuu.iOS.Core.Converter.ReviewTableViewCell;

namespace Naxam.Busuu.iOS
{
    public class Setup : MvxIosSetup
    {
		public Setup(MvxApplicationDelegate appDelegate, IMvxIosViewPresenter presenter) : base(appDelegate, presenter)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new App();
		}


        protected override IEnumerable<Assembly> GetViewAssemblies()
        {
			var assemblies = new List<Assembly>
			{
				typeof(StartPageView).Assembly,
                typeof(LearnView).Assembly,              
                typeof(ReviewView).Assembly,
				typeof(SocialView).Assembly,
				typeof(NotificationView).Assembly,
				typeof(ProfileView).Assembly,
                typeof(PremiumView).Assembly
			};

			assemblies.AddRange(base.GetViewAssemblies());
			return assemblies; 
        }

        protected override IEnumerable<Assembly> GetViewModelAssemblies()
        {
			var assemblies = new List<Assembly>
			{
                typeof(Busuu.Start.ViewModels.StartPageViewModel).Assembly,
                typeof(LearnViewModel).Assembly,
				typeof(ReviewViewModel).Assembly,
                typeof(SocialViewModel).Assembly,
                typeof(NotificationViewModel).Assembly,
                typeof(ProfileViewModel).Assembly,
                typeof(Busuu.Core.ViewModels.PremiumViewModel).Assembly
			};

			assemblies.AddRange(base.GetViewModelAssemblies());
			return assemblies;
        }

        protected override void FillBindingNames(IMvxBindingNameRegistry obj)
        {
            base.FillBindingNames(obj);
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<UILabel>(
                "FormattedText",
                x => new AttributedTextTargetBinding(x)
            );
            registry.RegisterCustomBindingFactory<CALayer>(
                "BorderColor",
                x=> new ColorTargetBinding(x)
            );
			registry.RegisterCustomBindingFactory<RippleLayer>(
				"RippleColor",
				x => new RippleColorTargetBinding(x)
			);
			registry.RegisterCustomBindingFactory<CALayer>(
				"LayerBackgroundColor",
				x => new LayerColorTargetBinding(x)
			);
			registry.RegisterCustomBindingFactory<UIView>(
				"ExcersizeImageView",
				x => new ImageExersiceTargetBinding(x)
			);
        }

		protected override void FillValueConverters(IMvxValueConverterRegistry registry)
		{
			// avoid the reflection overhead - do not call base class
			//base.FillValueConverters(registry);
			registry.AddOrOverwrite(nameof(ImageUriConverter), new ImageUriConverter());
            registry.AddOrOverwrite(nameof(SocialTypeToBoolFalseConverter), new SocialTypeToBoolFalseConverter());
            registry.AddOrOverwrite(nameof(VisibilityToHideConverter), new VisibilityToHideConverter());
            registry.AddOrOverwrite(nameof(HexToUIColorValueConverter), new HexToUIColorValueConverter());
            registry.AddOrOverwrite(nameof(AddMinutesValueConverter), new AddMinutesValueConverter());
            registry.AddOrOverwrite(nameof(HexToBrighterUIColorValueConverter), new HexToBrighterUIColorValueConverter());
            registry.AddOrOverwrite(nameof(StarDoubleConverter), new StarDoubleConverter());
            registry.AddOrOverwrite(nameof(StarDoubleTextConverter), new StarDoubleTextConverter());
            registry.AddOrOverwrite(nameof(FeedbackCountConverter), new FeedbackCountConverter());
            registry.AddOrOverwrite(nameof(SocialTypeToBoolConverter), new SocialTypeToBoolConverter());
            registry.AddOrOverwrite(nameof(HowDidTextConverter), new HowDidTextConverter());
            registry.AddOrOverwrite(nameof(PostedToStringConverter), new PostedToStringConverter());
            registry.AddOrOverwrite(nameof(NotifyTextToFormattedTextConverter), new NotifyTextToFormattedTextConverter());
            registry.AddOrOverwrite(nameof(NotificationDatetimeConverter), new NotificationDatetimeConverter());
            registry.AddOrOverwrite(nameof(NotifyToColorConverter), new NotifyToColorConverter());
            registry.AddOrOverwrite(nameof(lblLeverTopConverter), new lblLeverTopConverter());
            registry.AddOrOverwrite(nameof(lblLeverHeightConverter), new lblLeverHeightConverter());
            registry.AddOrOverwrite(nameof(SpeakLanguageToTextConverter), new SpeakLanguageToTextConverter());
            registry.AddOrOverwrite(nameof(IsCheckToViewSampleHeithConverter), new IsCheckToViewSampleHeithConverter());
            registry.AddOrOverwrite(nameof(IsCheckToViewSampleTopConverter), new IsCheckToViewSampleTopConverter());
            registry.AddOrOverwrite(nameof(IsFavoriteToImageStarConverter), new IsFavoriteToImageStarConverter());
            registry.AddOrOverwrite(nameof(StrengthLevelToImageConverter), new StrengthLevelToImageConverter());       
		}
    }
}
