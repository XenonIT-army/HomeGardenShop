using System;
using System.Collections.Generic;
using HomeGardenShop.Models;

namespace HomeGardenShop.AppManagers
{
	public class AppModel
	{
		public List<Product> Products { get; set; }

        public List<Category> Categorys { get; set; }

        public Order Order { get; set; }

        public List<Order> Orders { get; set; }

        public List<News> News { get; set; }

        public User User { get; set; }

        public string AboutUs { get; set; }

        public AppModel()
        {
			Products = new List<Product>();

            Categorys = new List<Category>();

            Order = new Order();

			Orders = new List<Order>();

            News = new List<News>();

            User = new User();
        }
	}
}

