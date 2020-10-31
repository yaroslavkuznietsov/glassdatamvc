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

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime TimeStamp { get; set; }

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
        public decimal GlassHeight { get; set; }

        [Required]
        public decimal GlassWidth { get; set; }

        [Required]
        public decimal GlassThickness { get; set; }

        [Required]
        public decimal GlassWeight { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Maximal Length is 10")]
        public string DestRackPos { get; set; }

        [Required]
        public Side DestRackSide { get; set; }

        [Required]
        public decimal PreviousHeight { get; set; }

        [Required]
        public decimal PreviousWidth { get; set; }

        [Required]
        public Result GlassResult { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<int> CustomerId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
