using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Core.Models
{
    public enum NotificationType { Reply, Correct, Accpect, Request, Like };

    public class NotificationModelBase: NotificationModel
    { 
        private int _CountRequest;

        public int CountRequest
        {
            get { return _CountRequest; }
            set
            {
                if (_CountRequest != value)
                {
                    _CountRequest = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
