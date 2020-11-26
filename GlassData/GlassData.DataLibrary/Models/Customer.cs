using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassData.DataLibrary.Models
{
    public class Customer
    {
        public Customer()
        {
            OrdersList = new List<Order>();
            GlassesList = new List<Glass>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximal Length is 50")]
        [Display(Name = "Customer")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<Order> OrdersList { get; set; }
        public List<Glass> GlassesList { get; set; }
    }
}
