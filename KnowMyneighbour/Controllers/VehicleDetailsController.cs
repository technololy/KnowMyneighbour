using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KnowMyneighbour.Models;
using Microsoft.AspNet.Identity;

namespace KnowMyneighbour.Controllers
{
    public class VehicleDetailsController : Controller
    {
        private Entities db = new Entities();
       

        // GET: VehicleDetails
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            var vehicleDetails = db.VehicleDetails.Where(v => v.AspNetUser.Id==userid);
            return View(vehicleDetails.ToList());
        }

        // GET: VehicleDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleDetail vehicleDetail = db.VehicleDetails.Find(id);
            if (vehicleDetail == null)
            {
                return HttpNotFound();
            }
            return View(vehicleDetail);
        }

        // GET: VehicleDetails/Create
        public ActionResult Create()
        {
            //ViewBag.userId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: VehicleDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VehicleType,Make,Model,year,userId")] VehicleDetail vehicleDetail)
        {
            if (ModelState.IsValid)
            {
                vehicleDetail.userId = User.Identity.GetUserId();
                db.VehicleDetails.Add(vehicleDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.AspNetUsers, "Id", "Email", vehicleDetail.userId);
            return View(vehicleDetail);
        }

        // GET: VehicleDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleDetail vehicleDetail = db.VehicleDetails.Find(id);
            if (vehicleDetail == null)
            {
                return HttpNotFound();
            }
            //ViewBag.userId = new SelectList(db.AspNetUsers, "Id", "Email", vehicleDetail.userId);
            return View(vehicleDetail);
        }

        // POST: VehicleDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VehicleType,Make,Model,year,userId")] VehicleDetail vehicleDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.AspNetUsers, "Id", "Email", vehicleDetail.userId);
            return View(vehicleDetail);
        }

        // GET: VehicleDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleDetail vehicleDetail = db.VehicleDetails.Find(id);
            if (vehicleDetail == null)
            {
                return HttpNotFound();
            }
            return View(vehicleDetail);
        }

        // POST: VehicleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleDetail vehicleDetail = db.VehicleDetails.Find(id);
            db.VehicleDetails.Remove(vehicleDetail);
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
