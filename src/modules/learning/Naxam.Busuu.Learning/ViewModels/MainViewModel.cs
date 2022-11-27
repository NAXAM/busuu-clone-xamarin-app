using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Learning.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naxam.Busuu.Core.ViewModels;

namespace Naxam.Busuu.Learning.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        readonly ILearningService learningService;

        public MainViewModel(ILearningService learningService)
        {
            this.learningService = learningService;
        }

        public async override void ViewAppeared()
        {
            base.ViewAppeared();
            if (Lessons == null) {
                try {
                    var result = await learningService.GetAllLesson();
                    Lessons = new MvxObservableCollection<LessonModel>(result);

					var lessonsAndSubLessions = new List<object>();
					foreach (var lesson in Lessons)
					{
						lessonsAndSubLessions.Add(lesson);
						foreach (var topic in lesson)
						{
							lessonsAndSubLessions.Add(topic);
						}
					}
                    InvokeOnMainThread( () => {
						LessonAndSubLessions = new MvxObservableCollection<object>(lessonsAndSubLessions);
					});
                }
                catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
				
				//LessonAndSubLessions = new MvxObservableCollection<object>();
				//foreach (var lesson in Lessons)
				//{
				//	LessonAndSubLessions.Add(lesson);
				//	foreach (var topic in lesson)
				//	{
				//		LessonAndSubLessions.Add(topic);
				//	}
				//}
            }
        }

        #region Property
        private MvxObservableCollection<LessonModel> _lessons;

        public MvxObservableCollection<LessonModel> Lessons
        {
            get { return _lessons; }
            set
			{
				SetProperty(ref _lessons, value);
            }
        }

        private MvxObservableCollection<object> _lessonAndSubLessions;

		public MvxObservableCollection<object> LessonAndSubLessions
		{
			get { return _lessonAndSubLessions; }
			set
			{
                SetProperty(ref _lessonAndSubLessions, value);
            }
		}

        #endregion Property

        #region Command

        private IMvxCommand _GoPremiumCommand;

        public IMvxCommand GoPremiumCommand
        {
            get { return _GoPremiumCommand = _GoPremiumCommand ?? new MvxCommand(RunGoPremiumCommand); }

        }

        void RunGoPremiumCommand()
        {
            ShowViewModel<PremiumViewModel>();
        }

        private IMvxCommand _ChangeLanguageCommand;

        public IMvxCommand ChangeLanguageCommand
        {
            get { return _ChangeLanguageCommand = _ChangeLanguageCommand ?? new MvxCommand(RunChangeLanguageCommand); }

        }

        void RunChangeLanguageCommand()
        {
            ShowViewModel<ChangeLanguageViewModel>();
        }


        private IMvxCommand _DownloadCommand;

        public IMvxCommand DownloadCommand
        {
            get { return _DownloadCommand = _DownloadCommand ?? new MvxCommand(RunDownloadCommand); }

        }

        void RunDownloadCommand()
        {

        }

        private IMvxCommand _ExerciseClickCommand;

        public IMvxCommand ExerciseClickCommand
        {
            get { return _ExerciseClickCommand = _ExerciseClickCommand ?? new MvxCommand<ExerciseClickEventArg>(RunExerciseClickCommand); }

        }

        void RunExerciseClickCommand(ExerciseClickEventArg e)
        {
            var ex = Lessons[e.LessonIndex][e.TopicIndex].Exercises[e.ExerciseIndex];
            switch (ex.Type)
            {
                case ExerciseModel.ExerciseType.Memorise:
                    ShowViewModel<MemoriseViewModel>(ex);
                    break;
                case ExerciseModel.ExerciseType.Vocabulary:
                    ShowViewModel<VocabularyViewModel>(ex);
                    break;
                case ExerciseModel.ExerciseType.Dialogue:
                    ShowViewModel<DialogueViewModel>(ex);
                    break;
            }

            // ShowViewModel<MemoriseViewModel>(e.Exercise);
        }

        #endregion Command
    }
}
