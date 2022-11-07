using System;
using System.ComponentModel;
using System.Threading;
using Google.Protobuf;
using Xamarin.Forms;

namespace HomeGardenShop.Models
{
	public class Product : INotifyPropertyChanged
	{
        public bool IsBusy { get; set; }

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

        private double _price;
        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnNotifyPropertyChanged("Price");


                }
            }
        }
        private double _countPrice;
        public double CountPrice
        {
            get { return _countPrice; }
            set
            {
                if (_countPrice != value)
                {
                    _countPrice = value;
                    OnNotifyPropertyChanged("CountPrice");


                }
            }
        }

        private double _discountPrice;
        public double DiscountPrice
        {
            get { return _discountPrice; }
            set
            {
                if (_discountPrice != value)
                {
                    _discountPrice = value;
                    OnNotifyPropertyChanged("DiscountPrice");


                }
            }
        }

        private double _count;
        public double Count
        {
            get { return _count; }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    OnNotifyPropertyChanged("Count");

                    if(DiscountPrice > 0)
                    {
                        var res = _count * DiscountPrice;
                        CountPrice = Math.Round(res,2);
                    }
                    else
                    {
                        var res = _count * Price;
                        CountPrice = Math.Round(res, 2);
                    }
                }
            }
        }

        private double _allCount;
        public double AllCount
        {
            get { return _allCount; }
            set
            {
                if (_allCount != value)
                {
                    _allCount = value;
                    OnNotifyPropertyChanged("AllCount");


                }
            }
        }
        private int _categoryId;
        public int CategoryId
        {
            get { return _categoryId; }
            set
            {
                if (_categoryId != value)
                {
                    _categoryId = value;
                    OnNotifyPropertyChanged("Category");


                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
		private void OnNotifyPropertyChanged(String info)
		{
			PropertyChangedEventHandler tmp = Interlocked.CompareExchange(ref PropertyChanged, null, null);
			if (tmp != null) { tmp(this, new PropertyChangedEventArgs(info)); }
		}

        public Product() { }

        public Product(Product product)
        {
            this.Id = product.Id;
            this.AllCount = product.AllCount;
            this.CategoryId = product.CategoryId;
            this.Description = product.Description;
            this.Name = product.Name;
            this.Price = product.Price;
            this.DiscountPrice = product.DiscountPrice;
            this.Image = product.Image;
            this.Count = product.Count;
        }
	}
}

