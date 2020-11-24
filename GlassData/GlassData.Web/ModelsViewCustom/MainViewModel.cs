using GlassData.DataLibrary.Models;
using GlassData.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlassData.Web.ModelsViewCustom
{
    public class MainViewModel
    {
        public List<Glass> Glasses = new List<Glass>();
        public List<Order> Oders = new List<Order>();
        public List<Customer> Customers = new List<Customer>();

        public DataFilterModel DataFilter = new DataFilterModel();
    }
}