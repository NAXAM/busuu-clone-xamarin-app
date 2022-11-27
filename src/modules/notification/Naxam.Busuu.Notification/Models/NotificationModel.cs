using System;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Notification.Models
{
	public enum ViewType { Reply, Correct, Accpect, Request, Like };

	public class NotificationModel : MvxNotifyPropertyChanged
    {
		ViewType _TypeView;

		public ViewType TypeView
		{
			get { return _TypeView; }
			set
			{
				if (_TypeView != value)
				{
					_TypeView = value;
					RaisePropertyChanged();
				}
			}
		}
        
		int _Id;

		public int Id
		{
			get { return _Id; }
			set
			{
				if (_Id != value)
				{
					_Id = value;
					RaisePropertyChanged();
				}
			}
		}

		string _ImgUser;

		public string ImgUser
		{
			get { return _ImgUser; }
			set
			{
				if (_ImgUser != value)
				{
					_ImgUser = value;
					RaisePropertyChanged();
				}
			}
		}

        string _NameUser;

		public string NameUser
		{
			get { return _NameUser; }
			set
			{
				if (_NameUser != value)
				{
					_NameUser = value;
					RaisePropertyChanged();
				}
			}
		}

		string _Details;

		public string Details
		{
			get { return _Details; }
			set
			{
				if (_Details != value)
				{
					_Details = value;
					RaisePropertyChanged();
				}
			}
		}

        DateTime _Time;

        public DateTime Time
		{
			get { return _Time; }
			set
			{
				if (_Time != value)
				{
					_Time = value;
					RaisePropertyChanged();
				}
			}
		}

		bool _Check;

		public bool Check
		{
			get { return _Check; }
			set
			{
				if (_Check != value)
				{
					_Check = value;
					RaisePropertyChanged();
				}
			}
		}

        bool _Friends;

		public bool Friends
		{
			get { return _Friends; }
			set
			{
				if (_Friends != value)
				{
					_Friends = value;
					RaisePropertyChanged();
				}
			}
		}
    }
}
