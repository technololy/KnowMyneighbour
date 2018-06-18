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
    public class VehicleRoutesController : Controller
    {
        private Entities db = new Entities();

        // GET: VehicleRoutes
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            var vehicleRoutes = db.VehicleRoutes.Where(v => v.AspNetUser.Id==userid);
            return View(vehicleRoutes.ToList());
        }

        // GET: VehicleRoutes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleRoute vehicleRoute = db.VehicleRoutes.Find(id);
            if (vehicleRoute == null)
            {
                return HttpNotFound();
            }
            return View(vehicleRoute);
        }

        // GET: VehicleRoutes/Create
        public ActionResult Create()
        {
            //ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: VehicleRoutes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,StartLocation,DestinationLocation,userid")] VehicleRoute vehicleRoute)
        {
            if (ModelState.IsValid)
            {
                vehicleRoute.userid = User.Identity.GetUserId();
                db.VehicleRoutes.Add(vehicleRoute);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", vehicleRoute.userid);
            return View(vehicleRoute);
        }

        // GET: VehicleRoutes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleRoute vehicleRoute = db.VehicleRoutes.Find(id);
            if (vehicleRoute == null)
            {
                return HttpNotFound();
            }
            //ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", vehicleRoute.userid);
            return View(vehicleRoute);
        }

        // POST: VehicleRoutes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,StartLocation,DestinationLocation,userid")] VehicleRoute vehicleRoute)
        {
            if (ModelState.IsValid)
            {
                vehicleRoute.userid = User.Identity.GetUserId();

                db.Entry(vehicleRoute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", vehicleRoute.userid);
            return View(vehicleRoute);
        }

        // GET: VehicleRoutes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleRoute vehicleRoute = db.VehicleRoutes.Find(id);
            if (vehicleRoute == null)
            {
                return HttpNotFound();
            }
            return View(vehicleRoute);
        }

        // POST: VehicleRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleRoute vehicleRoute = db.VehicleRoutes.Find(id);
            db.VehicleRoutes.Remove(vehicleRoute);
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
