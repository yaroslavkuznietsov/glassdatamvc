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
        /// <summary>
        /// Glass
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
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
                return context.GlassSet.AsNoTracking()
                    .Include(g => g.Order).ToList();
            }
        }

        public List<Glass> GetGlassesWithCustomer()
        {
            using (var context = new GlassContext())
            {
                //return context.GlassSet.Include(g => g.Customer).Include(g => g.Order).ToList();
                return context.GlassSet.AsNoTracking()
                    .Include(g => g.Customer).ToList();
            }
        }

        public List<Glass> GetGlassesWithOrderCustomer()
        {
            using (var context = new GlassContext())
            {
                //return context.GlassSet.Include(g => g.Customer).Include(g => g.Order).ToList();
                return context.GlassSet.AsNoTracking()
                    .Include(g => g.Customer)
                    .Include(g => g.Order).ToList();
            }
        }

        public Glass GetGlassWithOrderCustomer(int id)
        {
            using (var context = new GlassContext())
            {
                return context.GlassSet.AsNoTracking()
                    .Include(g => g.Customer)
                    .Include(g => g.Order)
                    .FirstOrDefault(g => g.Id == id);
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
                return context.CustomerSet.AsNoTracking()
                    .OrderBy(c => c.Name)
                    .Select(c => new { c.Id, c.Name }).ToList();  //, c.Address, c.Phone, c.OrdersList, c.GlassesList
            }
        }

        public IEnumerable GetOrderList()
        {
            using (var context = new GlassContext())
            {
                return context.OrderSet.AsNoTracking()
                    .OrderBy(o => o.Number)
                    .Select(o => new { o.Id, o.Number }).ToList();    //, o.DateTime, o.Customer, o.CustomerId
            }
        }


        /// <summary>
        /// Customer
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomersWithOrders()
        {
            using (var context = new GlassContext())
            {
                //return context.GlassSet.Include(g => g.Customer).Include(g => g.Order).ToList();
                return context.CustomerSet.AsNoTracking()
                    .OrderBy(c => c.Name)
                    .Include(c => c.OrdersList).ToList();
            }
        }

        public Customer GetCustomerWithOrders(int id)
        {
            using (var context = new GlassContext())
            {
                return context.CustomerSet.AsNoTracking().Include(c => c.OrdersList)
                  .FirstOrDefault(n => n.Id == id);
            }
        }

        public void SaveUpdatedCustomer (Customer customer)
        {
            using (var context = new GlassContext())
            {
                context.Entry(customer).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void SaveNewCustomer(Customer customer)
        {
            using (var context = new GlassContext())
            {
                context.CustomerSet.Add(customer);
                context.SaveChanges();
            }
        }

        public Customer GetCustomerById(int id)
        {
            using (var context = new GlassContext())
            {
                return context.CustomerSet.Find(id);
                // return context.CustomerSet.AsNoTracking().SingleOrDefault(n => n.Id == id);
            }
        }

        public void DeleteCustomer(int customerId)
        {
            using (var context = new GlassContext())
            {
                var customer = context.CustomerSet.Find(customerId);
                context.Entry(customer).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }


        /// <summary>
        /// Order
        /// </summary>
        /// <returns></returns>
        public List<Order> GetOrdersWithCustomers()
        {
            using (var context = new GlassContext())
            {
                //return context.GlassSet..OrderBy(o => o.Number).Include(o => o.Customer).Include(o => o.GlassesList).ToList();
                return context.OrderSet.AsNoTracking()
                    .OrderBy(o => o.Number)
                    .Include(o => o.Customer)
                    .Include(o => o.GlassesList).ToList();
            }
        }

        public void SaveNewOrder(Order order)   //, int customerId
        {
            using (var context = new GlassContext())
            {
                context.OrderSet.Add(order);
                context.SaveChanges();
            }
        }

        public void SaveUpdatedOrder(Order order)
        {
            using (var context = new GlassContext())
            {
                context.Entry(order).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Order GetOrderWithCustomer(int id)
        {
            using (var context = new GlassContext())
            {
                return context.OrderSet.AsNoTracking()
                    .Include(o => o.Customer)
                    .FirstOrDefault(o => o.Id == id);
            }
        }

        public Order GetOrderById(int id)
        {
            using (var context = new GlassContext())
            {
                return context.OrderSet.Find(id);
            }
        }

        //public Order GetOrderById(int id)
        //{
        //    using (var context = new GlassContext())
        //    {
        //        Order order = context.OrderSet.Find(id);
        //        Customer customer = GetCustomerById(order.CustomerId);
        //        order.Customer = customer;
        //        return order;
        //    }
        //}

        public void DeleteOrder(int orderId)
        {
            using (var context = new GlassContext())
            {
                var order = context.OrderSet.Find(orderId);
                context.Entry(order).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
