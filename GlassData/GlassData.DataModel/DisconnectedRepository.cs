using GlassData.DataLibrary.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GlassData.DataModel
{
    public class DisconnectedRepository
    {
        public List<Glass> GetQueryableGlassWithOrderCustomer(string query, int page, int pageSize)
        {
            using (var context = new GlassContext())
            {
                context.Database.Log = message => Debug.WriteLine(message);
                var linqQuery = context.GlassSet.Include(g => g.Customer).Include(g => g.Order);
                if (!string.IsNullOrEmpty(query))
                {
                    linqQuery = linqQuery.Where(g => g.GlassId.Contains(query));
                }
                if (page > 0 && pageSize > 0)
                {
                    linqQuery = linqQuery.OrderBy(n => n.GlassId).Skip(page - 1).Take(pageSize);
                }

                return linqQuery.ToList();
            }
        }


        public List<Glass> GetGlassesWithOrder()
        {
            using (var context = new GlassContext())
            {
                //return context.GlassSet.Include(g => g.Customer).Include(g => g.Order).ToList();
                return context.GlassSet.AsNoTracking().Include(g => g.Order).ToList();
            }
        }

        public List<Glass> GetGlassesWithCustomer()
        {
            using (var context = new GlassContext())
            {
                //return context.GlassSet.Include(g => g.Customer).Include(g => g.Order).ToList();
                return context.GlassSet.AsNoTracking().Include(g => g.Customer).ToList();
            }
        }

        public List<Glass> GetGlassesWithOrderCustomer()
        {
            using (var context = new GlassContext())
            {
                //return context.GlassSet.Include(g => g.Customer).Include(g => g.Order).ToList();
                return context.GlassSet.AsNoTracking().Include(g => g.Customer).Include(g => g.Order).ToList();
            }
        }

        public Glass GetGlassWithOrderCustomer(int id)
        {
            using (var context = new GlassContext())
            {
                return context.GlassSet.AsNoTracking().Include(g => g.Customer).Include(g => g.Order).FirstOrDefault(g => g.Id == id);
            }
        }

        public Glass GetGlassById(int id)
        {
            using (var context = new GlassContext())
            {
                return context.GlassSet.Find(id);
                // return context.GlassSet.AsNoTracking().SingleOrDefault(g => g.Id == id);
            }
        }

        public void SaveUpdatedGlass(Glass glass)
        {
            using (var context = new GlassContext())
            {
                context.Entry(glass).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        
        public void SaveNewGlass(Glass glass)
        {
            using (var context = new GlassContext())
            {
                context.GlassSet.Add(glass);
                context.SaveChanges();
            }
        }

        public void DeleteGlass(int glassId)
        {
            using (var context = new GlassContext())
            {
                var glass = context.GlassSet.Find(glassId);
                context.Entry(glass).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public IEnumerable GetCustomerList()
        {
            using (var context = new GlassContext())
            {
                return context.CustomerSet.AsNoTracking().OrderBy(c => c.Name)
                  .Select(c => new { c.Id, c.Name, c.Address, c.Phone, c.OrdersList, c.GlassesList }).ToList();
            }
        }

        public IEnumerable GetOrderList()
        {
            using (var context = new GlassContext())
            {
                return context.OrderSet.AsNoTracking().OrderBy(o => o.Number)
                  .Select(o => new { o.Id, o.Number, o.DateTime, o.Customer, o.CustomerId,  }).ToList();
            }
        }
    }
}
