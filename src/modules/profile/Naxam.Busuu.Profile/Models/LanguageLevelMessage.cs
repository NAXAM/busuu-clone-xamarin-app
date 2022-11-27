using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.Models
{
    public class LanguageLevelMessage : MvxMessage
    {
        public LanguageLevelMessage(object sender) : base(sender)
        {
        }

        public bool Accept { get; set; }
        public LanguageLevel Level { set; get; }
    }
}
