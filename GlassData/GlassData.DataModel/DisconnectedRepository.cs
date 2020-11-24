using GlassData.DataLibrary.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Glass GetGlassById(int id)
        {
            using (var context = new GlassContext())
            {
                return context.GlassSet.Find(id);
                // return context.Ninjas.AsNoTracking().SingleOrDefault(n => n.Id == id);
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

        public List<Glass> GetGlassesWithOrderCustomer()
        {
            using (var context = new GlassContext())
            {
                //return context.Ninjas.Include(n => n.Clan).ToList();
                return context.GlassSet.AsNoTracking().Include(g => g.Customer).Include(g => g.Order).ToList();
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
                var ninja = context.GlassSet.Find(glassId);
                context.Entry(ninja).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
