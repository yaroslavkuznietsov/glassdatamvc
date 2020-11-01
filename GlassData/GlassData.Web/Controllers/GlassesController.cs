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

namespace GlassData.Web.Controllers
{
    public class GlassesController : Controller
    {
        private GlassContext db = new GlassContext();

        // GET: Glasses
        public ActionResult Index()
        {
            var glassSet = db.GlassSet.Include(g => g.Customer).Include(g => g.Order);
            return View(glassSet.ToList());
        }

        // GET: Glasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Glass glass = db.GlassSet.Find(id);
            if (glass == null)
            {
                return HttpNotFound();
            }
            return View(glass);
        }

        // GET: Glasses/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.CustomerSet, "Id", "Name");
            ViewBag.OrderId = new SelectList(db.OrderSet, "Id", "Number");
            return View();
        }

        // POST: Glasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TimeStamp,LinePos,SourcePos,SourceSide,GlassId,GlassHeight,GlassWidth,GlassThickness,GlassWeight,DestRackPos,DestRackSide,PreviousHeight,PreviousWidth,GlassResult,OrderId,CustomerId")] Glass glass)
        {
            if (ModelState.IsValid)
            {
                db.GlassSet.Add(glass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.CustomerSet, "Id", "Name", glass.CustomerId);
            ViewBag.OrderId = new SelectList(db.OrderSet, "Id", "Number", glass.OrderId);
            return View(glass);
        }

        // GET: Glasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Glass glass = db.GlassSet.Find(id);
            if (glass == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.CustomerSet, "Id", "Name", glass.CustomerId);
            ViewBag.OrderId = new SelectList(db.OrderSet, "Id", "Number", glass.OrderId);
            return View(glass);
        }

        // POST: Glasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TimeStamp,LinePos,SourcePos,SourceSide,GlassId,GlassHeight,GlassWidth,GlassThickness,GlassWeight,DestRackPos,DestRackSide,PreviousHeight,PreviousWidth,GlassResult,OrderId,CustomerId")] Glass glass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(glass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.CustomerSet, "Id", "Name", glass.CustomerId);
            ViewBag.OrderId = new SelectList(db.OrderSet, "Id", "Number", glass.OrderId);
            return View(glass);
        }

        // GET: Glasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Glass glass = db.GlassSet.Find(id);
            if (glass == null)
            {
                return HttpNotFound();
            }
            return View(glass);
        }

        // POST: Glasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Glass glass = db.GlassSet.Find(id);
            db.GlassSet.Remove(glass);
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
