using GlassData.DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassData.DataModel
{
    public class DisconnectedRepository
    {
        public void SaveNewGlass(Glass glass)
        {
            using (var context = new GlassContext())
            {
                context.GlassSet.Add(glass);
                context.SaveChanges();
            }
        }
    }
}
