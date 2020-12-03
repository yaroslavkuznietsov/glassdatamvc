using GlassData.DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlassData.Web.ViewModels
{
    public class OrdersViewModel
    {
        /// <summary>
        /// Server Models
        /// </summary>
        public List<Glass> Glasses = new List<Glass>();
        public List<Order> Oders = new List<Order>();
        public List<Customer> Customers = new List<Customer>();

        /// <summary>
        /// View Models
        /// </summary>
        public DataFilterModel DataFilter = new DataFilterModel();
    }
}