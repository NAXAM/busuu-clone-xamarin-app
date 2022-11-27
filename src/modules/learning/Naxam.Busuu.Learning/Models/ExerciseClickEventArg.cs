using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.Models
{
    public class ExerciseClickEventArg
    {
        public int LessonIndex { get; set; }
        public int TopicIndex { get; set; }
        public int ExerciseIndex { set; get; }
        public LessonModel Lesson { get; set; }
        public TopicModel Topic { get; set; }
        public ExerciseModel Exercise { set; get; }

    }
}
