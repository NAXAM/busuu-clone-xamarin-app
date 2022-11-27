using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Profile.Models
{
    public class FriendListModel : UserModel
    {
        private int _Count;

        public int Count
        {
            get { return _Count; }
            set
            {
                if (_Count != value)
                {
                    _Count = value;
                    RaisePropertyChanged();
                }
            }
        }

    }
}