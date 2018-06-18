using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnowMyneighbour.Models;
using KnowMyneighbour.ViewModels;

namespace KnowMyneighbour.Controllers
{
    public class TripsLandingController : Controller
    {
        private Entities db = new Entities();

        // GET: TripsLanding
        public ActionResult Index()
        {
            //Trip tp = new Trip() { ActiveStatus=true,Amount=100,Charge=50,StartLocationId=1,DestinationId=2,DateAdded=DateTime.Now,DateModified=DateTime.Now,City = new City() {City1="isolo",StateId=1 } ,City1 = new City() {City1="Okota" },AspNetUser = new AspNetUser() { Email="lol@k.com"} };
            ViewBag.FromWhere = new SelectList(db.Cities, "Id", "City1");
            ViewBag.ToWhere = new SelectList(db.Cities, "Id", "City1");

            return View();
        }

        // GET: TripsLanding/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel svm)
        {
            
           
            var tp = from h in db.Trips join d in db.Cities on h.Id equals d.Id where d.City1 == svm.trips.City.City1 select h ;

            

            return View(svm);
        }
        // GET: TripsLanding/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TripsLanding/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TripsLanding/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TripsLanding/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TripsLanding/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TripsLanding/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
