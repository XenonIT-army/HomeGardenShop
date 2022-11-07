using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace HomeGardenShop.Models
{
	public class Order : INotifyPropertyChanged
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
        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    OnNotifyPropertyChanged("UserId");


                }
            }
        }

        private List<Product> _products;
        public List<Product> Products
        {
            get { return _products; }
            set
            {
                if (_products != value)
                {
                    _products = value;
                    OnNotifyPropertyChanged("Products");


                }
            }
        }

        private int _statusId;
        public int StatusId
        {
            get { return _statusId; }
            set
            {
                if (_statusId != value)
                {
                    _statusId = value;
                    OnNotifyPropertyChanged("StatusId");


                }
            }
        }

        private double _sum;
        public double Sum
        {
            get { return _sum; }
            set
            {
                if (_sum != value)
                {
                    _sum = value;
                    OnNotifyPropertyChanged("Sum");


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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnNotifyPropertyChanged(String info)
        {
            PropertyChangedEventHandler tmp = Interlocked.CompareExchange(ref PropertyChanged, null, null);
            if (tmp != null) { tmp(this, new PropertyChangedEventArgs(info)); }
        }

        public Order()
        {
            if (Products == null)
            {
                Products = new List<Product>();
            }
            else
            {
                Products.Clear();
            }
        }
        public Order(Order order)
        {
            this.Id = order.Id;
            this.Products = order.Products;
            this.StatusId = order.StatusId;
            this.Sum = order.Sum;
            this.UserId = order.UserId;
            this.DataTime = order.DataTime;
        }
    }

    public enum OrderStatus
    {
        Error = 1,
        Make = 2,
        InProcess = 3,
        Formed = 4,
        Complete = 5,
        Canceled = 6
    }
}

