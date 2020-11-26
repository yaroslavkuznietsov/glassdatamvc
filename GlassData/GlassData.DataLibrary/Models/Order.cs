using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassData.DataLibrary.Models
{
    public class Order
    {
        public Order()
        {
            GlassesList = new List<Glass>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximal Length is 50")]
        [Display(Name = "Order Nr.")]
        public string Number { get; set; }
        public string DateTime { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public List<Glass> GlassesList { get; set; }
    }
}
