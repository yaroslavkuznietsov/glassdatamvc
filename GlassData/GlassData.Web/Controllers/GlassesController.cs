using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GlassData.DataLibrary.Models;
using GlassData.DataModel;
using GlassData.Web.ViewModels;

namespace GlassData.Web.Controllers
{
    public class GlassesController : Controller
    {
        private readonly DisconnectedRepository _repo = new DisconnectedRepository();

        //private readonly GlassContext db = new GlassContext();

        private GlassesViewModel glassesViewModel = new GlassesViewModel();


        // GET: Glasses
        public ActionResult Index()
        {
            if (glassesViewModel.Glasses.Count == 0)
            {
                var glasses = _repo.GetGlassesWithOrder()
                    .OrderByDescending(g => g.TimeStamp).Take(100);
                glassesViewModel.Glasses.Clear();
                foreach (var item in glasses)
                {
                    glassesViewModel.Glasses.Add(item);
                } 
            }
            glassesViewModel.DataFilter.Count = glassesViewModel.Glasses.Count();
            return View(glassesViewModel);
        }

                
        // POST: Glasses/Index
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DataFilterModel dataFilter)
        {
            if (dataFilter.DateStart == DateTime.MinValue || dataFilter.DateStart == null)
            {
                dataFilter.DateStart = DateTime.Now.AddDays(-5);
            }
            if (dataFilter.DateEnd == DateTime.MinValue || dataFilter.DateEnd == null)
            {
                dataFilter.DateEnd = DateTime.Now;
            }
            DateTime dt1 = (DateTime)dataFilter.DateStart;
            DateTime dt2 = (DateTime)dataFilter.DateEnd;
            var glasses = _repo.GetGlassesWithOrder()
                .Where(g => g.TimeStamp >= dt1 && g.TimeStamp <= dt2)
                .OrderBy(g => g.TimeStamp);
            glassesViewModel.Glasses.Clear();
            foreach (var item in glasses)
            {
                glassesViewModel.Glasses.Add(item);
            }
            glassesViewModel.DataFilter.DateStart = dt1;
            glassesViewModel.DataFilter.DateEnd = dt2;
            glassesViewModel.DataFilter.Count = glassesViewModel.Glasses.Count();
            return View(glassesViewModel);
        }


        // GET: Glasses/Details/5
        public ActionResult Details(int? id)
        {
            #region EF6 Code
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Glass glass = db.GlassSet.Find(id);
            //if (glass == null)
            //{
            //    return HttpNotFound();
            //} 
            //return View(glass);
            #endregion

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var glass = _repo.GetGlassWithOrderCustomer(id.Value);
            if (glass == null)
            {
                return HttpNotFound();
            }
            return View(glass);
        }


        // GET: Glasses/Create
        public ActionResult Create()
        {
            #region EF6 Code
            //ViewBag.CustomerId = new SelectList(db.CustomerSet, "Id", "Name");
            //ViewBag.OrderId = new SelectList(db.OrderSet, "Id", "Number"); 
            #endregion

            ViewBag.CustomerId = new SelectList(_repo.GetCustomerList(), "Id", "Name"); //, "Address", "Phone", "OrdersList", "GlassesList"
            ViewBag.OrderId = new SelectList(_repo.GetOrderList(), "Id", "Number"); //, "DateTime", "Customer", "CustomerId", "GlassesList"
            return View();
        }

        // POST: Glasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TimeStamp,LinePos,SourcePos,SourceSide,GlassId,GlassHeight,GlassWidth,GlassThickness,GlassWeight,DestRackPos,DestRackSide,PreviousHeight,PreviousWidth,GlassResult,OrderId,CustomerId")] Glass glass)
        {
            #region EF6 Code
            //if (ModelState.IsValid)
            //{
            //    db.GlassSet.Add(glass);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.CustomerId = new SelectList(db.CustomerSet, "Id", "Name", glass.CustomerId);
            //ViewBag.OrderId = new SelectList(db.OrderSet, "Id", "Number", glass.OrderId);
            //return View(glass);
            #endregion

            if (glass.TimeStamp == DateTime.MinValue)
            {
                glass.TimeStamp = DateTime.Now;
            }
            if (ModelState.IsValid)
            {
                _repo.SaveNewGlass(glass);
                return RedirectToAction("Index");
            }

            #region EF& Code
            //ViewBag.CustomerId = new SelectList(db.CustomerSet, "Id", "Name", glass.CustomerId);
            //ViewBag.OrderId = new SelectList(db.OrderSet, "Id", "Number", glass.OrderId); 
            #endregion

            ViewBag.CustomerId = new SelectList(_repo.GetCustomerList(), "Id", "Name", glass.CustomerId);
            ViewBag.OrderId = new SelectList(_repo.GetOrderList(), "Id", "Number", glass.CustomerId);
            return View(glass);
        }


        // GET: Glasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Glass glass = _repo.GetGlassWithOrderCustomer(id.Value);
            if (glass == null)
            {
                return HttpNotFound();
            }

            #region EF6 Code
            //ViewBag.CustomerId = new SelectList(db.CustomerSet, "Id", "Name", glass.CustomerId);
            //ViewBag.OrderId = new SelectList(db.OrderSet, "Id", "Number", glass.OrderId); 
            #endregion

            ViewBag.CustomerId = new SelectList(_repo.GetCustomerList(), "Id", "Name", glass.CustomerId);
            ViewBag.OrderId = new SelectList(_repo.GetOrderList(), "Id", "Number", glass.CustomerId);

            return View(glass);
        }


        // POST: Glasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TimeStamp,LinePos,SourcePos,SourceSide,GlassId,GlassHeight,GlassWidth,GlassThickness,GlassWeight,DestRackPos,DestRackSide,PreviousHeight,PreviousWidth,GlassResult,OrderId,CustomerId")] Glass glass)
        {
            #region EF6 Code
            //if (ModelState.IsValid)
            //{
            //    db.Entry(glass).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.CustomerId = new SelectList(db.CustomerSet, "Id", "Name", glass.CustomerId);
            //ViewBag.OrderId = new SelectList(db.OrderSet, "Id", "Number", glass.OrderId);
            //return View(glass); 
            #endregion

            if (ModelState.IsValid)
            {
                _repo.SaveUpdatedGlass(glass);
                return RedirectToAction("Index");
            }

            #region EF6 Code
            //ViewBag.CustomerId = new SelectList(db.CustomerSet, "Id", "Name", glass.CustomerId);
            //ViewBag.OrderId = new SelectList(db.OrderSet, "Id", "Number", glass.OrderId); 
            #endregion

            ViewBag.CustomerId = new SelectList(_repo.GetCustomerList(), "Id", "Name", glass.CustomerId);
            ViewBag.OrderId = new SelectList(_repo.GetOrderList(), "Id", "Number", glass.CustomerId);

            return View(glass);
        }

        // GET: Glasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Glass glass = _repo.GetGlassById(id.Value);
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
            #region EF6 Code
            //Glass glass = db.GlassSet.Find(id);
            //db.GlassSet.Remove(glass);
            //db.SaveChanges();
            //return RedirectToAction("Index"); 
            #endregion

            _repo.DeleteGlass(id);
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    #region EF6 Code
            //    //db.Dispose(); 
            //    #endregion

            //    //_repo.Dispose();
            //}
            base.Dispose(disposing);
        }
    }
}
