using GlassData.DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassData.DataModel
{
    public class GlassContext : DbContext
    {
        public GlassContext()
        : base("name=DefaultConnection")
        {
        }

        public DbSet<Glass> GlassSet { get; set; }
        public DbSet<Customer> CustomerSet { get; set; }
        public DbSet<Order> OrderSet { get; set; }
    }
}
