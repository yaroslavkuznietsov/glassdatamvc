using GlassData.DataLibrary.Models;
using GlassData.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlassData.Web.Converter
{
    public static class ModelConverter
    {
        public static Order ConvertToOrderModel(OrderViewModel model)
        {
            Order order = new Order();

            order.Id = model.Id;
            order.Number = model.Number;
            order.DateTime = model.DateTime;
            order.CustomerId = model.CustomerId;
            order.Customer = model.Customer;
            order.GlassesList = model.GlassesList;

            //order.GlassesList.Clear();
            //foreach (var item in model.GlassesList)
            //{
            //    order.GlassesList.Add(item);
            //}

            return order;
        }

        public static OrderViewModel ConvertToOrderViewModel(Order model)
        {
            OrderViewModel order = new OrderViewModel();

            order.Id = model.Id;
            order.Number = model.Number;
            order.DateTime = model.DateTime;
            order.CustomerId = model.CustomerId;
            order.Customer = model.Customer;
            order.GlassesList = model.GlassesList;

            return order;
        }
    }
}