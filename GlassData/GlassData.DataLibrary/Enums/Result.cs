using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassData.DataLibrary.Enums
{
    public enum Result
    {
        [Description("None")]
        None = 0,
        [Description("Done")]
        Done = 1,
        [Description("Lost")]
        Lost = 2
    }
}
