﻿using System;
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
    public class OrdersController : Controller
    {
        private GlassContext db = new GlassContext();

        private DisconnectedRepository _repo = new DisconnectedRepository();

        private OrdersViewModel ordersViewModel = new OrdersViewModel();

        // GET: Orders
        public ActionResult Index()
        {
            #region EF6 Code
            //var orderSet = db.OrderSet.Include(o => o.Customer);
            //return View(orderSet.ToList()); 
            #endregion

            var orders = _repo.GetOrdersWithCustomers();
            return View(orders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.OrderSet.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create(int customerId, string customerName)
        {
            ViewBag.CustomerId = customerId;
            ViewBag.CustomerName = customerName;

            
            ViewBag.CustomerId = new SelectList(_repo.GetCustomerList(), "Id", "Name", customerId);

            #region EF6 Code
            //ViewBag.CustomerId = new SelectList(db.CustomerSet, "Id", "Name"); 
            #endregion
            return View();

        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Number,DateTime,CustomerId")] Order order)
        {
            #region EF6 Code
            //if (ModelState.IsValid)
            //{
            //    db.OrderSet.Add(order);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.CustomerId = new SelectList(db.CustomerSet, "Id", "Name", order.CustomerId);
            //return View(order); 
            #endregion

            if (ModelState.IsValid)
            {
                _repo.SaveNewOrder(order);
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(_repo.GetCustomerList(), "Id", "Name", order.CustomerId);
            return View(order); 
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.OrderSet.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.CustomerSet, "Id", "Name", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Number,DateTime,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.CustomerSet, "Id", "Name", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.OrderSet.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.OrderSet.Find(id);
            db.OrderSet.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}