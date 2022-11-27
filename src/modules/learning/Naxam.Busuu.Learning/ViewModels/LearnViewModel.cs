using System;
using MvvmCross.Core.ViewModels; 
using Naxam.Busuu.Core.ViewModels;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Learning.Services;

namespace Naxam.Busuu.Learning.ViewModels
{
    public class LearnViewModel : MvxViewModel
    {
        readonly ILearningService learningService;

        public LearnViewModel(ILearningService learningService)
        {
            this.learningService = learningService;
        }

        public async override void Start()
        {
            base.ViewAppeared();
            Lessons = new MvxObservableCollection<LessonModel>(await learningService.GetAllLesson());

            LessonAndSubLessions = new MvxObservableCollection<object>();
            foreach (var lesson in Lessons)
            {
                LessonAndSubLessions.Add(lesson);
                foreach (var topic in lesson)
                {
                    LessonAndSubLessions.Add(topic);
                }
            }

            base.Start();
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
            switch (e.Exercise.Type)
			{
				case ExerciseModel.ExerciseType.Memorise:
                    ShowViewModel<MemoriseViewModel>(new { id = e.Exercise.ExerciseId });
					break;
				case ExerciseModel.ExerciseType.Vocabulary:
					ShowViewModel<VocabularyViewModel>(new { id = e.Exercise.ExerciseId });
					break;
				case ExerciseModel.ExerciseType.Dialogue:
					ShowViewModel<DialogueViewModel>(new { id = e.Exercise.ExerciseId });
					break;
			}
            //var ex = Lessons[e.LessonIndex][e.TopicIndex].Exercises[e.ExerciseIndex];
            //switch (ex.Type)
            //{
               // case ExerciseModel.ExerciseType.Memorise:
               //     ShowViewModel<MemoriseViewModel>(ex.ExerciseId);
                 //   break;
//                case ExerciseModel.ExerciseType.Vocabulary:
                // ShowViewModel<VocabularyViewModel>(ex.ExerciseId);
                  //  break;
                //case ExerciseModel.ExerciseType.Dialogue:
                //    ShowViewModel<DialogueFillListSentenceViewModel>(ex.ExerciseId);
              //      break;
            //}
        }
        #endregion Command

    }
}
