using System;
using System.ComponentModel;
using System.Threading;
using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;

namespace HomeGardenShop.Models
{
	public class User : INotifyPropertyChanged
    {

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnNotifyPropertyChanged("Id");


                }
            }
        }

        private string _name = "<unknown>";
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnNotifyPropertyChanged("Name");


                }
            }
        }

        private string _address = "<unknown>";
        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnNotifyPropertyChanged("Address");


                }
            }
        }

        private string _mail = "<unknown>";
        public string Mail
        {
            get { return _mail; }
            set
            {
                if (_mail != value)
                {
                    _mail = value;
                    OnNotifyPropertyChanged("Mail");


                }
            }
        }

        private Validatable<string> _phone;
        public Validatable<string> Phone
        {
            get => _phone;
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnNotifyPropertyChanged("Phone");

                }
            }
        }

        private string _codePhone;
        public string CodePhone
        {
            get { return _codePhone; }
            set
            {
                try
                {
                    if (_codePhone != value)
                    {
                        _codePhone = value;
                        OnNotifyPropertyChanged("CodePhone");

                    }
                }
                catch (Exception ex)
                { }

            }
        }

        private string _photoUrl = "Default.png";
        public string PhotoUrl
        {
            get { return _photoUrl; }
            set
            {
                if (_photoUrl != value)
                {
                    _photoUrl = value;
                    OnNotifyPropertyChanged("PhotoUrl");

                }
            }
        }

        public void SetValue(User user)
        {
            Name = user.Name;
            Mail = user.Mail;
            Address = user.Address;
            Phone.Value = user.Phone.Value;
            Id = user.Id;
            PhotoUrl = user.PhotoUrl;
            CodePhone = user.CodePhone;
        }
        public void SignOut()
        {
            Name = "";
            Mail = "";
            Address = "";
            Phone.Value = "";
            Id = "";
            PhotoUrl = "Default.png";
            CodePhone = "";
        }
        public User()
        {
            Phone = Validator.Build<string>()
             .IsRequired("Empty");
            Phone.Value = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnNotifyPropertyChanged(String info)
        {
            PropertyChangedEventHandler tmp = Interlocked.CompareExchange(ref PropertyChanged, null, null);
            if (tmp != null) { tmp(this, new PropertyChangedEventArgs(info)); }
        }
    }
}

