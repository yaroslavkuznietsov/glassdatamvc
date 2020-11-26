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
using GlassData.Web.ModelsViewCustom;

namespace GlassData.Web.Controllers
{
    public class GlassesController : Controller
    {
        private readonly DisconnectedRepository _repo = new DisconnectedRepository();

        private readonly GlassContext db = new GlassContext();

        private readonly MainViewModel mainViewModel = new MainViewModel();

        // GET: Glasses
        public ActionResult Index()
        {
            var glasses = _repo.GetGlassesWithOrder().OrderByDescending(g => g.TimeStamp).Take(100);
            mainViewModel.Glasses.Clear();
            foreach (var item in glasses)
            {
                mainViewModel.Glasses.Add(item);
            }
            mainViewModel.DataFilter.Count = mainViewModel.Glasses.Count();
            return View(mainViewModel);
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
        //public PartialViewResult _GlassesList()
        //{
        //    DateTime dt1 = DateTime.Now.AddDays(-7);
        //    DateTime dt2 = DateTime.Now;
        //    var glassSet = _repo.GetGlassesWithOrderCustomer().Where(g => g.TimeStamp >= dt1 && g.TimeStamp <= dt2).OrderBy(g => g.TimeStamp); 
        //    return PartialView(glassSet.ToList());
        //}

        #region _DateTimeSpan
        //public PartialViewResult _DateTimeSpan(DateTime? dt1, DateTime? dt2)
        //{
        //    if (dt1 == null || dt2 == null)
        //    {
        //        dt1 = DateTime.Now.AddDays(-3);
        //        dt2 = DateTime.Now;
        //    }

        //    mainViewModel.DateTimeSpan.DateStart = dt1;
        //    mainViewModel.DateTimeSpan.DateStart = dt2;


        //    var glasses = _repo.GetGlassesWithOrderCustomer().
        //            Where(g => g.TimeStamp >= dt1 && g.TimeStamp <= dt2).
        //            OrderBy(g => g.TimeStamp).Take(1000).ToList();

        //    return PartialView(mainViewModel.DateTimeSpan);
        //} 
        #endregion


        // POST: Glasses/Index
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DataFilterModel dataFilter)
        {
            DateTime dt1 = (DateTime)dataFilter.DateStart;
            DateTime dt2 = (DateTime)dataFilter.DateEnd;

            var glasses = _repo.GetGlassesWithOrder().Where(g => g.TimeStamp >= dt1 && g.TimeStamp <= dt2).OrderBy(g => g.TimeStamp);
            mainViewModel.Glasses.Clear();
            foreach (var item in glasses)
            {
                mainViewModel.Glasses.Add(item);
            }
            mainViewModel.DataFilter.Count = mainViewModel.Glasses.Count();
            return View(mainViewModel);
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

            if (glass.TimeStamp == DateTime.MinValue)
            {
                glass.TimeStamp = DateTime.Now;
            }

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
            Glass glass = _repo.GetGlassWithOrderCustomer(id.Value);
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
            #region MyRegion
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
