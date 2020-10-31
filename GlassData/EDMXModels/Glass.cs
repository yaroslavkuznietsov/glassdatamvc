//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EDMXModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Glass
    {
        public int Id { get; set; }
        public string TimeStamp { get; set; }
        public string LinePos { get; set; }
        public string SourcePos { get; set; }
        public int SourceSide { get; set; }
        public string GlassId { get; set; }
        public decimal GlassHeight { get; set; }
        public decimal GlassWidth { get; set; }
        public string GlassThickness { get; set; }
        public decimal GlassWeight { get; set; }
        public string DestRackPos { get; set; }
        public string DestRackSide { get; set; }
        public decimal PreviousHeight { get; set; }
        public decimal PreviousWidth { get; set; }
        public string GlassResult { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
