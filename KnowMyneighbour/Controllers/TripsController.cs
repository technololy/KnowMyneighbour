using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KnowMyneighbour.Models;
using KnowMyneighbour.Magic;
using Microsoft.AspNet.Identity;

namespace KnowMyneighbour.Controllers
{
    public class TripsController : Controller
    {
        private Entities db = new Entities();
     public static string loggedInUserID;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            loggedInUserID = User.Identity.GetUserId();
            base.OnActionExecuting(filterContext);
        }

        // GET: Trips
        public ActionResult Index()
        {
            ViewBag.StartLocationId = new SelectList(db.Cities, "Id", "City1");
            ViewBag.DestinationId = new SelectList(db.Cities, "Id", "City1");
           // DAL dl = new DAL();
            DAL.Trips dt = new DAL.Trips();
            var currentLoggedInEmail = User.Identity.GetUserName();
            ViewBag.currentLoggedInEmail = currentLoggedInEmail;
            // var tripzz = dl.TripIndex(User.Identity.GetUserId());
            //var tripzzz = dt.TripIndex(User.Identity.GetUserId());
            var tripzzz = dt.GetAllTrips();
            //var trips = db.Trips.Include(t => t.AspNetUser).Include(t => t.City).Include(t => t.City1);
            return View(tripzzz.ToList());
        }


        // GET: Trips
        public ActionResult IndexByUser()
        {
            ViewBag.StartLocationId = new SelectList(db.Cities, "Id", "City1");
            ViewBag.DestinationId = new SelectList(db.Cities, "Id", "City1");
            // DAL dl = new DAL();
            DAL.Trips dt = new DAL.Trips();
            var currentLoggedInEmail = User.Identity.GetUserName();
            ViewBag.currentLoggedInEmail = currentLoggedInEmail;
            // var tripzz = dl.TripIndex(User.Identity.GetUserId());
            //var tripzzz = dt.TripIndex(User.Identity.GetUserId());
            var tripzzz = dt.TripIndex(loggedInUserID);
            //var trips = db.Trips.Include(t => t.AspNetUser).Include(t => t.City).Include(t => t.City1);
            return View("Index",tripzzz.ToList());
        }

        [HttpPost]
        public ActionResult Search([Bind(Include = "StartLocationId,DestinationId")] Trip tripss)
        {
            //DAL d = new DAL();
            DAL.Trips dt = new DAL.Trips();

            var search = dt.SearchTrips(tripss.StartLocationId, tripss.DestinationId);
            //var trips = db.Trips.Where(t => t.StartLocationId==tripss.StartLocationId).Where(t=>t.DestinationId==tripss.DestinationId);
            ViewBag.StartLocationId = new SelectList(db.Cities, "Id", "City1");
            ViewBag.DestinationId = new SelectList(db.Cities, "Id", "City1");
            return View("Index", search.ToList());
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAL.Trips dt = new DAL.Trips();
            Trip trip = dt.GetTripsById(id);
           // Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            ViewBag.TripAdmin = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.StartLocationId = new SelectList(db.Cities, "Id", "City1");
            ViewBag.DestinationId = new SelectList(db.Cities, "Id", "City1");
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartLocationId,DestinationId,TripAdmin,Charge,DateAdded,ActiveStatus,DateModified")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.GetUserId();
                trip.TripAdmin = user;
                DAL.Trips dt = new DAL.Trips();
                dt.CreateTrip(trip);
                //db.Trips.Add(trip);
                //db.SaveChanges();
                //TempData["msg"] = "<script>alert('Change succesfully');</script>";
                TempData["success"] = true;
                TempData["AlertMessage"] = "Perfect!! That action has been saved";
                
                //ViewBag.Message = "Created Successfully";
                return RedirectToAction("Index");
            }
            TempData["success"] = false;
            TempData["AlertMessage"] = "Snap!! That action failed. Please try again";

            ViewBag.TripAdmin = new SelectList(db.AspNetUsers, "Id", "Email", trip.TripAdmin);
            ViewBag.StartLocationId = new SelectList(db.Cities, "Id", "City1", trip.StartLocationId);
            ViewBag.DestinationId = new SelectList(db.Cities, "Id", "City1", trip.DestinationId);
            return View(trip);
        }

        // GET: Trips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAL.Trips dt = new DAL.Trips();
            Trip trip = dt.GetTripsById(id);
            
            //Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            ViewBag.TripAdmin = new SelectList(db.AspNetUsers, "Id", "Email", trip.TripAdmin);
            ViewBag.StartLocationId = new SelectList(db.Cities, "Id", "City1", trip.StartLocationId);
            ViewBag.DestinationId = new SelectList(db.Cities, "Id", "City1", trip.DestinationId);
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartLocationId,DestinationId,TripAdmin,DateAdded,Charge,ActiveStatus,DateModified")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                trip.TripAdmin = User.Identity.GetUserId();                
                DAL.Trips dt = new DAL.Trips();
                dt.EditTripById(trip);              
              
                return RedirectToAction("Index");
            }
            ViewBag.TripAdmin = new SelectList(db.AspNetUsers, "Id", "Email", trip.TripAdmin);
            ViewBag.StartLocationId = new SelectList(db.Cities, "Id", "City1", trip.StartLocationId);
            ViewBag.DestinationId = new SelectList(db.Cities, "Id", "City1", trip.DestinationId);
            //TempData["AlertMessage"] = "failure";
            //TempData["msg"] = "<script>alert('Change Unsuccesfully');</script>";
            ViewBag.Message = "Created Unsuccessfully";


            return View(trip);
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Trip trip = db.Trips.Find(id);
            DAL.Trips dt = new DAL.Trips();
            Trip trip = dt.GetTripsById(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DAL.Trips dt = new DAL.Trips();
            dt.DeleteTripsById(id);
            //Trip trip = db.Trips.Find(id);
            //db.Trips.Remove(trip);
            //db.SaveChanges();
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
