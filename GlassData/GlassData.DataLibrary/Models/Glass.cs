using GlassData.DataLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassData.DataLibrary.Models
{
    public class Glass
    {
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(10, ErrorMessage ="Maximal Length is 10")]
        public string LinePos { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Maximal Length is 10")]
        public string SourcePos { get; set; }

        [Required]
        public Side SourceSide { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximal Length is 50")]
        public string GlassId { get; set; }

        [Required]
        [Display(Name = "Height")]
        public decimal GlassHeight { get; set; }

        [Required]
        [Display(Name = "Width")]
        public decimal GlassWidth { get; set; }

        [Required]
        [Display(Name = "Thickness")]
        public decimal GlassThickness { get; set; }

        [Required]
        [Display(Name = "Weight")]
        public decimal GlassWeight { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Maximal Length is 10")]
        public string DestRackPos { get; set; }

        [Required]
        public Side DestRackSide { get; set; }

        [Required]
        [Display(Name = "Previous \n"+"Height")]
        public decimal PreviousHeight { get; set; }

        [Required]
        [Display(Name = "Previous \n" + "Width")]
        public decimal PreviousWidth { get; set; }

        [Required]
        public Result GlassResult { get; set; }
        public int? OrderId { get; set; }
        public int? CustomerId { get; set; }

        public Order Order { get; set; }
        public Customer Customer { get; set; }
    }
}
