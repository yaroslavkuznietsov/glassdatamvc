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
        None = 1,
        [Description("Done")]
        Done = 2,
        [Description("Lost")]
        Lost = 3
    }
}
