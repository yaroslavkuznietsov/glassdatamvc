using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassData.DataLibrary.Enums
{
    public class LinePos
    {
        public string Line { get; set; }

        List<LinePos> lines = new List<LinePos>()
        {
            new LinePos() {Line = "L01"},
            new LinePos() {Line = "L02"},
            new LinePos() {Line = "L03"}
        };
    }
}
