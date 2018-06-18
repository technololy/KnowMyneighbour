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
    public class MoreUserDetailsController : Controller
    {
        private Entities db = new Entities();

        // GET: MoreUserDetails
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            var moreUserDetails = db.MoreUserDetails.Where(m => m.AspNetUser.Id==userid);
            return View(moreUserDetails.ToList());
        }

        // GET: MoreUserDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoreUserDetail moreUserDetail = db.MoreUserDetails.Find(id);
            if (moreUserDetail == null)
            {
                return HttpNotFound();
            }
            return View(moreUserDetail);
        }

        // GET: MoreUserDetails/Create
        public ActionResult Create()
        {
            //ViewBag.user_Id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: MoreUserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,HomeStreetAddress,HomeCityName,HomeLivingStateName,WorkStreetAddress,WorkCityName,WorkStateName,user_Id")] MoreUserDetail moreUserDetail)
        {
            if (ModelState.IsValid)
            {
                moreUserDetail.user_Id = User.Identity.GetUserId();
                db.MoreUserDetails.Add(moreUserDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.user_Id = new SelectList(db.AspNetUsers, "Id", "Email", moreUserDetail.user_Id);
            return View(moreUserDetail);
        }

        // GET: MoreUserDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoreUserDetail moreUserDetail = db.MoreUserDetails.Find(id);
            if (moreUserDetail == null)
            {
                return HttpNotFound();
            }
            //ViewBag.user_Id = new SelectList(db.AspNetUsers, "Id", "Email", moreUserDetail.user_Id);
            return View(moreUserDetail);
        }

        // POST: MoreUserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,HomeStreetAddress,HomeCityName,HomeLivingStateName,WorkStreetAddress,WorkCityName,WorkStateName,user_Id")] MoreUserDetail moreUserDetail)
        {
            if (ModelState.IsValid)
            {
                moreUserDetail.user_Id = User.Identity.GetUserId();
                db.Entry(moreUserDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.user_Id = new SelectList(db.AspNetUsers, "Id", "Email", moreUserDetail.user_Id);
            return View(moreUserDetail);
        }

        // GET: MoreUserDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoreUserDetail moreUserDetail = db.MoreUserDetails.Find(id);
            if (moreUserDetail == null)
            {
                return HttpNotFound();
            }
            return View(moreUserDetail);
        }

        // POST: MoreUserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MoreUserDetail moreUserDetail = db.MoreUserDetails.Find(id);
            db.MoreUserDetails.Remove(moreUserDetail);
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
