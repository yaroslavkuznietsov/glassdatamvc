using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GlassData.DataLibrary.Models;
using GlassData.DataModel;
using GlassData.Web.ViewModels;

namespace GlassData.Web.Controllers
{
    public class CustomersController : Controller
    {
        //private GlassContext db = new GlassContext();

        private readonly DisconnectedRepository _repo = new DisconnectedRepository();

        private CustomersViewModel customersViewModel = new CustomersViewModel();

        // GET: Customers
        public ActionResult Index()
        {
            #region EF6 Code
            //return View(db.CustomerSet.ToList()); 
            #endregion

            var customers = _repo.GetCustomersWithOrders();
            customersViewModel.Customers.Clear();
            foreach (var item in customers)
            {
                customersViewModel.Customers.Add(item);
            }
            customersViewModel.DataFilter.Count = customersViewModel.Customers.Count();
            return View(customersViewModel);
        }


        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            #region EF6 Code
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Customer customer = db.CustomerSet.Find(id);
            //if (customer == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(customer); 
            #endregion

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = _repo.GetCustomerWithOrders(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Phone")] Customer customer)
        {
            #region EF6 Code
            //if (ModelState.IsValid)
            //{
            //    db.CustomerSet.Add(customer);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //} 
            #endregion

            if (ModelState.IsValid)
            {
                _repo.SaveNewCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            #region EF6 Code
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Customer customer = db.CustomerSet.Find(id);
            //if (customer == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(customer); 
            #endregion

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = _repo.GetCustomerWithOrders(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Phone")] Customer customer)
        {
            #region EF6 Code
            //if (ModelState.IsValid)
            //{
            //    db.Entry(customer).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(customer); 
            #endregion

            if (ModelState.IsValid)
            {
                _repo.SaveUpdatedCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            #region EF6 Code
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Customer customer = db.CustomerSet.Find(id);
            //if (customer == null)
            //{
            //    return HttpNotFound();
            //} 
            #endregion

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = _repo.GetCustomerById(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            #region EF6 Code
            //Customer customer = db.CustomerSet.Find(id);
            //db.CustomerSet.Remove(customer);
            //db.SaveChanges(); 
            #endregion

            _repo.DeleteCustomer(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            //base.Dispose(disposing);
        }
    }
}
