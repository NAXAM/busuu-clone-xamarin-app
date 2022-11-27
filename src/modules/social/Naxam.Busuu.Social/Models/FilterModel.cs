using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Social.Models
{
    public class FilterModel : MvxMessage
    {
        public bool FilterLanguage { get; set; }
        public bool FilterSpeaking { get; set; }
        public bool FilterWriting { get; set; }
        public FilterModel(object sender) : base(sender)
        {
        }
    }
}
