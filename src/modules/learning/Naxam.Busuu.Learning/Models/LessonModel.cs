using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Linq.Expressions;
using MvvmCross.Platform.UI;

namespace Naxam.Busuu.Learning.Models
{
    public class LessonModel : MvxObservableCollection<TopicModel>
    {
        public LessonModel(IList<TopicModel> collection) : base(collection)
        {
        }

        //public event EventHandler<LessonModel> DownloadHandle;

        private int _Id;

        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    UpdateLessionId();
                }
            }
        }

        private void UpdateLessionId()
        {
            foreach (TopicModel topic in this) {
                topic.LessonId = Id;
            }
        }

        protected override void InsertItem(int index, TopicModel item)
        {
            item.LessonId = Id;
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, TopicModel item)
        {
            item.LessonId = Id;
            base.SetItem(index, item);
        }

        private string _color;

        public string Color
        {
            get { return _color; }
            set
            {
                _color = value; 
            }
        }

        private string _lessonNumber;

        public string LessonNumber
        {
            get { return _lessonNumber; }
            set
            {
                if (_lessonNumber != value)
                {
                    _lessonNumber = value; 
                }
            }
        }

        private string _lessonName;

        public string LessonName
        {
            get { return _lessonName; }
            set
            {
                if (_lessonName != value)
                {
                    _lessonName = value; 
                }
            }
        }


        public int Percent { get; set; }

        public string Icon { get; set; } 

   
         
    }
}
