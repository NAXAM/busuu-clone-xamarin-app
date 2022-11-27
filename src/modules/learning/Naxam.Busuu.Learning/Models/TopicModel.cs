using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.Models
{
    public class TopicModel : MvxNotifyPropertyChanged
    {
        private string _color;

        public string Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value; 
                    RaisePropertyChanged();
                }
            }
        }

        private string _toppic;

        public string Toppic
        {
            get { return _toppic; }
            set
            {
                if (_toppic != value)
                {
                    _toppic = value;
                    RaisePropertyChanged();
                }
            }
        }
        //minutes
        private int _time;

        public int Time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<ExerciseModel> _exercises;

        public IList<ExerciseModel> Exercises
        {
            get { return _exercises; }
            set
            {
                if (_exercises != value)
                {
                    _exercises = value; 
                    RaisePropertyChanged();
                }
            }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged();
                }
            }
        }

		private int _lessonId;

		public int LessonId
		{
			get { return _lessonId; }
			set
			{
				if (_lessonId != value)
				{
					_lessonId = value;
					RaisePropertyChanged();
				}
			}
		}

    }
}
