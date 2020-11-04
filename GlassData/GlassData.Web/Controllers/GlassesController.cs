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

namespace GlassData.Web.Controllers
{
    public class GlassesController : Controller
    {
        private readonly DisconnectedRepository _repo = new DisconnectedRepository();

        private GlassContext db = new GlassContext();

        // GET: Glasses
        public ActionResult Index()
        {
            ViewBag.DateTimeSpanSet = new SelectList(db.DateTimeSpanSet, "Id", "DateStart", "DateEnd");

            DateTime dt1 = DateTime.Now.AddDays(-1);
            DateTime dt2 = DateTime.Now;

            //var glassSet = db.GlassSet.Include(g => g.Customer).Include(g => g.Order);
            var glassSet = db.GlassSet.Include(g => g.Customer).Include(g => g.Order).Where(g => g.TimeStamp >= dt1 && g.TimeStamp <= dt2).OrderBy(g=>g.TimeStamp);
            return View(glassSet.ToList());
        }

        /// <summary>
        /// говнокод c попыткой сделать partial view (https://www.c-sharpcorner.com/UploadFile/ff2f08/multiple-models-in-single-view-in-mvc/) 
        /// в которым есть скрипт с выбором даты и времени (https://www.codeproject.com/Articles/1136464/Simplest-Way-to-Use-JQuery-Date-Picker-and-Date-Ti), 
        /// но я так понимаю это не правильно. 
        /// Эти два поля мне нужни здесь в index view чтобы по кнопке Save(submit) передать эти два значения даты и времени 
        /// в [HTTP Post] ActionResult Index  и и сделать query за выбранный период и снова вывести на view
        /// по сути мне нужно вызвать этот сценарий и вернуть значения из поля. Или пох как но мне нужно, 
        /// чтобы вернулись два значения и с ними работать. У самой модели поля нет для этого, 
        /// как вернуть значения назад мне не хватает знаний пока что :((  подскажи что почитать
        /// </summary>
        /// <returns></returns>
        public PartialViewResult _DateTimeSpan()
        {
            DateTimeSpan dts = new DateTimeSpan() { DateStart = DateTime.Now.AddDays(-100), DateEnd = DateTime.Now };
            return PartialView(db.DateTimeSpanSet.Add(dts));
        }

        // POST: Glasses/Index
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DateTime? dt1, DateTime? dt2)
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

            if (ModelState.IsValid)
            {
                _repo.SaveNewGlass(glass);
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
