﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlassData.Web.ModelsViewCustom
{
    public class DataFilterModel
    {
        //public int Id { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? Count { get; set; }
        public DataFilterModel()
        {

        }
    }
}