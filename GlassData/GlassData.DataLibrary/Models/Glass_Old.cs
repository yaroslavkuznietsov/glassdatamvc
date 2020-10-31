using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassData.DataLibrary.Models
{
    public class Glass_Old
    {
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public string LinePos { get; set; }

        public string SourcePos { get; set; }

        public int SourceSide { get; set; }

        public string GlassId { get; set; }

        public float GlassHeight { get; set; }

        public float GlassWidth { get; set; }

        public float GlassThickness { get; set; }

        public float GlassWeight { get; set; }

        public string DestRackPos { get; set; }

        public int DestRackSide { get; set; }

        public float PreviousHeight { get; set; }

        public float PreviousWidth { get; set; }

        public string GlassResult { get; set; }
    }
}
