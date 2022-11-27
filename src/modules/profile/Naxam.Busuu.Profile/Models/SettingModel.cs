using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Profile.Models
{
    public class SettingModel : MvxMessage
    {
        public SettingModel(object sender) : base(sender)
        {
        }
        
        public string Key { get; set; }
        public UserModel Value { get; set; }
    }
}
