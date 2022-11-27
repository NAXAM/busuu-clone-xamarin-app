using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Core.Models
{
    public enum Gender
    {
        Male,
        Female,
        Undisclosed
    }
    public class UserModel : MvxNotifyPropertyChanged
    {
        private int _Id;

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


        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _photo;

        public string Photo
        {
            get { return _photo; }
            set
            {
                if (_photo != value)
                {
                    _photo = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _aboutMe;

        public string AboutMe
        {
            get { return _aboutMe; }
            set
            {
                if (_aboutMe != value)
                {
                    _aboutMe = value;
                    RaisePropertyChanged();
                }
            }
        }

        
        private Gender _Gender;

        public Gender Gender
        {
            get { return _Gender; }
            set
            {
                if (_Gender != value)
                {
                    _Gender = value;
                    RaisePropertyChanged();
                }
            }
        }

        private CountryModel _country;

        public CountryModel Country
        {
            get { return _country; }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    RaisePropertyChanged();
                }
            }
        }

   

        private IList<LanguageModel> _language;

        public IList<LanguageModel> Languages
        {
            get { return _language; }
            set
            {
                if (_language != value)
                {
                    _language = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<SocialModel> _MyExercises;

        public IList<SocialModel> MyExercises
        {
            get { return _MyExercises; }
            set
            {
                if (_MyExercises != value)
                {
                    _MyExercises = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<SocialModel> _MyCorrections;

        public IList<SocialModel> MyCorrections
        {
            get { return _MyCorrections; }
            set
            {
                if (_MyCorrections != value)
                {
                    _MyCorrections = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<UserModel> _FriendsRequest;

        public IList<UserModel> FriendsRequest
        {
            get { return _FriendsRequest; }
            set
            {
                if (_FriendsRequest != value)
                {
                    _FriendsRequest = value;
                    RaisePropertyChanged();
                }
            }
        }


        private IList<UserModel> _friends;

        public IList<UserModel> Friends
        {
            get { return _friends; }
            set
            {
                if (_friends != value)
                {
                    _friends = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _avatarImage;

        public string avatarImage
        {
            get { return _avatarImage; }
            set
            {
                if (_avatarImage != value)
                {
                    _avatarImage = value;
                    RaisePropertyChanged();
                }
            }
        }


        private string _username;

        public string username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    RaisePropertyChanged();
                }
            }
        }


        private string _password;

        public string password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _selfDescription;

        public string selfDescription
        {
            get { return _selfDescription; }
            set
            {
                if (_selfDescription != value)
                {
                    _selfDescription = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _fullName;

        public string fullName
        {
            get { return _fullName; }
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    RaisePropertyChanged();
                }
            }
        }


        

        private int _gender;

        public int gender
        {
            get { return _gender; }
            set
            {
                if (_gender != value)
                {
                    _gender = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<LanguageModel> _speakLanguages;

        public IList<LanguageModel> SpeakLanguages
        {
            get { return _speakLanguages; }
            set
            {
                if (_speakLanguages != value)
                {
                    _speakLanguages = value;
                    RaisePropertyChanged();
                }
            }
        }

        private LanguageModel _interfaceLanguage;

        public LanguageModel interfaceLanguage
        {
            get { return _interfaceLanguage; }
            set
            {
                if (_interfaceLanguage != value)
                {
                    _interfaceLanguage = value;
                    RaisePropertyChanged();
                }
            }
        }


        private List<VoucherModel> _voucher;

        public List<VoucherModel> voucher
        {
            get { return _voucher; }
            set
            {
                if (_voucher != value)
                {
                    _voucher = value;
                    RaisePropertyChanged();
                }
            }
        }


    }
}
