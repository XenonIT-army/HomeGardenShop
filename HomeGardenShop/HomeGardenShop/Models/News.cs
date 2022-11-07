using System;
using System.ComponentModel;
using System.Threading;
using Google.Protobuf;

namespace HomeGardenShop.Models
{
	public class News : INotifyPropertyChanged
    {
        private int _id;
        public int Id
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

        private DateTime _dataTime;
        public DateTime DataTime
        {
            get { return _dataTime; }
            set
            {
                if (_dataTime != value)
                {
                    _dataTime = value;
                    OnNotifyPropertyChanged("DataTime");


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
                    OnNotifyPropertyChanged("Name");


                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnNotifyPropertyChanged("Description");


                }
            }
        }

        private ByteString _image;
        public ByteString Image
        {
            get { return _image; }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnNotifyPropertyChanged("Image");


                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnNotifyPropertyChanged(String info)
        {
            PropertyChangedEventHandler tmp = Interlocked.CompareExchange(ref PropertyChanged, null, null);
            if (tmp != null) { tmp(this, new PropertyChangedEventArgs(info)); }
        }
    }
}

