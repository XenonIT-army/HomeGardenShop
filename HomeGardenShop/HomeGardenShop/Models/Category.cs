using System;
using System.ComponentModel;
using System.Threading;

namespace HomeGardenShop.Models
{
	public class Category : INotifyPropertyChanged
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

        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    OnNotifyPropertyChanged("Count");


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

