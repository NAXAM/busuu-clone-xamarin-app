using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Review.Models
{
    public class SearchModel : MvxMessage
    {
        public string Text { get; set; }
        public SearchModel(object sender) : base(sender)
        {
        }
    }
}
