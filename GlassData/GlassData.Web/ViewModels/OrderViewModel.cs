using GlassData.DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlassData.Web.ViewModels
{
    public class OrderViewModel
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public OrderViewModel()
        {
            GlassesList = new List<Glass>();
        }

        /// <summary>
        /// Server Model Properties
        /// </summary>
        public int Id { get; set; }
        public string Number { get; set; }
        public string DateTime { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public List<Glass> GlassesList { get; set; }

        /// <summary>
        /// View Model Properties
        /// </summary>
        public string PreviousUrl { get; set; }
    }
}