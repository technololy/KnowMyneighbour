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
    public class AddressesController : Controller
    {
        private Entities db = new Entities();
       private DAL.Addresszz dt = new DAL.Addresszz();            
        //private Microsoft.AspNet.Identity =new Microsoft.AspNet.Identity.GetUserId();


        // GET: Addresses
        public ActionResult Index()
        {
            string userid = User.Identity.GetUserId();
            //var addresses = db.Addresses.Include(a => a.AspNetUser.Id==userid).Include(a => a.City).Include(a => a.State).Include(a => a.AspNetUser);
            //DAL.Addresszz dt = new DAL.Addresszz();
           var addresses =  dt.AddressIndex(User.Identity.GetUserId());
            return View(addresses.ToList());
        }

        // GET: Addresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var address = dt.GetAddressByID(id);
            //Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            ViewBag.AddressTypeId = new SelectList(db.AddressTypes, "Id", "TypeName");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "City1");
            ViewBag.StateId = new SelectList(db.States, "Id", "State1");
            ViewBag.ApplicationUserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Street,CityId,StateId,AddressTypeId,ApplicationUserId")] Address address)
        {
            if (ModelState.IsValid)
            {
                address.ApplicationUserId = User.Identity.GetUserId();
                //db.Addresses.Add(address);
                //db.SaveChanges();
                if (dt.CreateNewAddress(address)>0)
                {
                    TempData["success"] = true;
                    TempData["AlertMessage"] = "Great!! That was successful";
                }
                else
                {
                    TempData["success"] = false;

                    TempData["AlertMessage"] = "Snap!! That was Unsuccessful";
                }
                return RedirectToAction("Index");
            }

            ViewBag.AddressTypeId = new SelectList(db.AddressTypes, "Id", "TypeName", address.AddressTypeId);
            ViewBag.CityId = new SelectList(db.Cities, "Id", "City1", address.CityId);
            ViewBag.StateId = new SelectList(db.States, "Id", "State1", address.StateId);
            ViewBag.ApplicationUserId = new SelectList(db.AspNetUsers, "Id", "Email", address.ApplicationUserId);
            return View(address);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressTypeId = new SelectList(db.AddressTypes, "Id", "TypeName", address.AddressTypeId);
            ViewBag.CityId = new SelectList(db.Cities, "Id", "City1", address.CityId);
            ViewBag.StateId = new SelectList(db.States, "Id", "State1", address.StateId);
            ViewBag.ApplicationUserId = new SelectList(db.AspNetUsers, "Id", "Email", address.ApplicationUserId);
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Street,CityId,StateId,AddressTypeId,ApplicationUserId")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressTypeId = new SelectList(db.AddressTypes, "Id", "TypeName", address.AddressTypeId);
            ViewBag.CityId = new SelectList(db.Cities, "Id", "City1", address.CityId);
            ViewBag.StateId = new SelectList(db.States, "Id", "State1", address.StateId);
            ViewBag.ApplicationUserId = new SelectList(db.AspNetUsers, "Id", "Email", address.ApplicationUserId);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
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
